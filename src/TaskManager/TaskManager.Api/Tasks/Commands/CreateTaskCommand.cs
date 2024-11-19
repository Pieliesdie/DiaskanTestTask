using TaskManager.Common.Models.Tasks;
using TaskManager.Tasks.Errors;
using TaskManager.Tasks.Mappers;

namespace TaskManager.Tasks.Commands;

[RegisterScoped]
public class CreateTaskCommand(
    TaskContext taskContext,
    TaskMapper taskMapper,
    ILogger<CreateTaskCommand> logger
) : BaseTaskCommand(taskContext, logger)
{
    public async Task<Result> Execute(TaskClientDto taskClientDto, CancellationToken ct = default)
    {
        var isTaskExist = await TaskContext.Tasks.FindAsync([taskClientDto.Id], cancellationToken: ct) is not null;
        if (isTaskExist)
        {
            return TaskError.Conflict(taskClientDto.Id);
        }
        var dbTask = taskMapper.MapToDbDto(taskClientDto);
        dbTask.CreationDate = DateTime.UtcNow;
        try
        {
            await TaskContext.Tasks.AddAsync(dbTask, ct);
            return await SaveChangesAsync(ct);
        }
        catch (Exception e)
        {
            logger.LogError(e, e.Message);
            return TaskError.CreateFailure;
        }
    }
}