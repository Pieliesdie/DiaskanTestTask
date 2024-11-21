namespace FilesHash.Models;

public class FileItem
{
    public required string FileName { get; init; }
    public required string FilePath { get; init; }
    
    public override bool Equals(object? obj)
    {
        return obj is FileItem item &&
            FilePath.Equals(item.FilePath, StringComparison.OrdinalIgnoreCase);
    }

    public override int GetHashCode() => FilePath.GetHashCode();
}