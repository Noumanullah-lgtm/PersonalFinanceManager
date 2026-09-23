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

        public bool DeleteTransaction(int index)
        {
            if (index >= 0 && index < transactions.Count)
            {
                transactions.RemoveAt(index);
                return true;
            }

            return false;
        }

        public bool EditTransaction(
            int index,
            string description,
            decimal amount,
            string category)
        {
            if (index >= 0 && index < transactions.Count)
            {
                Transaction transaction = transactions[index];

                transaction.Description = description;
                transaction.Amount = amount;
                transaction.Category = category;

                return true;
            }

            return false;
        }

        // Get only income transactions
        public List<Transaction> GetIncomeTransactions()
        {
            return transactions
                .Where(t => t is Income)
                .ToList();
        }

        // Get only expense transactions
        public List<Transaction> GetExpenseTransactions()
        {
            return transactions
                .Where(t => t is Expense)
                .ToList();
        }

        // Find transactions that match a category
        public List<Transaction> GetTransactionsByCategory(string category)
        {
            return transactions
                .Where(t =>
                    t.Category.Equals(
                        category,
                        StringComparison.OrdinalIgnoreCase))
                .ToList();
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