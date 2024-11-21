using FilesHash.Db.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FilesHash;

public partial class App
{
    public static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
    {
        services.AddSingleton<MainWindow>();
        services.AutoRegister();
        var sqlLiteConnectionString = configuration.GetConnectionString("SQLite")
            ?? throw new InvalidOperationException("SQLite connection string not configured.");
        services.AddDbContext<FileHashDbContext>(o => o.UseSqlite(sqlLiteConnectionString));
        services.AddDbContextFactory<FileHashDbContext>(o => o.UseSqlite(sqlLiteConnectionString));
    }
}