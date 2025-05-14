using JustBudget.Data;
using JustBudget.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JustBudget;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly AppDbContext _context;

    public MainWindow(AppDbContext context)
    {
        InitializeComponent();
        _context = context;
        LoadBudgetSummary();
    }
    private void LoadBudgetSummary()
    {
        var transactions = _context.Transactions.ToList();

        var income = transactions
            .Where(t => t.TransactionType == TransactionType.Income)
            .Sum(t => t.Amount);

        var expenses = transactions
            .Where(t => t.TransactionType == TransactionType.Expense)
            .Sum(t => t.Amount);

        var remaining = income - expenses;

        IncomeText.Text = $"Income: {income}";
        SpendingsText.Text = $"Spendings: {expenses}";
        RemainingText.Text = $"Remaining Budget: {(remaining >= 0 ? "+" : "")}{remaining}";
        RemainingText.Foreground = new SolidColorBrush(remaining >= 0 ? Colors.LightGreen : Colors.IndianRed);
    }



    private void OpenTransactionsList(object sender, RoutedEventArgs e)
    {
        var transactionsWindow = new TransactionsWindow(_context); // <-- przekazanie kontekstu
        transactionsWindow.ShowDialog();
    }

    private void AddTransaction(object sender, RoutedEventArgs e)
    {
        var addWindow = new AddEditTransactionWindow(_context)
        {
            Owner = this
        };

        if (addWindow.ShowDialog() == true)
        {
            LoadBudgetSummary(); // odśwież dane po dodaniu
        }
    }
}