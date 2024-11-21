namespace FilesHash.Db.Models;

public record FileHashDbModel
{
    public Guid Id { get; set; }
    public string Filename { get; set; }
    public string Hash { get; set; }
    public string Type { get; set; }
}