using TaskManager.Tasks.Errors;
using TaskManager.Tasks.Mappers;

namespace TaskManager.Tasks.Commands;

[RegisterScoped]
public class UpdateTaskCommand(
    TaskContext taskContext,
    TaskMapper taskMapper,
    ILogger<UpdateTaskCommand> logger
) : BaseTaskCommand(taskContext, logger)
{
    public async Task<Result> Execute(TaskClientDto taskClientDto, CancellationToken ct = default)
    {
        var updateTask = taskMapper.MapToDbDto(taskClientDto);

        var dbTask = await TaskContext.Tasks.FindAsync([updateTask.Id], cancellationToken: ct);
        if (dbTask == null)
        {
            return TaskError.NotFound(updateTask.Id);
        }

        dbTask.Name = updateTask.Name;
        dbTask.Description = updateTask.Description;
        dbTask.Priority = updateTask.Priority;
        dbTask.Category = updateTask.Category;
        dbTask.CompletionDate = updateTask.CompletionDate;
        dbTask.Deadline = updateTask.Deadline;
        dbTask.Tags = updateTask.Tags;

        try
        {
            TaskContext.Update(dbTask);
            return await SaveChangesAsync(ct);
        }
        catch (Exception e)
        {
            logger.LogError(e, e.Message);
            return TaskError.UpdateFailure;
        }
    }
}