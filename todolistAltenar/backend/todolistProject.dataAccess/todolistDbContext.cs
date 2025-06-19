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

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<GroupEntity> Groups { get; set; }
        public DbSet<NoteStorageEntity> NoteStorages { get; set; }
        public DbSet<NoteEntity> Notes { get; set; }
    }
}
