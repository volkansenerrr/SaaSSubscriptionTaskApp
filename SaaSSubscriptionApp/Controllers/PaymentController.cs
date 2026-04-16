using Microsoft.AspNetCore.Mvc;
using SaaSSubscriptionApp.Models;
using System.Security.Cryptography;
using System.Text;
using SaaSSubscriptionApp.Services;

namespace SaaSSubscriptionApp.Controllers
{
    public class PaymentController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ParamPosService _paramPosService;

        private const string SecretKey = "SaaSSecret123";

        public PaymentController(AppDbContext context, ParamPosService paramPosService)
        {
            _context = context;
            _paramPosService = paramPosService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetInstallments(string cardNumber)
        {
            if (string.IsNullOrEmpty(cardNumber) || cardNumber.Length < 6)
                return BadRequest("Invalid card number");

            var bin = cardNumber.Substring(0, 6);

            var bank = GetBankFromBin(bin);

            var installments = new List<object>
            {
                new { count = 1, rate = 0 },
                new { count = 3, rate = 0.05 },
                new { count = 6, rate = 0.10 }
            };

            return Json(new
            {
                bin,
                bank,
                installments
            });
        }

        // 💳 PAYMENT INIT (API + SIMULATION)
        [HttpPost]
        public async Task<IActionResult> Pay(decimal amount)
        {
            var orderId = Guid.NewGuid().ToString();

            // 🔐 HASH SIGN
            var raw = $"orderId={orderId}&amount={amount}&key={SecretKey}";
            var hash = GenerateHash(raw);

            // 💾 DB kayıt
            var payment = new Payment
            {
                Amount = amount,
                IsSuccess = false,
                Date = DateTime.Now
            };

            _context.Payments.Add(payment);
            _context.SaveChanges();

            string apiResult;

            try
            {
                // 🔥 PARAMPOS API ÇAĞRISI
                apiResult = await _paramPosService.CreatePaymentAsync(amount, orderId, hash);
            }
            catch
            {
                // API çalışmazsa fallback
                apiResult = "API_ERROR_FALLBACK";
            }

            // demo success
            bool success = new Random().Next(0, 2) == 1;

            payment.IsSuccess = success;
            _context.SaveChanges();

            return Json(new
            {
                orderId,
                amount,
                hash,
                success,
                apiResult
            });
        }

        // 🔁 CALLBACK (SIMULATION)
        [HttpPost]
        public IActionResult Callback(string orderId, bool status)
        {
            var payment = _context.Payments
                .OrderByDescending(x => x.Id)
                .FirstOrDefault();

            if (payment != null)
            {
                payment.IsSuccess = status;
                _context.SaveChanges();
            }

            return Ok(new { message = "Callback processed" });
        }

        // 🔐 SHA512 HASH
        public string GenerateHash(string data)
        {
            using (SHA512 sha = SHA512.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(data);
                return Convert.ToBase64String(sha.ComputeHash(bytes));
            }
        }

        private string GetBankFromBin(string bin)
        {
            if (bin.StartsWith("4"))
                return "Visa Bank (Simulated)";

            if (bin.StartsWith("5"))
                return "Mastercard Bank (Simulated)";

            return "Unknown Bank";
        }
    }
}