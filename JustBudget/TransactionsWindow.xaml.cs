using JustBudget.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace JustBudget
{
    /// <summary>
    /// Interaction logic for TransactionsWindow.xaml
    /// </summary>
    public partial class TransactionsWindow : Window
    {
        public TransactionsWindow()
        {
            InitializeComponent();
            LoadTransactions();
        }

        private void LoadTransactions()
        {
            var transactions = new List<Transaction>
            {
                new Transaction { Name = "Salary", Amount = 5000, Date = DateTime.Now, TransactionType = TransactionType.Income },
                new Transaction { Name = "Groceries", Amount = 300, Date = DateTime.Now, TransactionType = TransactionType.Expense },
                new Transaction { Name = "Electric Bill", Amount = 150, Date = DateTime.Now, TransactionType = TransactionType.Expense },
                new Transaction { Name = "Freelance Work", Amount = 1200, Date = DateTime.Now, TransactionType = TransactionType.Income }
            };

            TransactionsGrid.ItemsSource = transactions;
        }

        private void Close_Window(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Add_Transaction(object sender, RoutedEventArgs e)
        {

        }
    }
}
