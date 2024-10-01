// Models/FileModel.cs
namespace placeholder.Models;
public class FileModel
{
    public int Id { get; set; }
    public string? FileName { get; set; }
    public string? FilePath { get; set; }
    public DateTime UploadDate { get; set; }
}