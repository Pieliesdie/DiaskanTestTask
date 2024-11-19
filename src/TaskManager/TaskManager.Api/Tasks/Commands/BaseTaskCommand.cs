namespace TaskManager.Tasks.Commands;

public abstract class BaseTaskCommand(TaskContext taskContext, ILogger logger)
{
    protected readonly TaskContext TaskContext = taskContext;

    protected async Task<Result> SaveChangesAsync(CancellationToken ct = default)
    {
        try
        {
            var isSaved = await TaskContext.SaveChangesAsync(ct) > 0;
            return isSaved ? Result.Success() : Result.Failure(Error.Failure("Something went wrong in saving to database"));
        }
        catch (DbUpdateException e)
        {
            logger.LogError(e, "Exception saving to db: {ExceptionMessage}", e.Message);
            return Result.Failure(Error.Failure("Something went wrong in saving to database"));
        }
    }
}