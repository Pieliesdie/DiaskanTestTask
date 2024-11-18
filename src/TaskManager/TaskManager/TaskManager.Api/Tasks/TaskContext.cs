using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;
using TaskManager.Tasks.Models;

namespace TaskManager.Tasks;

[RegisterScoped]
public class TaskContext(IConfiguration configuration) : DbContext
{
    private string? mongoConnectionString;
    public DbSet<TaskDbDto> Tasks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        mongoConnectionString ??= configuration.GetConnectionString("MongoDb") 
                                ?? throw new InvalidOperationException("MongoDB connection string not configured.");
        optionsBuilder.UseMongoDB(mongoConnectionString, "TaskManagementDb");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaskDbDto>().HasKey(t => t.Id);
        modelBuilder.Entity<TaskDbDto>().ToCollection("Tasks");
    }
}