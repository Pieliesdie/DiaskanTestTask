using TaskManager.Tasks.Models;

namespace TaskManager.Tasks.Mappers;

public class TaskMapper
{
    public TaskDbDto MapToDbDto(TaskClientDto taskClientDto)
    {
        return new TaskDbDto
        {
            Id = taskClientDto.Id,
            Category = taskClientDto.Category,
            CompletionDate = taskClientDto.CompletionDate,
            CreationDate = taskClientDto.CreationDate,
            Deadline = taskClientDto.Deadline,
            Description = taskClientDto.Description,
            Name = taskClientDto.Name,
            Priority = taskClientDto.Priority,
            Tags = taskClientDto.Tags
        };
    }
}