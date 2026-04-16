using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace SaaSSubscriptionApp.Services
{
    public class ParamPosService
    {
        private readonly HttpClient _httpClient;

        public ParamPosService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> CreatePaymentAsync(decimal amount, string orderId, string hash)
        {
            var request = new
            {
                merchant_id = "TEST_MERCHANT",
                order_id = orderId,
                amount = amount,
                currency = "TRY",
                hash = hash,
                callback_url = "https://localhost:5001/Payment/Callback"
            };

            var json = JsonSerializer.Serialize(request);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // ⚠️ ParamPos sandbox endpoint (dokümana göre değişebilir)
            var response = await _httpClient.PostAsync("https://sandbox.parampos.com/api/payment", content);

            return await response.Content.ReadAsStringAsync();
        }
    }
}