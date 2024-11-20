using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Api.Tests.TestUtils;
using TaskManager.Api.Tests.TestUtils.Logging;
using TaskManager.Tasks.Models;

namespace TaskManager.Api.Tests;

public class ApiTestAppFactory(
    MemoryLoggerProvider loggerProvider
) : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.AddMemoryLogging(loggerProvider);
            services
                .RemoveByServiceType<TaskContext>()
                .AddScoped(_ =>
                {
                    var options = new DbContextOptionsBuilder<TaskContext>()
                        .UseInMemoryDatabase("InMemoryDb")
                        .Options;
                    return new TaskContext(options);
                });
        });
    }
}