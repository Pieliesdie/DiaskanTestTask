using TaskManager.Tasks.Errors;
using TaskManager.Tasks.Mappers;

namespace TaskManager.Tasks.Queries;

[RegisterScoped]
public class GetByIdTaskQuery(TaskContext taskContext, TaskMapper taskMapper)
{
    public async Task<Result<TaskClientDto>> Get(Guid taskId, CancellationToken ct = default)
    {
        var dbTask = await taskContext.Tasks.FindAsync([taskId], ct);
        if (dbTask == null)
        {
            return TaskError.NotFound(taskId);
        }

        var clientTask = taskMapper.MapToClientDto(dbTask);
        return Result<TaskClientDto>.Success(clientTask);
    }
}