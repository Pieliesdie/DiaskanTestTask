using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace TaskManager.Api.Tests;

[Collection(nameof(ApiDefinition))]
public class ServerTestBase : IAsyncLifetime
{
    protected readonly ITestOutputHelper Console;
    protected readonly ServerFixture Fixture;
    protected readonly HttpClient HttpClient;

    public ServerTestBase(ServerFixture fixture, ITestOutputHelper console)
    {
        Fixture = fixture;
        Console = console;

        HttpClient = fixture.TestAppFactory.CreateClient();

        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
    }
    
    
    public virtual async Task InitializeAsync()
    {
        await Fixture.ResetAsync();
    }

    public virtual Task DisposeAsync()
    {
        Fixture.LoggerProvider.DumpAndClear(Console.WriteLine);
        return Task.CompletedTask;
    }
}
