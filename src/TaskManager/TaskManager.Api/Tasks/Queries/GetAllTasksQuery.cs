using TaskManager.Tasks.Mappers;
using TaskManager.Tasks.Models;

namespace TaskManager.Tasks.Queries;

[RegisterScoped]
public class GetAllTasksQuery(TaskContext taskContext, TaskMapper taskMapper)
{
    public async Task<IEnumerable<TaskClientDto>> Get(CancellationToken ct = default)
    {
        var dbTasks = await taskContext.Tasks.ToListAsync(ct);
        var clientTasks = dbTasks.Select(taskMapper.MapToClientDto);
        return clientTasks;
    }
}