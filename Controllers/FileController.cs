using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using placeholder.Models;
using Microsoft.EntityFrameworkCore;
using System;

public class FileController : Controller
{
    private readonly AppDbContext _context;

    public FileController(AppDbContext context)
    {
        _context = context;
    }

    // GET: List all files
    public async Task<IActionResult> Index()
    {
        var files = await _context.Files.ToListAsync();
        return View(files);
    }

    // POST: Upload file
    [HttpPost]
    public async Task<IActionResult> UploadFile(IFormFile file)
    {
        if (file != null && file.Length > 0)
        {
            var filePath = Path.Combine("wwwroot/uploads", file.FileName);

            // Save file to server
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Save file information to database
            var fileModel = new FileModel
            {
                FileName = file.FileName,
                FilePath = filePath,
                UploadDate = DateTime.Now
            };

            _context.Files.Add(fileModel);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction("File");
    }

    // GET: Download file
    public async Task<IActionResult> DownloadFile(int id)
    {
        var file = await _context.Files.FindAsync(id);
        if (file == null)
        {
            return NotFound();
        }

        var memory = new MemoryStream();
        using (var stream = new FileStream(file.FilePath, FileMode.Open))
        {
            await stream.CopyToAsync(memory);
        }
        memory.Position = 0;

        return File(memory, "application/octet-stream", file.FileName);
    }

    // POST: Remove file
    [HttpPost]
    public async Task<IActionResult> RemoveFile(int id)
    {
        var file = await _context.Files.FindAsync(id);
        if (file == null)
        {
            return NotFound();
        }

        // Remove file from server
        if (System.IO.File.Exists(file.FilePath))
        {
            System.IO.File.Delete(file.FilePath);
        }

        // Remove file information from database
        _context.Files.Remove(file);
        await _context.SaveChangesAsync();

        return RedirectToAction("File");
    }
}
