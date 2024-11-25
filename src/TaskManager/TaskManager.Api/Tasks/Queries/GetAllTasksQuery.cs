﻿using TaskManager.Common.Models.Tasks;
using TaskManager.Tasks.Mappers;

namespace TaskManager.Tasks.Queries;

[RegisterScoped]
public class GetAllTasksQuery(TaskContext taskContext, TaskMapper taskMapper)
{
    public async Task<IEnumerable<TaskClientDto>> Get(CancellationToken ct = default)
    {
        var dbTasks = await taskContext.Tasks.ToListAsync(ct);
        var clientTasks = dbTasks.Select(taskMapper.MapToClientDto);
        return clientTasks;
    }
}