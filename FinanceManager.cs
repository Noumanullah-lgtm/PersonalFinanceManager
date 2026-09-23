namespace PersonalFinanceManager
{
    public class FinanceManager
    {
        private List<Transaction> transactions = new List<Transaction>();

        // Add a new transaction
        public void AddTransaction(Transaction transaction)
        {
            transactions.Add(transaction);
        }

        // Load transactions from JSON storage
        public void LoadTransactions(List<Transaction> loadedTransactions)
        {
            transactions = loadedTransactions;
        }

        // Return all transactions
        public List<Transaction> GetTransactions()
        {
            return transactions;
        }

        // Delete a transaction
        public bool DeleteTransaction(int index)
        {
            if (index >= 0 && index < transactions.Count)
            {
                transactions.RemoveAt(index);
                return true;
            }

            return false;
        }

        // Edit an existing transaction
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

        // Calculate total income
        public decimal GetTotalIncome()
        {
            return transactions
                .Where(t => t is Income)
                .Sum(t => t.Amount);
        }

        // Calculate total expenses
        public decimal GetTotalExpenses()
        {
            return transactions
                .Where(t => t is Expense)
                .Sum(t => t.Amount);
        }

        // Calculate current balance
        public decimal GetBalance()
        {
            return GetTotalIncome() - GetTotalExpenses();
        }
    }
}