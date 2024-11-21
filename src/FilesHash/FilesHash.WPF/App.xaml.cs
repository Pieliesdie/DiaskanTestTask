using System.Windows;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FilesHash;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public IServiceProvider ServiceProvider { get; private set; }
    public IConfiguration Configuration { get; private set; }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var builder = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        Configuration = builder.Build();

        // Create the service collection and configure services
        var serviceCollection = new ServiceCollection();
        ConfigureServices(Configuration, serviceCollection);

        // Build the service provider
        ServiceProvider = serviceCollection.BuildServiceProvider();

        // Start the main window
        var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
        mainWindow.Title = Configuration.GetSection("AppSettings")["AppTitle"] ?? "MainWindow";
        mainWindow.Show();
    }
}