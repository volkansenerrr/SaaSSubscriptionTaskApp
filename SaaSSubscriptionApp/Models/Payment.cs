namespace SaaSSubscriptionApp.Models
{
    public class Payment
    {
        public int Id { get; set; }   // 🔥 PRIMARY KEY (zorunlu)

        public decimal Amount { get; set; }
        public bool IsSuccess { get; set; }
        public DateTime Date { get; set; }
    }
}