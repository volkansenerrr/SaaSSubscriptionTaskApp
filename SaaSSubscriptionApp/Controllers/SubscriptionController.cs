using Microsoft.AspNetCore.Mvc;

namespace SaaSSubscriptionApp.Controllers
{
    public class SubscriptionController : Controller
    {
        private readonly AppDbContext _context;

        public SubscriptionController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetPlans()
        {
            return Json(_context.SubscriptionPlans.ToList());
        }

        public IActionResult GetFeatures()
        {
            return Json(_context.Features.ToList());
        }

        // 🔥 FİYAT HESAPLAMA BURAYA
        [HttpPost]
        public IActionResult CalculateTotal(int planId, List<int> featureIds, bool yearly)
        {
            var plan = _context.SubscriptionPlans.Find(planId);
            var features = _context.Features.Where(x => featureIds.Contains(x.Id)).ToList();

            decimal total = plan.Price + features.Sum(x => x.Price);

            if (yearly)
            {
                total = total * 12;
                total = total * 0.8m; // %20 indirim
            }

            return Json(total);
        }
    }
}
