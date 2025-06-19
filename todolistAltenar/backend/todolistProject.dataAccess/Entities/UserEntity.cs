using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace todolistProject.dataAccess.Entities
{
    [Table("User")]
    public class UserEntity
    {
        [Key]
        [Column("iduser")]
        public Guid idUser { get; set; }

        [Column("username")]
        public required string username { get; set; }

        [Column("useremail")]
        public required string userEmail { get; set; }

        [Column("userpassword")]
        public required string userPassword { get; set; }
        
        public ICollection<NoteEntity> notes { get; set; } = new List<NoteEntity>();
        public ICollection<GroupEntity> Groups { get; set; } = new List<GroupEntity>();
    }
}
