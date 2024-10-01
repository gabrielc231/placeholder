using Microsoft.EntityFrameworkCore;
using placeholder.Models;

namespace placeholder.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {

        }

        // Aqui você define os DbSets para as tabelas no banco de dados
        public DbSet<FileModel> Files { get; set; }
    }
}