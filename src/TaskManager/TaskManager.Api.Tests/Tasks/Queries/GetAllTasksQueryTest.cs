using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Api.Tests.TestUtils;
using TaskManager.Common.Models.Tasks;
using TaskManager.Tasks.Models;
using TaskManager.Tasks.Queries;
using Xunit;
using Xunit.Abstractions;

namespace TaskManager.Api.Tests.Tasks.Queries;

[TestSubject(typeof(GetAllTasksQuery))]
public class GetAllTasksQueryTest(ServerFixture fixture, ITestOutputHelper console) : ServerTestBase(fixture, console)
{

    [Fact]
    public async Task Execute()
    {
        var tasks = new List<TaskDbDto>
        {
            new()
            {
                Name = "test1"
            },
            new()
            {
                Name = "test2"
            }
        };
        await Fixture.AddTask(tasks.ToArray());
        await using var scope = Fixture.TestAppFactory.Services.CreateAsyncScope();
        var query = scope.ServiceProvider.GetRequiredService<GetAllTasksQuery>();
        var actualTasks = (await query.Get()).ToList();
        
        Assert.NotNull(actualTasks);
        Assert.NotEmpty(actualTasks);
        Assert.Collection(actualTasks, x =>
        {
            x.Name = "test1";
        },
        x =>
        {
            x.Name = "test2";
        });
    }
}