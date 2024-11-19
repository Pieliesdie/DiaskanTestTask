namespace TaskManager.Tasks.Mappers;

[RegisterTransient]
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
            Priority = (int)taskClientDto.Priority,
            Tags = taskClientDto.Tags
        };
    }

    public TaskClientDto MapToClientDto(TaskDbDto taskClientDto)
    {
        return new TaskClientDto
        {
            Id = taskClientDto.Id,
            Category = taskClientDto.Category,
            CompletionDate = taskClientDto.CompletionDate,
            CreationDate = taskClientDto.CreationDate,
            Deadline = taskClientDto.Deadline,
            Description = taskClientDto.Description,
            Name = taskClientDto.Name,
            Priority = (Priority)taskClientDto.Priority,
            Tags = taskClientDto.Tags
        };
    }
}