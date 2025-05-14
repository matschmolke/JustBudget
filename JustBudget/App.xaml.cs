using JustBudget.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Windows;

namespace JustBudget;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public static IServiceProvider ServiceProvider { get; private set; }

    protected override void OnStartup(StartupEventArgs e)
    {
        var services = new ServiceCollection();
        ConfigureServices(services);
        ServiceProvider = services.BuildServiceProvider();

        var db = ServiceProvider.GetRequiredService<AppDbContext>();
        DbInitializer.Initialize(db);

        var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }

    private void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite("Data Source=budget.db")); // lub pełna ścieżka

        services.AddSingleton<MainWindow>();
        services.AddTransient<TransactionsWindow>();
    }

    public static class FontManager
    {
        public static void SetScale(double multiplier)
        {
            Application.Current.Resources["FontScale"] = multiplier;

            double title = 28 * multiplier;
            double header = 20 * multiplier;
            double normal = 14 * multiplier;

            Application.Current.Resources["TitleBaseFontSize"] = title;
            Application.Current.Resources["HeaderBaseFontSize"] = header;
            Application.Current.Resources["NormalBaseFontSize"] = normal;
        }
    }
}

