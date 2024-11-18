using System.ComponentModel.DataAnnotations;

namespace TaskManager.Tasks.Models;

public record TaskDbDto
{
    public Guid Id { get; set; } = Guid.NewGuid();

    // Наименование
    [MaxLength(100)]
    public required string Name { get; set; }

    // Дата создания
    public DateTime CreationDate { get; set; }

    // Дата завершения
    public DateTime? CompletionDate { get; set; }

    // Срок выполнения(deadline)
    public DateTime Deadline { get; set; }

    // Тэги/Метки
    [MaxLength(100)]
    public List<string> Tags { get; set; } = [];

    // Категория
    [MaxLength(100)]
    public string? Category { get; set; }

    // Приоритет
    public Priority Priority { get; set; }

    // Комментарий/Описание
    [MaxLength(150)]
    public string? Description { get; set; }
}