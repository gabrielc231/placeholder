using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using placeholder.Models;
using placeholder.Models;

namespace placeholder.Tests.UnitTests
{
    [TestClass]
    public class FileRepositoryTests
    {
        private AppDbContext _context;
        private FileRepository _fileRepository;

        [TestInitialize]
        public void Setup()
        {
            // Configura o banco de dados InMemory
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new AppDbContext(options);

            // Inicializa o repositório com o contexto InMemory
            _fileRepository = new FileRepository(_context);
        }

        [TestMethod]
        public async Task GetAllFilesAsync_ShouldReturnAllFiles()
        {
            // Arrange - adicionar dados ao contexto InMemory
            var file1 = new FileModel { FileName = "file1.txt", FilePath = "/uploads/file1.txt" };
            var file2 = new FileModel { FileName = "file2.txt", FilePath = "/uploads/file2.txt" };

            await _fileRepository.AddFileAsync(file1);
            await _fileRepository.AddFileAsync(file2);

            // Act - recupera todos os arquivos
            var result = await _fileRepository.GetAllFilesAsync();

            // Assert - verifica se os arquivos foram retornados corretamente
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("file1.txt", result[0].FileName);
            Assert.AreEqual("file2.txt", result[1].FileName);
        }

        [TestMethod]
        public async Task GetFileByIdAsync_ShouldReturnCorrectFile()
        {
            // Arrange - adicionar dados ao contexto InMemory
            var file = new FileModel { FileName = "file1.txt", FilePath = "/uploads/file1.txt" };

            await _fileRepository.AddFileAsync(file);
            var addedFile = await _fileRepository.GetAllFilesAsync(); // Recupera o arquivo adicionado

            // Act - recupera o arquivo pelo ID
            var result = await _fileRepository.GetFileByIdAsync(addedFile[0].Id);

            // Assert - verifica se o arquivo correto foi retornado
            Assert.IsNotNull(result);
            Assert.AreEqual("file1.txt", result.FileName);
        }

        [TestMethod]
        public async Task AddFileAsync_ShouldAddFileToDatabase()
        {
            // Arrange - criar um novo arquivo
            var file = new FileModel { FileName = "file3.txt", FilePath = "/uploads/file3.txt" };

            // Act - adicionar o arquivo ao banco de dados
            await _fileRepository.AddFileAsync(file);

            // Assert - verifica se o arquivo foi adicionado com sucesso
            var result = await _fileRepository.GetAllFilesAsync();
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("file3.txt", result[0].FileName);
        }

        [TestCleanup]
        public void Cleanup()
        {
            // Limpa o banco de dados InMemory após cada teste
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
