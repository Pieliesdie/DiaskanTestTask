namespace TaskManager.Tasks.Models;

public record TaskClientDto
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Name { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime? CompletionDate { get; set; }
    public DateTime Deadline { get; set; }
    public List<string> Tags { get; set; } = new();
    public string? Category { get; set; }
    public Priority Priority { get; set; }
    public string? Description { get; set; }
}