namespace PersonalFinanceManager
{
    public class Expense : Transaction
    {
        public Expense(
            string description,
            decimal amount,
            string category,
            DateTime date)
            : base(description, amount, category, date)
        {
        }

        public override string GetTransactionType()
        {
            return "Expense";
        }
    }
}