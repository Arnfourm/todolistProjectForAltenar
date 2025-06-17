using Microsoft.EntityFrameworkCore;
using todolistProject.Core.Models;
using todolistProject.dataAccess.Entities;

namespace todolistProject.dataAccess
{
    public class todolistDbContext : DbContext
    {
        public todolistDbContext(DbContextOptions<todolistDbContext> options)
            : base (options)
        {
        }

        public DbSet<NoteEntity> Notes { get; set; }
    }
}
