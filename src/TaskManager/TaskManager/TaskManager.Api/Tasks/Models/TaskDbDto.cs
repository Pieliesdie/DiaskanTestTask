using System.ComponentModel.DataAnnotations;

namespace TaskManager.Tasks.Models;

public record TaskDbDto
{
    public Guid Id { get; init; } = Guid.NewGuid();

    // Наименование
    [MaxLength(100)]
    public required string Name { get; init; }

    // Дата создания
    public DateTime CreationDate { get; init; }

    // Дата завершения
    public DateTime? CompletionDate { get; init; }

    // Срок выполнения(deadline)
    public DateTime Deadline { get; init; }

    // Тэги/Метки
    [MaxLength(100)]
    public List<string> Tags { get; init; } = new();

    // Категория
    [MaxLength(100)]
    public string? Category { get; init; }

    // Приоритет
    public Priority Priority { get; set; }

    // Комментарий/Описание
    [MaxLength(150)]
    public string? Description { get; init; }
}