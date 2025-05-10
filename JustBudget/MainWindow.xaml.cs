using JustBudget.Data;
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
    }

    private void OpenTransactionsList(object sender, RoutedEventArgs e)
    {
        var transactionsWindow = new TransactionsWindow(_context); // <-- przekazanie kontekstu
        transactionsWindow.ShowDialog();
    }
}