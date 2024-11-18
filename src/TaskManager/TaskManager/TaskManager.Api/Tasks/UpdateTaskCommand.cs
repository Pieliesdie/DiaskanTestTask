using Microsoft.EntityFrameworkCore;
using TaskManager.Tasks.Mappers;
using TaskManager.Tasks.Models;

namespace TaskManager.Tasks;

[RegisterScoped]
public class UpdateTaskCommand(
    TaskContext taskContext,
    TaskMapper taskMapper,
    ILogger<CreateTaskCommand> logger
)
{
    public async Task<bool> Execute(TaskClientDto taskClientDto, CancellationToken ct = default)
    {
        var dbTask = taskContext.Tasks.FindAsync([taskClientDto.Id], cancellationToken: ct) with { };
        
        try
        {
            var isSaved = await taskContext.SaveChangesAsync(ct);
            return isSaved > 0;
        }
        catch (DbUpdateException e)
        {
            logger.LogError(e, "Exception saving to db: {ExceptionMessage}", e.Message);
            return false;
        }
    }
}