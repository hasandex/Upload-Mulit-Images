using Microsoft.EntityFrameworkCore;
using TestUploadFiles.Models;

namespace TestUploadFiles.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImages> ProductImages { get; set; }
    }
}
