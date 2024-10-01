using Microsoft.EntityFrameworkCore;
using placeholder.Models;

public class FileRepository
{
    private readonly AppDbContext _context;

    public FileRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<FileModel>> GetAllFilesAsync()
    {
        return await _context.Files.ToListAsync();
    }

    public async Task<FileModel> GetFileByIdAsync(int id)
    {
        return await _context.Files.FindAsync(id);
    }
    // Add other repository methods here...

    public async Task AddFileAsync(FileModel file)
    {
        await _context.Files.AddAsync(file); // Método assíncrono para adicionar arquivos
        await _context.SaveChangesAsync();   // Salva as mudanças no banco de dados
    }

    
}