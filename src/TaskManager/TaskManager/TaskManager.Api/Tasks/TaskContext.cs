using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;
using TaskManager.Tasks.Models;

namespace TaskManager.Tasks;

public class TaskContext : DbContext
{
    public DbSet<TaskDbDto> Tasks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var mongoConnectionString = Environment.GetEnvironmentVariable("ConnectionStrings__MongoDb");
        if (string.IsNullOrWhiteSpace(mongoConnectionString))
        {
            throw new InvalidOperationException("MongoDB connection string not configured.");
        }

        optionsBuilder.UseMongoDB(mongoConnectionString, "TaskManager.Api");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaskDbDto>().HasKey(t => t.Id);
        modelBuilder.Entity<TaskDbDto>().ToCollection("Tasks");
    }
}