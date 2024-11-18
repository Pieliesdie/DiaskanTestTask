using Microsoft.EntityFrameworkCore;
using TaskManager.Tasks.Mappers;
using TaskManager.Tasks.Models;

namespace TaskManager.Tasks;

[RegisterScoped]
public class GetAllTasksQuery(TaskContext taskContext, TaskMapper taskMapper)
{
    public async Task<IEnumerable<TaskClientDto>> GetAllTasks(CancellationToken ct = default)
    {
        var dbTasks = await taskContext.Tasks.ToListAsync(ct);
        var clientTasks = dbTasks.Select(taskMapper.MapToClientDto);
        return clientTasks;
    }
}