using System.Collections.ObjectModel;
using System.IO;
using System.Security.Cryptography;
using System.Windows;
using CommunityToolkit.Mvvm.Input;
using FilesHash.Db.Commands;
using FilesHash.Db.Queries;
using FilesHash.Models;
using FilesHash.Utils;
using Microsoft.Win32;

namespace FilesHash;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly CreateFileHashCommand createFileHashCommand;
    private readonly GetAllFilesHashQuery getAllFilesHashQuery;
    private readonly CalcHashQuery hashQuery;
    public ObservableCollection<FileItem> Files { get; set; }
    public ObservableCollection<FileHashViewModel> DbFiles { get; set; } = [];
    public RelayCommand<FileItem> RemoveFileCommand { get; set; }

    public MainWindow(
        CreateFileHashCommand createFileHashCommand,
        GetAllFilesHashQuery getAllFilesHashQuery,
        CalcHashQuery hashQuery
    )
    {
        this.createFileHashCommand = createFileHashCommand;
        this.getAllFilesHashQuery = getAllFilesHashQuery;
        this.hashQuery = hashQuery;
        InitializeComponent();
        Files = [];
        RemoveFileCommand = new RelayCommand<FileItem>(RemoveFile);
        FilesDataGrid.DataContext = this;
        DbFilesGrid.DataContext = this;
    }

    private void PickFiles_Click(object sender, RoutedEventArgs e)
    {
        var openFileDialog = new OpenFileDialog
        {
            Multiselect = true,
            Title = "Select Files",
            Filter = "All Files (*.*)|*.*"
        };

        if (openFileDialog.ShowDialog() != true)
        {
            return;
        }

        foreach (var filePath in openFileDialog.FileNames)
        {
            var newFile = new FileItem { FileName = Path.GetFileName(filePath), FilePath = filePath };
            if (!Files.Contains(newFile))
            {
                Files.Add(newFile);
            }
        }
    }

    private void CalculateHash_Click(object sender, RoutedEventArgs e)
    {
        _ = CalculateHashAsync(Files.ToArray())
            .ContinueWith(t =>
            {
                _ = t.IsFaulted
                    ? MessageBox.Show($"Error:  {t.Exception?.Message}")
                    : MessageBox.Show("Hashes calced");
            });
        Files.Clear();
    }

    private async Task CalculateHashAsync(FileItem[] files)
    {
        var hashes = await Task.Run(() =>
            files
                .DistinctBy(x => x.FilePath)
                .AsParallel()
                .AsUnordered()
                .Select(x =>
                {
                    using var hashAlgorithm = MD5.Create();
                    return new FileHashViewModel
                    {
                        Type = hashAlgorithm.GetType().FullName,
                        Filename = Path.GetFileName(x.FilePath),
                        Hash = hashQuery.Get(x.FilePath, hashAlgorithm)
                    };
                })
                .ToList()
        );
        await SaveToDbAsync(hashes);
    }

    private async Task SaveToDbAsync(IEnumerable<FileHashViewModel> viewModels, CancellationToken ct = default)
    {
        await Task.Run(async () =>
        {
            var dbTasks = viewModels
                .Select(FileHashMapper.ToDbModel)
                .Select(x => createFileHashCommand.Handle(x, ct));
            await Task.WhenAll(dbTasks);
        }, ct);
    }

    private void RemoveFile(FileItem? parameter)
    {
        if (parameter == null) { return; }

        Files.Remove(parameter);
    }

    private async void RefreshButton_Click(object sender, RoutedEventArgs e)
    {
        await RefreshFileHashesAsync();
    }

    private async Task RefreshFileHashesAsync(CancellationToken ct = default)
    {
        try
        {
            var results = await Task.Run(async () => await getAllFilesHashQuery.Execute(ct), ct);
            DbFiles.Clear();
            foreach (var fileHash in results.Select(FileHashMapper.ToViewModel))
            {
                DbFiles.Add(fileHash);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Failed to load file hashes: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}