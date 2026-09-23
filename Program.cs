using PersonalFinanceManager;

FinanceManager manager = new FinanceManager();
JsonStorage storage = new JsonStorage();

// Load saved transactions when application starts
manager.LoadTransactions(storage.LoadTransactions());

bool running = true;

while (running)
{
    Console.Clear();

    Console.WriteLine("====================================");
    Console.WriteLine("      PERSONAL FINANCE MANAGER");
    Console.WriteLine("====================================");
    Console.WriteLine("1. Add Income");
    Console.WriteLine("2. Add Expense");
    Console.WriteLine("3. View Transactions");
    Console.WriteLine("4. View Financial Summary");
    Console.WriteLine("5. Delete Transaction");
    Console.WriteLine("6. Exit");
    Console.WriteLine("====================================");
    Console.Write("Choose an option: ");

    string? choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            AddIncome(manager, storage);
            break;

        case "2":
            AddExpense(manager, storage);
            break;

        case "3":
            ViewTransactions(manager);
            break;

        case "4":
            ViewSummary(manager);
            break;

        case "5":
            DeleteTransaction(manager, storage);
            break;

        case "6":
            storage.SaveTransactions(manager.GetTransactions());
            running = false;
            break;

        default:
            Console.WriteLine("Invalid option. Please choose 1-6.");
            Pause();
            break;
    }
}

Console.WriteLine("Thank you for using Personal Finance Manager.");


// ADD INCOME
static void AddIncome(
    FinanceManager manager,
    JsonStorage storage)
{
    Console.Clear();
    Console.WriteLine("=== ADD INCOME ===");

    Console.Write("Description: ");
    string description = Console.ReadLine() ?? "";

    Console.Write("Category (for example Salary): ");
    string category = Console.ReadLine() ?? "";

    Console.Write("Amount: $");

    if (!decimal.TryParse(Console.ReadLine(), out decimal amount)
        || amount <= 0)
    {
        Console.WriteLine("Invalid amount.");
        Pause();
        return;
    }

    Income income = new Income(
        description,
        amount,
        category,
        DateTime.Now
    );

    manager.AddTransaction(income);

    // Save immediately
    storage.SaveTransactions(manager.GetTransactions());

    Console.WriteLine();
    Console.WriteLine("Income added and saved successfully!");

    Pause();
}


// ADD EXPENSE
static void AddExpense(
    FinanceManager manager,
    JsonStorage storage)
{
    Console.Clear();
    Console.WriteLine("=== ADD EXPENSE ===");

    Console.Write("Description: ");
    string description = Console.ReadLine() ?? "";

    Console.Write("Category (Food, Transport, Bills, etc.): ");
    string category = Console.ReadLine() ?? "";

    Console.Write("Amount: $");

    if (!decimal.TryParse(Console.ReadLine(), out decimal amount)
        || amount <= 0)
    {
        Console.WriteLine("Invalid amount.");
        Pause();
        return;
    }

    Expense expense = new Expense(
        description,
        amount,
        category,
        DateTime.Now
    );

    manager.AddTransaction(expense);

    // Save immediately
    storage.SaveTransactions(manager.GetTransactions());

    Console.WriteLine();
    Console.WriteLine("Expense added and saved successfully!");

    Pause();
}


// VIEW TRANSACTIONS
static void ViewTransactions(FinanceManager manager)
{
    Console.Clear();

    Console.WriteLine("=== TRANSACTION HISTORY ===");
    Console.WriteLine();

    List<Transaction> transactions = manager.GetTransactions();

    if (transactions.Count == 0)
    {
        Console.WriteLine("No transactions have been recorded.");
    }
    else
    {
        for (int i = 0; i < transactions.Count; i++)
        {
            Transaction transaction = transactions[i];

            Console.WriteLine(
                $"{i + 1}. " +
                $"{transaction.Date:dd/MM/yyyy} | " +
                $"{transaction.GetTransactionType()} | " +
                $"{transaction.Description} | " +
                $"{transaction.Category} | " +
                $"${transaction.Amount:F2}"
            );
        }
    }

    Pause();
}


// VIEW SUMMARY
static void ViewSummary(FinanceManager manager)
{
    Console.Clear();

    Console.WriteLine("=== FINANCIAL SUMMARY ===");
    Console.WriteLine();

    Console.WriteLine(
        $"Total Income:    ${manager.GetTotalIncome():F2}"
    );

    Console.WriteLine(
        $"Total Expenses:  ${manager.GetTotalExpenses():F2}"
    );

    Console.WriteLine(
        $"Current Balance: ${manager.GetBalance():F2}"
    );

    Pause();
}


// DELETE TRANSACTION
static void DeleteTransaction(
    FinanceManager manager,
    JsonStorage storage)
{
    Console.Clear();

    Console.WriteLine("=== DELETE TRANSACTION ===");
    Console.WriteLine();

    List<Transaction> transactions = manager.GetTransactions();

    if (transactions.Count == 0)
    {
        Console.WriteLine("There are no transactions to delete.");
        Pause();
        return;
    }

    for (int i = 0; i < transactions.Count; i++)
    {
        Transaction transaction = transactions[i];

        Console.WriteLine(
            $"{i + 1}. " +
            $"{transaction.GetTransactionType()} | " +
            $"{transaction.Description} | " +
            $"{transaction.Category} | " +
            $"${transaction.Amount:F2}"
        );
    }

    Console.WriteLine();
    Console.Write("Enter the number of the transaction to delete: ");

    if (!int.TryParse(Console.ReadLine(), out int number))
    {
        Console.WriteLine("Invalid number.");
        Pause();
        return;
    }

    int index = number - 1;

    bool deleted = manager.DeleteTransaction(index);

    if (deleted)
    {
        storage.SaveTransactions(manager.GetTransactions());
        Console.WriteLine("Transaction deleted successfully.");
    }
    else
    {
        Console.WriteLine("Transaction not found.");
    }

    Pause();
}


// PAUSE
static void Pause()
{
    Console.WriteLine();
    Console.WriteLine("Press Enter to continue...");
    Console.ReadLine();
}