using PersonalFinanceManager;

FinanceManager manager = new FinanceManager();
JsonStorage storage = new JsonStorage();

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
    Console.WriteLine("5. Exit");
    Console.WriteLine("====================================");
    Console.Write("Choose an option: ");

    string? choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            AddIncome(manager);
            break;

        case "2":
            AddExpense(manager);
            break;

        case "3":
            ViewTransactions(manager);
            break;

        case "4":
            ViewSummary(manager);
            break;

       case "5":
    storage.SaveTransactions(manager.GetTransactions());
    Console.WriteLine("Transactions saved.");
    running = false;
    break;

        default:
            Console.WriteLine("Invalid option. Please choose 1-5.");
            Pause();
            break;
    }
}

Console.WriteLine("Thank you for using Personal Finance Manager.");


// ---------------- ADD INCOME ----------------

static void AddIncome(FinanceManager manager)
{
    Console.Clear();
    Console.WriteLine("=== ADD INCOME ===");

    Console.Write("Description: ");
    string description = Console.ReadLine() ?? "";

    Console.Write("Category (for example Salary): ");
    string category = Console.ReadLine() ?? "";

    Console.Write("Amount: $");

    if (!decimal.TryParse(Console.ReadLine(), out decimal amount) || amount <= 0)
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

    Console.WriteLine();
    Console.WriteLine("Income added successfully!");

    Pause();
}


// ---------------- ADD EXPENSE ----------------

static void AddExpense(FinanceManager manager)
{
    Console.Clear();
    Console.WriteLine("=== ADD EXPENSE ===");

    Console.Write("Description: ");
    string description = Console.ReadLine() ?? "";

    Console.Write("Category (Food, Transport, Bills, etc.): ");
    string category = Console.ReadLine() ?? "";

    Console.Write("Amount: $");

    if (!decimal.TryParse(Console.ReadLine(), out decimal amount) || amount <= 0)
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

    Console.WriteLine();
    Console.WriteLine("Expense added successfully!");

    Pause();
}


// ---------------- VIEW TRANSACTIONS ----------------

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
        foreach (Transaction transaction in transactions)
        {
            Console.WriteLine(
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


// ---------------- FINANCIAL SUMMARY ----------------

static void ViewSummary(FinanceManager manager)
{
    Console.Clear();

    Console.WriteLine("=== FINANCIAL SUMMARY ===");
    Console.WriteLine();

    Console.WriteLine(
        $"Total Income:   ${manager.GetTotalIncome():F2}"
    );

    Console.WriteLine(
        $"Total Expenses: ${manager.GetTotalExpenses():F2}"
    );

    Console.WriteLine(
        $"Current Balance: ${manager.GetBalance():F2}"
    );

    Pause();
}


// ---------------- PAUSE ----------------

static void Pause()
{
    Console.WriteLine();
    Console.WriteLine("Press Enter to continue...");
    Console.ReadLine();
}