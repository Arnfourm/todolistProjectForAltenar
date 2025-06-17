using Microsoft.EntityFrameworkCore;
using todolistProject.Core.Models;

namespace todolistProject.dataAccess
{
    public class todolistDbContext : DbContext
    {
        public todolistDbContext(DbContextOptions<todolistDbContext> options)
            : base (options)
        {
        }

        public DbSet<Note> Notes { get; set; }
    }
}
