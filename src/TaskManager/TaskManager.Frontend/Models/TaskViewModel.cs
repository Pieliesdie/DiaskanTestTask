using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using TaskManager.Common.Models.Tasks;

namespace TaskManager.Frontend.Models;

public partial class TaskViewModel : ObservableValidator
{
    [Required]
    [ObservableProperty]
    private Guid id = Guid.NewGuid();
    [Required]
    [MaxLength(50)]
    [ObservableProperty]
    private string name;
    private bool isCompleted;
    public bool IsCompleted
    {
        get => isCompleted;
        set
        {
            SetProperty(ref isCompleted, value);
            CompletionDate = value ? DateTime.Now : null;
        }
    }

    [ObservableProperty]
    private DateTime creationDate;
    [ObservableProperty]
    private DateTime? completionDate;
    [ObservableProperty]
    private DateTime deadline;
    [ObservableProperty]
    private string? newTag;
    [ObservableProperty]
    private List<string> tags = [];
    [ObservableProperty]
    private string? category;
    [ObservableProperty]
    private Priority priority;
    [MaxLength(150)]
    [ObservableProperty]
    private string? description;
}