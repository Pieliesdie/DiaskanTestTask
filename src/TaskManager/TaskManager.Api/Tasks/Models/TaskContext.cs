﻿using MongoDB.EntityFrameworkCore.Extensions;

namespace TaskManager.Tasks.Models;

public class TaskContext(DbContextOptions options) : DbContext(options)
{
    public virtual DbSet<TaskDbDto> Tasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaskDbDto>().HasKey(t => t.Id);
        modelBuilder.Entity<TaskDbDto>()
            .Property(e => e.Name)
            .HasMaxLength(50);
        modelBuilder.Entity<TaskDbDto>()
            .Property(e => e.Description)
            .HasMaxLength(150);
        modelBuilder.Entity<TaskDbDto>().ToCollection("Tasks");
    }
}