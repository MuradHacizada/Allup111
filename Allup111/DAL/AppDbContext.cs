using Allup111.Models;
using Microsoft.EntityFrameworkCore;

namespace Allup111.DAL
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }
        public DbSet<Category> Categories { get; set; }
    }
}
