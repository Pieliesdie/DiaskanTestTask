using System.ComponentModel.DataAnnotations;

namespace TaskManager.Common.Models.Tasks;

public record TaskClientDto
{
    [Required]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [MaxLength(50)]
    public required string Name { get; set; }

    public bool IsCompleted => CompletionDate is not null;
    public DateTime CreationDate { get; set; }
    public DateTime? CompletionDate { get; set; }
    public DateTime Deadline { get; set; }
    public List<string> Tags { get; set; } = [];
    public string? Category { get; set; }
    public Priority Priority { get; set; }

    [MaxLength(150)]
    public string? Description { get; set; }
}