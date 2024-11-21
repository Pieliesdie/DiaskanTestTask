using FilesHash.Db.Models;

namespace FilesHash.Models;

public static class FileHashMapper
{
    public static FileHashViewModel ToViewModel(this FileHashDbModel model)
    {
        return new FileHashViewModel
        {
            Id = model.Id,
            Hash = model.Hash,
            Filename = model.Filename,
            Type = model.Type
        };
    }
    
    public static FileHashDbModel ToDbModel(this FileHashViewModel model)
    {
        return new FileHashDbModel
        {
            Id = model.Id,
            Hash = model.Hash,
            Filename = model.Filename,
            Type = model.Type
        };
    }
}