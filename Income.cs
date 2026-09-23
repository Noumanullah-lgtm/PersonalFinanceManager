namespace PersonalFinanceManager
{
    public class Income : Transaction
    {
        public Income(
            string description,
            decimal amount,
            string category,
            DateTime date)
            : base(description, amount, category, date)
        {
        }

        public override string GetTransactionType()
        {
            return "Income";
        }
    }
}