using Microsoft.AspNetCore.Mvc;
using TaskManager.Tasks.Commands;
using TaskManager.Tasks.Queries;

namespace TaskManager.Tasks.Endpoints;

/// <summary>
///     Controller for handling tasks-related operations.
/// </summary>
[Route("[controller]")]
public class TasksController : BaseController
{
    [HttpGet("[action]")]
    public async Task<IActionResult> GetAll(
        [FromServices] GetAllTasksQuery allTasksQuery,
        CancellationToken ct
    )
    {
        var tasks = await allTasksQuery.Get(ct);
        return Ok(tasks);
    }

    [HttpGet("[action]/{taskId}")]
    public async Task<IActionResult> Get(
        [FromServices] GetByIdTaskQuery getByIdTaskQuery,
        [FromRoute] Guid taskId,
        CancellationToken ct
    )
    {
        var task = await getByIdTaskQuery.Get(taskId, ct);
        return task.Match(
            onSuccess: Ok,
            onFailure: Problem
        );
    }

    [HttpPut("[action]")]
    public async Task<IActionResult> Create(
        [FromServices] CreateTaskCommand createTaskCommand,
        [FromBody] TaskClientDto taskClientDto,
        CancellationToken ct
    )
    {
        var result = await createTaskCommand.Execute(taskClientDto, ct);
        return result.Match(
            onSuccess: Ok,
            onFailure: Problem
        );
    }

    [HttpPatch("[action]")]
    public async Task<IActionResult> Update(
        [FromServices] UpdateTaskCommand updateTaskCommand,
        [FromBody] TaskClientDto taskClientDto,
        CancellationToken ct
    )
    {
        var result = await updateTaskCommand.Execute(taskClientDto, ct);
        return result.Match(
            onSuccess: Ok,
            onFailure: Problem
        );
    }

    [HttpDelete("[action]/{taskId}")]
    public async Task<IActionResult> Remove(
        [FromServices] DeleteTaskCommand deleteTaskCommand,
        [FromRoute] Guid taskId,
        CancellationToken ct
    )
    {
        var result = await deleteTaskCommand.Execute(taskId, ct);
        return result.Match(
            onSuccess: Ok,
            onFailure: Problem
        );
    }
}