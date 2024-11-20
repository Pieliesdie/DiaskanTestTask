using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using JetBrains.Annotations;
using TaskManager.Api.Tests.TestUtils;
using TaskManager.Common.Models.Tasks;
using TaskManager.Tasks.Endpoints;
using TaskManager.Tasks.Models;
using Xunit;
using Xunit.Abstractions;

namespace TaskManager.Api.Tests.Tasks.Endpoints;

[TestSubject(typeof(TasksController))]
public class TaskControllerTests(ServerFixture fixture, ITestOutputHelper console) : ServerTestBase(fixture, console)
{
    [Fact]
    public async Task GetAllTasks_ShouldReturnAllTasks()
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

        var actualTasks = await HttpClient.GetAsync("api/Tasks/GetAll").Read<TaskClientDto[]>();

        Assert.NotNull(actualTasks);
        Assert.NotEmpty(actualTasks);
        Assert.Collection(actualTasks,
            x =>
            {
                x.Name = "test1";
            },
            x =>
            {
                x.Name = "test2";
            }
        );
    }

    [Fact]
    public async Task GetById_ShouldReturnTask()
    {
        var task = new TaskDbDto()
        {
            Id = Guid.NewGuid(),
            Name = "test1"
        };

        await Fixture.AddTask(task);

        var actualTasks = await HttpClient.GetAsync($"api/Tasks/Get/{task.Id}").Read<TaskClientDto>();

        Assert.NotNull(actualTasks);
        Assert.Equal(task.Id, actualTasks.Id);
        Assert.Equal(task.Name, actualTasks.Name);
    }

    [Fact]
    public async Task GetById_ShouldReturn404NotFound()
    {
        var task = new TaskDbDto
        {
            Id = Guid.NewGuid(),
            Name = "test1"
        };

        await Fixture.AddTask(task);

        var res = await HttpClient.GetAsync($"api/Tasks/Get/{Guid.NewGuid()}");
        Assert.Equal(HttpStatusCode.NotFound, res.StatusCode);
    }

    [Fact]
    public async Task CreateTask_ShouldCreateTask()
    {
        var task = new TaskClientDto()
        {
            Id = Guid.NewGuid(),
            Name = "test1"
        };
        var createRes = await HttpClient.PutAsync("api/Tasks/Create", JsonContent.Create(task));
        createRes.EnsureSuccessStatusCode();
        var taskRes = await HttpClient.GetAsync($"api/Tasks/Get/{task.Id}").Read<TaskClientDto>();
        Assert.NotNull(taskRes);
        Assert.Equal(task.Name, taskRes.Name);
    }

    [Fact]
    public async Task RemoveTask_ShouldRemoveTask()
    {
        var tasks = new List<TaskDbDto>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Name = "test1"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "test2"
            }
        };
        await Fixture.AddTask(tasks.ToArray());

        var deleteRes = await HttpClient.DeleteAsync($"api/Tasks/Remove/{tasks.First().Id}");
        deleteRes.EnsureSuccessStatusCode();
        var actualTasks = await HttpClient.GetAsync("api/Tasks/GetAll").Read<TaskClientDto[]>();
        Assert.Collection(actualTasks,
            x =>
            {
                x.Name = "test2";
            }
        );
    }

    [Fact]
    public async Task UpdateTask_ShouldUpdateTask()
    {
        var tasks = new List<TaskDbDto>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Name = "test1"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "test2"
            }
        };
        await Fixture.AddTask(tasks.ToArray());

        var patchRes = await HttpClient.PatchAsync("api/Tasks/Update", JsonContent.Create(tasks.First() with
        {
            Name = "Changed"
        }));
        patchRes.EnsureSuccessStatusCode();

        var actualTasks = await HttpClient.GetAsync("api/Tasks/GetAll").Read<TaskClientDto[]>();
        Assert.Collection(actualTasks,
            x =>
            {
                x.Name = "Changed";
            },
            x =>
            {
                x.Name = "test2";
            }
        );
    }
}