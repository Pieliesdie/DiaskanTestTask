using Microsoft.AspNetCore.Mvc;
using TaskManager.Tasks.Models;

namespace TaskManager.Tasks.Endpoints;

/// <summary>
///     Controller for handling tasks-related operations.
/// </summary>
[ApiController]
[Route("[controller]")]
public class TasksController : ControllerBase
{
    [HttpGet("[action]")]
    public async Task<IActionResult> GetAll(
        [FromServices] GetAllTasksQuery allTasksQuery,
        CancellationToken ct
    )
    {
        var tasks = await allTasksQuery.GetAllTasks(ct);
        return Ok(tasks);
    }
    [HttpGet("[action]")]
    public async Task<IActionResult> Get()
    {
        return Ok();
    }
    [HttpPut("[action]")]
    public async Task<IActionResult> Create(
        [FromServices] CreateTaskCommand createTaskCommand,
        [FromBody] TaskClientDto taskClientDto,
        CancellationToken ct
    )
    {
        var isCreated = await createTaskCommand.Execute(taskClientDto, ct);
        if (isCreated)
        {
            return Ok();
        }

        return BadRequest();
    }
    [HttpPatch("[action]")]
    public async Task<IActionResult> Update()
    {
        return Ok();
    }
    [HttpDelete("[action]")]
    public async Task<IActionResult> Remove()
    {
        return Ok();
    }
}