using FilesHash.Db.Models;
using Microsoft.EntityFrameworkCore;

namespace FilesHash.Db.Queries;

[RegisterTransient]
public class GetAllFilesHashQuery(IDbContextFactory<FileHashDbContext> dbContextFactory)
{
    public async Task<IEnumerable<FileHashDbModel>> Execute(CancellationToken ct = default)
    {
        await using var dbContext = await dbContextFactory.CreateDbContextAsync(ct);
        return await dbContext.FileHashes.ToListAsync(ct);
    }
}