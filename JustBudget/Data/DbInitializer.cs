using JustBudget.Models;
using System;
using System.Linq;

namespace JustBudget.Data
{
    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Transactions.Any())
                return;

            var transactions = new[]
            {
                new Transaction { Name = "Salary", Amount = 5000, TransactionType = TransactionType.Income, Date = DateTime.Today },
                new Transaction { Name = "Freelance", Amount = 1200, TransactionType = TransactionType.Income, Date = DateTime.Today.AddDays(-5) },
                new Transaction { Name = "Groceries", Amount = 300, TransactionType = TransactionType.Expense, Date = DateTime.Today.AddDays(-3) },
                new Transaction { Name = "Rent", Amount = 1500, TransactionType = TransactionType.Expense, Date = DateTime.Today.AddDays(-10) },
            };

            //Uncomment if you want to add this default data above
            //context.Transactions.AddRange(transactions);
            context.SaveChanges();
        }
    }
}
