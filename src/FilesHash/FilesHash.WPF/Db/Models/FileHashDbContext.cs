using Microsoft.EntityFrameworkCore;

namespace FilesHash.Db.Models;

public sealed class FileHashDbContext : DbContext
{
    public FileHashDbContext(DbContextOptions options) : base(options)
    {
        Database.EnsureCreated();
    }
    public DbSet<FileHashDbModel> FileHashes { get; set; }
}