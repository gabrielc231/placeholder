using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using placeholder.Models;
using System.Threading.Tasks;

[TestClass]
public class FileServiceTests
{
    private FileService _fileService;
    private AppDbContext _dbContext;
    private FileRepository _fileRepository;

    [TestInitialize]
    public void Setup()
    {
        // Configura o AppDbContext para usar o banco de dados em memória
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "FileServiceTestDb")
            .Options;

        _dbContext = new AppDbContext(options);

        // Inicializa o FileRepository com o contexto
        _fileRepository = new FileRepository(_dbContext);

        // Inicializa o FileService com o repositório
        _fileService = new FileService(_fileRepository);
    }

    [TestMethod]
    public async Task AddFileAsync_ShouldAddFileToDatabase()
    {
        // Arrange - cria um novo arquivo
        var newFile = new FileModel { Id = 1, FileName = "testfile.txt", FilePath = "/uploads/testfile.txt" };

        // Act - adiciona o arquivo ao serviço
        await _fileService.AddFileAsync(newFile);

        // Assert - verifica se o arquivo foi salvo no banco
        var files = await _fileService.GetAllFilesAsync();
        Assert.AreEqual(1, files.Count);
        Assert.AreEqual("testfile.txt", files[0].FileName);
    }

    [TestMethod]
    public async Task GetFileByIdAsync_ShouldReturnCorrectFile()
    {
        // Arrange - adiciona um arquivo ao banco
        var newFile = new FileModel { Id = 1, FileName = "file1.txt", FilePath = "/uploads/file1.txt" };
        await _fileService.AddFileAsync(newFile);

        // Act - busca o arquivo pelo ID
        var result = await _fileService.GetFileByIdAsync(1);

        // Assert - verifica se o arquivo retornado é o correto
        Assert.IsNotNull(result);
        Assert.AreEqual("file1.txt", result.FileName);
    }

    [TestMethod]
    public async Task GetAllFilesAsync_ShouldReturnAllFiles()
    {
        // Arrange - adiciona alguns arquivos ao banco
        var file1 = new FileModel { Id = 1, FileName = "file1.txt", FilePath = "/uploads/file1.txt" };
        var file2 = new FileModel { Id = 2, FileName = "file2.txt", FilePath = "/uploads/file2.txt" };
        await _fileService.AddFileAsync(file1);
        await _fileService.AddFileAsync(file2);

        // Act - busca todos os arquivos
        var result = await _fileService.GetAllFilesAsync();

        // Assert - verifica se todos os arquivos foram retornados
        Assert.AreEqual(2, result.Count);
        Assert.AreEqual("file1.txt", result[0].FileName);
        Assert.AreEqual("file2.txt", result[1].FileName);
    }

    [TestMethod]
    public async Task GetFileByIdAsync_ShouldReturnNull_WhenFileDoesNotExist()
    {
        // Act - tenta buscar um arquivo inexistente pelo ID
        var result = await _fileService.GetFileByIdAsync(999);

        // Assert - verifica se o resultado é null
        Assert.IsNull(result);
    }

    [TestCleanup]
    public void Cleanup()
    {
        _dbContext.Database.EnsureDeleted();  // Limpa o banco de dados em memória após cada teste
        _dbContext.Dispose();
    }
}
