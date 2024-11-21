using CommunityToolkit.Mvvm.ComponentModel;

namespace FilesHash.Models;

public partial class FileHashViewModel : ObservableValidator
{
    [ObservableProperty]
    private Guid id = Guid.NewGuid();
    [ObservableProperty]
    private string filename;
    [ObservableProperty]
    private string? hash;
    [ObservableProperty]
    private string? type;
}