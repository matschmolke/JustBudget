using JustBudget.Models;
using Microsoft.EntityFrameworkCore;
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
using Microsoft.EntityFrameworkCore;
using JustBudget.Data;


namespace JustBudget
{
    /// <summary>
    /// Interaction logic for TransactionsWindow.xaml
    /// </summary>
    public partial class TransactionsWindow : Window
    {
        private readonly AppDbContext _context;

        public TransactionsWindow(AppDbContext context)
        {
            InitializeComponent();
            _context = context;
            LoadTransactions();
        }

        private void LoadTransactions()
        {
            var transactions = _context.Transactions.ToList();
            TransactionsGrid.ItemsSource = transactions;
        }

        private void Close_Window(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Add_Transaction(object sender, RoutedEventArgs e)
        {
            var addWindow = new AddEditTransactionWindow(_context)
            {
                Owner = this
            };

            if (addWindow.ShowDialog() == true)
            {
                LoadTransactions(); // odśwież dane po dodaniu
            }
        }
        private void TransactionsGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (TransactionsGrid.SelectedItem is Transaction selectedTransaction)
            {
                var editWindow = new AddEditTransactionWindow(_context, selectedTransaction)
                {
                    Owner = this
                };

                if (editWindow.ShowDialog() == true)
                {
                    LoadTransactions(); // odśwież dane
                }
            }
        }
    }
}
