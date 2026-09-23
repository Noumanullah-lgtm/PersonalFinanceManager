namespace PersonalFinanceManager
{
    public class FinanceManager
    {
        private List<Transaction> transactions = new List<Transaction>();

        public void AddTransaction(Transaction transaction)
        {
            transactions.Add(transaction);
        }

        public void LoadTransactions(List<Transaction> loadedTransactions)
        {
            transactions = loadedTransactions;
        }

        public List<Transaction> GetTransactions()
        {
            return transactions;
        }

        public decimal GetTotalIncome()
        {
            return transactions
                .Where(t => t is Income)
                .Sum(t => t.Amount);
        }

        public decimal GetTotalExpenses()
        {
            return transactions
                .Where(t => t is Expense)
                .Sum(t => t.Amount);
        }

        public decimal GetBalance()
        {
            return GetTotalIncome() - GetTotalExpenses();
        }
    }
}