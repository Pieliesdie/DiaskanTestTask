using MongoDB.EntityFrameworkCore.Extensions;

namespace TaskManager.Tasks.Models;

public class TaskContext : DbContext
{
    public TaskContext(DbContextOptions options) : base(options) { }
    
    public DbSet<TaskDbDto> Tasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaskDbDto>().HasKey(t => t.Id);
        modelBuilder.Entity<TaskDbDto>().ToCollection("Tasks");
    }
}