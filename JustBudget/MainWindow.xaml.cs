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
using System.Globalization;
using static JustBudget.App;

namespace JustBudget;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly AppDbContext _context;
    private AppSettings _settings;

    public MainWindow(AppDbContext context)
    {
        InitializeComponent();
        _context = context;
        _settings = SettingsManager.Load();
        FontManager.SetScale(_settings.FontSize);
        UpdateSummaryTitle();
        LoadBudgetSummary();

    }
    public void LoadBudgetSummary()
    {
        var transactions = _context.Transactions.ToList();

        if (_settings.FilterMonth != 0)
            transactions = transactions.Where(t => t.Date.Month == _settings.FilterMonth).ToList();

        if (_settings.FilterYear != 0)
            transactions = transactions.Where(t => t.Date.Year == _settings.FilterYear).ToList();

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

    private void UpdateSummaryTitle()
    {
        string monthText = _settings.FilterMonth == 0
            ? "All Months"
            : new DateTime(2000, _settings.FilterMonth, 1).ToString("MMMM", CultureInfo.InvariantCulture);


        string yearText = _settings.FilterYear == 0
            ? ""
            : _settings.FilterYear.ToString();

        string title = "Budget Summary – ";

        if (_settings.FilterMonth == 0 && _settings.FilterYear == 0)
            title += "All Time";
        else
            title += $"{monthText} {yearText}".Trim();

        SummaryTitle.Text = title;
    }

    private void UpdateButton_Click(object sender, RoutedEventArgs e)
    {
        LoadBudgetSummary();     
        UpdateSummaryTitle();    
    }

    private void SetFontSmall(object sender, RoutedEventArgs e)
    {
        FontManager.SetScale(0.9); 
        _settings.FontSize = 0.9;
        SettingsManager.Save(_settings);
    }

    private void SetFontMedium(object sender, RoutedEventArgs e)
    {
        FontManager.SetScale(1.0);
        _settings.FontSize = 1.0;
        SettingsManager.Save(_settings);
    }

    private void SetFontLarge(object sender, RoutedEventArgs e)
    {
        FontManager.SetScale(1.2);
        _settings.FontSize = 1.2;
        SettingsManager.Save(_settings);
    }

    private void FilterMonth_Click(object sender, RoutedEventArgs e)
    {
        if (sender is MenuItem item && int.TryParse(item.Tag?.ToString(), out int month))
        {
            _settings.FilterMonth = month;
            SettingsManager.Save(_settings);
            UpdateSummaryTitle();
            LoadBudgetSummary();
        }
    }

    private void SetMonthFilter(object sender, RoutedEventArgs e)
    {
        if (sender is MenuItem item && int.TryParse(item.Tag.ToString(), out int month))
        {
            _settings.FilterMonth = month;
            SettingsManager.Save(_settings);
            UpdateSummaryTitle();
            LoadBudgetSummary();
        }
    }

    private void SetYearFilter(object sender, RoutedEventArgs e)
    {
        if (sender is MenuItem item && int.TryParse(item.Tag.ToString(), out int year))
        {
            _settings.FilterYear = year;
            SettingsManager.Save(_settings);
            UpdateSummaryTitle();
            LoadBudgetSummary();
        }
    }

    private void ResetFilters(object sender, RoutedEventArgs e)
    {
        _settings.FilterMonth = 0;
        _settings.FilterYear = 0;
        SettingsManager.Save(_settings);
        UpdateSummaryTitle();
        LoadBudgetSummary();
    }

    private void ClearAllData(object sender, RoutedEventArgs e)
    {
        var result = MessageBox.Show("Are you sure you want to delete ALL transactions?", "Confirm", MessageBoxButton.YesNo);
        if (result == MessageBoxResult.Yes)
        {
            _context.Transactions.RemoveRange(_context.Transactions);
            _context.SaveChanges();
            LoadBudgetSummary();
        }
    }

}