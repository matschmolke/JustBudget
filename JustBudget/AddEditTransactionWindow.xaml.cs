using JustBudget.Data;
using JustBudget.Models;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace JustBudget
{
    public partial class AddEditTransactionWindow : Window
    {
        private readonly AppDbContext _context;
        private readonly Transaction _transactionToEdit;

        public Visibility IsDeleteVisible => _transactionToEdit != null ? Visibility.Visible : Visibility.Collapsed;

        public AddEditTransactionWindow(AppDbContext context)
        {
            InitializeComponent();
            _context = context;
            DatePicker.SelectedDate = DateTime.Today;
        }
        public AddEditTransactionWindow(AppDbContext context, Transaction transactionToEdit) : this(context)
        {
            _transactionToEdit = transactionToEdit;
            NameBox.Text = transactionToEdit.Name;
            AmountBox.Text = transactionToEdit.Amount.ToString();
            DatePicker.SelectedDate = transactionToEdit.Date;
            TypeBox.SelectedIndex = transactionToEdit.TransactionType == TransactionType.Income ? 0 : 1;
        }

        private void SaveTransaction_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameBox.Text) ||
                !decimal.TryParse(AmountBox.Text, out decimal amount) ||
                TypeBox.SelectedItem is null || DatePicker.SelectedDate is null)
            {
                MessageBox.Show("Please fill out all fields correctly.");
                return;
            }

            var type = ((ComboBoxItem)TypeBox.SelectedItem).Content.ToString() == "Income" ? TransactionType.Income : TransactionType.Expense;

            if (_transactionToEdit != null)
            {
                _transactionToEdit.Name = NameBox.Text;
                _transactionToEdit.Amount = amount;
                _transactionToEdit.TransactionType = type;
                _transactionToEdit.Date = DatePicker.SelectedDate.Value;

                _context.Update(_transactionToEdit);
            }
            else
            {
                var transaction = new Transaction
                {
                    Name = NameBox.Text,
                    Amount = amount,
                    TransactionType = type,
                    Date = DatePicker.SelectedDate.Value
                };

                _context.Transactions.Add(transaction);
            }

            _context.SaveChanges();
            DialogResult = true;
            Close();
        }
        private void AmountBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !System.Text.RegularExpressions.Regex.IsMatch(
                ((TextBox)sender).Text.Insert(((TextBox)sender).SelectionStart, e.Text),
                @"^\d*([.,]?\d{0,2})?$");
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (_transactionToEdit != null)
            {
                var result = MessageBox.Show("Are you sure you want to delete this transaction?", "Confirm Delete", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    _context.Transactions.Remove(_transactionToEdit);
                    _context.SaveChanges();
                    DialogResult = true;
                    Close();
                }
            }
        }
    }
}
