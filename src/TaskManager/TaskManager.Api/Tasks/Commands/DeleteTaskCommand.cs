using TaskManager.Tasks.Errors;

namespace TaskManager.Tasks.Commands;

[RegisterScoped]
public class DeleteTaskCommand(
    TaskContext taskContext,
    ILogger<DeleteTaskCommand> logger
) : BaseTaskCommand(taskContext, logger)
{
    public async Task<Result> Execute(Guid taskId, CancellationToken ct = default)
    {
        var dbTask = await TaskContext.Tasks.FindAsync([taskId], cancellationToken: ct);
        if (dbTask == null)
        {
            return TaskError.NotFound(taskId);
        }

        try
        {
            TaskContext.Tasks.Remove(dbTask);
            return await SaveChangesAsync(ct);
        }
        catch (Exception e)
        {
            logger.LogError(e, e.Message);
            return TaskError.DeleteFailure;
        }
    }
}