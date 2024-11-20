using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Api.Tests.TestUtils.Logging;
using TaskManager.Tasks.Models;
using Xunit;

namespace TaskManager.Api.Tests;

// ReSharper disable once ClassNeverInstantiated.Global
public class ServerFixture : IAsyncLifetime
{
    public readonly MemoryLoggerProvider LoggerProvider = new();
    public readonly ApiTestAppFactory TestAppFactory;

    public ServerFixture()
    {
        TestAppFactory = new ApiTestAppFactory(LoggerProvider);
    }

    public async Task InitializeAsync() { }

    public async Task DisposeAsync()
    {
        await TestAppFactory.DisposeAsync();
    }

    public async Task AddTask(params TaskDbDto[] tasks)
    {
        await using var scope = TestAppFactory.Services.CreateAsyncScope();
        var taskContext = scope.ServiceProvider.GetRequiredService<TaskContext>();
        await taskContext.Tasks.AddRangeAsync(tasks);
        await taskContext.SaveChangesAsync();
    }
    
    public async Task ResetAsync()
    {
        await using var scope = TestAppFactory.Services.CreateAsyncScope();
        var taskContext = scope.ServiceProvider.GetRequiredService<TaskContext>();
        var tasks = await taskContext.Tasks.ToListAsync();
        taskContext.RemoveRange(tasks);
        await taskContext.SaveChangesAsync();
    }
}