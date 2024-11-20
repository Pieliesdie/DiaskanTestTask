using TaskManager.Common.Models.Tasks;
using TaskManager.Frontend.Models;

namespace TaskManager.Frontend.Mappers;

[RegisterTransient]
public class TaskMapper
{
    public TaskViewModel ToViewModel(TaskClientDto task)
    {
        return new TaskViewModel
        {
            Id = task.Id,
            Description = task.Description,
            Priority = task.Priority,
            Category = task.Category,
            CompletionDate = task.CompletionDate,
            CreationDate = task.CreationDate,
            Deadline = task.Deadline,
            Name = task.Name,
            Tags = task.Tags
        };
    }
    
    public TaskClientDto FromViewModel(TaskViewModel task)
    {
        return new TaskClientDto
        {
            Id = task.Id,
            Description = task.Description,
            Priority = task.Priority,
            Category = task.Category,
            CompletionDate = task.CompletionDate,
            CreationDate = task.CreationDate,
            Deadline = task.Deadline,
            Name = task.Name,
            Tags = task.Tags
        };
    }
}