using FilesHash.Db.Models;
using Microsoft.EntityFrameworkCore;

namespace FilesHash.Db.Commands;

[RegisterTransient]
public class CreateFileHashCommand(IDbContextFactory<FileHashDbContext> dbContextFactory)
{
    public async Task<bool> Handle(FileHashDbModel model, CancellationToken ct = default)
    {
        await using var dbContext = await dbContextFactory.CreateDbContextAsync(ct);
        var isTaskExist = await dbContext.FileHashes.FindAsync([model.Id], cancellationToken: ct) is not null;
        if (isTaskExist)
        {
            return false;
        }
        try
        {
            await dbContext.FileHashes.AddAsync(model, ct);
            var isSaved = await dbContext.SaveChangesAsync(ct) > 0;
            return isSaved;
        }
        catch
        {
            return false;
        }
    }
}