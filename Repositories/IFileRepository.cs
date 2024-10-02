using placeholder.Models;

public interface IFileRepository
{
    Task<List<FileModel>> GetAllFilesAsync();
    Task<FileModel> GetFileByIdAsync(int id);
    Task AddFileAsync(FileModel file);
}
