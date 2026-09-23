namespace PersonalFinanceManager
{
    public class TransactionData
    {
        public string Description { get; set; } = "";
        public decimal Amount { get; set; }
        public string Category { get; set; } = "";
        public DateTime Date { get; set; }
        public string Type { get; set; } = "";
    }
}