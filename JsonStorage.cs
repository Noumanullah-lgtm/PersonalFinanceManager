using System.Text.Json;

namespace PersonalFinanceManager
{
    public class JsonStorage
    {
        private readonly string filePath = "transactions.json";

        public void SaveTransactions(List<Transaction> transactions)
        {
            List<TransactionData> data = new List<TransactionData>();

            foreach (Transaction transaction in transactions)
            {
                TransactionData item = new TransactionData
                {
                    Description = transaction.Description,
                    Amount = transaction.Amount,
                    Category = transaction.Category,
                    Date = transaction.Date,
                    Type = transaction.GetTransactionType()
                };

                data.Add(item);
            }

            string json = JsonSerializer.Serialize(
                data,
                new JsonSerializerOptions { WriteIndented = true }
            );

            File.WriteAllText(filePath, json);
        }

        public List<Transaction> LoadTransactions()
        {
            List<Transaction> transactions = new List<Transaction>();

            if (!File.Exists(filePath))
            {
                return transactions;
            }

            try
            {
                string json = File.ReadAllText(filePath);

                List<TransactionData>? data =
                    JsonSerializer.Deserialize<List<TransactionData>>(json);

                if (data == null)
                {
                    return transactions;
                }

                foreach (TransactionData item in data)
                {
                    if (item.Type == "Income")
                    {
                        transactions.Add(
                            new Income(
                                item.Description,
                                item.Amount,
                                item.Category,
                                item.Date
                            )
                        );
                    }
                    else if (item.Type == "Expense")
                    {
                        transactions.Add(
                            new Expense(
                                item.Description,
                                item.Amount,
                                item.Category,
                                item.Date
                            )
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Could not load transactions: {ex.Message}"
                );
            }

            return transactions;
        }
    }
}