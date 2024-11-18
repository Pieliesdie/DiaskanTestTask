using Microsoft.AspNetCore.Mvc;

namespace TaskManager.Tasks.Endpoints;

/// <summary>
///     Controller for handling tasks-related operations.
/// </summary>
[ApiController]
[Route("[controller]")]
public class TasksController : ControllerBase
{
    [HttpGet("[action]")]
    public async Task<IActionResult> GetAll()
    {
        return Ok();
    }
    
    [HttpGet("[action]")]
    public async Task<IActionResult> Get()
    {
        return Ok();
    }
    
    [HttpPut("[action]")]
    public async Task<IActionResult> Create()
    {
        return Ok();
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
