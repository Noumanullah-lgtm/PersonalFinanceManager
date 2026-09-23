using PersonalFinanceManager;

FinanceManager manager = new FinanceManager();
JsonStorage storage = new JsonStorage();

// Load saved transactions when the application starts
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
    Console.WriteLine("5. Filter Transactions");
    Console.WriteLine("6. Edit Transaction");
    Console.WriteLine("7. Delete Transaction");
    Console.WriteLine("8. Exit");
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
            FilterTransactions(manager);
            break;

        case "6":
            EditTransaction(manager, storage);
            break;

        case "7":
            DeleteTransaction(manager, storage);
            break;

        case "8":
            storage.SaveTransactions(manager.GetTransactions());
            running = false;
            break;

        default:
            Console.WriteLine("Invalid option. Please choose 1-8.");
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

    storage.SaveTransactions(manager.GetTransactions());

    Console.WriteLine();
    Console.WriteLine("Expense added and saved successfully!");

    Pause();
}


// VIEW ALL TRANSACTIONS
static void ViewTransactions(FinanceManager manager)
{
    Console.Clear();

    Console.WriteLine("=== TRANSACTION HISTORY ===");
    Console.WriteLine();

    DisplayTransactions(manager.GetTransactions());

    Pause();
}


// FILTER TRANSACTIONS
static void FilterTransactions(FinanceManager manager)
{
    bool filtering = true;

    while (filtering)
    {
        Console.Clear();

        Console.WriteLine("=== FILTER TRANSACTIONS ===");
        Console.WriteLine();
        Console.WriteLine("1. Show Income");
        Console.WriteLine("2. Show Expenses");
        Console.WriteLine("3. Search by Category");
        Console.WriteLine("4. Back");
        Console.WriteLine();
        Console.Write("Choose an option: ");

        string? choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                Console.Clear();
                Console.WriteLine("=== INCOME TRANSACTIONS ===");
                Console.WriteLine();

                DisplayTransactions(
                    manager.GetIncomeTransactions()
                );

                Pause();
                break;

            case "2":
                Console.Clear();
                Console.WriteLine("=== EXPENSE TRANSACTIONS ===");
                Console.WriteLine();

                DisplayTransactions(
                    manager.GetExpenseTransactions()
                );

                Pause();
                break;

            case "3":
                Console.Clear();
                Console.WriteLine("=== SEARCH BY CATEGORY ===");
                Console.WriteLine();

                Console.Write("Enter category: ");
                string category = Console.ReadLine() ?? "";

                Console.WriteLine();
                Console.WriteLine(
                    $"=== RESULTS FOR {category.ToUpper()} ==="
                );
                Console.WriteLine();

                DisplayTransactions(
                    manager.GetTransactionsByCategory(category)
                );

                Pause();
                break;

            case "4":
                filtering = false;
                break;

            default:
                Console.WriteLine("Invalid option. Please choose 1-4.");
                Pause();
                break;
        }
    }
}


// DISPLAY A LIST OF TRANSACTIONS
static void DisplayTransactions(
    List<Transaction> transactions)
{
    if (transactions.Count == 0)
    {
        Console.WriteLine("No matching transactions found.");
        return;
    }

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


// EDIT TRANSACTION
static void EditTransaction(
    FinanceManager manager,
    JsonStorage storage)
{
    Console.Clear();

    Console.WriteLine("=== EDIT TRANSACTION ===");
    Console.WriteLine();

    List<Transaction> transactions = manager.GetTransactions();

    if (transactions.Count == 0)
    {
        Console.WriteLine("There are no transactions to edit.");
        Pause();
        return;
    }

    DisplayTransactions(transactions);

    Console.WriteLine();
    Console.Write("Enter the number of the transaction to edit: ");

    if (!int.TryParse(Console.ReadLine(), out int number))
    {
        Console.WriteLine("Invalid number.");
        Pause();
        return;
    }

    int index = number - 1;

    if (index < 0 || index >= transactions.Count)
    {
        Console.WriteLine("Transaction not found.");
        Pause();
        return;
    }

    Transaction selectedTransaction = transactions[index];

    Console.WriteLine();
    Console.WriteLine("Current details:");

    Console.WriteLine(
        $"{selectedTransaction.GetTransactionType()} | " +
        $"{selectedTransaction.Description} | " +
        $"{selectedTransaction.Category} | " +
        $"${selectedTransaction.Amount:F2}"
    );

    Console.WriteLine();

    Console.Write("New description: ");
    string description = Console.ReadLine() ?? "";

    Console.Write("New category: ");
    string category = Console.ReadLine() ?? "";

    Console.Write("New amount: $");

    if (!decimal.TryParse(Console.ReadLine(), out decimal amount)
        || amount <= 0)
    {
        Console.WriteLine("Invalid amount.");
        Pause();
        return;
    }

    bool edited = manager.EditTransaction(
        index,
        description,
        amount,
        category
    );

    if (edited)
    {
        storage.SaveTransactions(manager.GetTransactions());
        Console.WriteLine(
            "Transaction updated and saved successfully!"
        );
    }
    else
    {
        Console.WriteLine(
            "Transaction could not be updated."
        );
    }

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

    DisplayTransactions(transactions);

    Console.WriteLine();
    Console.Write(
        "Enter the number of the transaction to delete: "
    );

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
        Console.WriteLine(
            "Transaction deleted successfully."
        );
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