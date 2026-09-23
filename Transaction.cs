namespace PersonalFinanceManager
{
    public abstract class Transaction
    {
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public string Category { get; set; }
        public DateTime Date { get; set; }

        public Transaction(
            string description,
            decimal amount,
            string category,
            DateTime date)
        {
            Description = description;
            Amount = amount;
            Category = category;
            Date = date;
        }

        public abstract string GetTransactionType();
    }
}