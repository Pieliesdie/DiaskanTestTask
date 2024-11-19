namespace TaskManager.Tasks.Models;

public record TaskDbDto
{
    public Guid Id { get; set; } = Guid.NewGuid();

    // Наименование
    public required string Name { get; set; }

    // Дата создания
    public DateTime CreationDate { get; set; }

    // Дата завершения
    public DateTime? CompletionDate { get; set; }

    // Срок выполнения(deadline)
    public DateTime Deadline { get; set; }

    // Тэги/Метки
    public List<string> Tags { get; set; } = [];

    // Категория
    public string? Category { get; set; }

    // Приоритет
    public int Priority { get; set; }

    // Комментарий/Описание
    public string? Description { get; set; }
}