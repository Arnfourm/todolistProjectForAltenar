using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace todolistProject.dataAccess.Entities
{
    [Table("Group")]
    public class GroupEntity
    {
        [Key]
        [Column("idgroup")]
        public int idGroup { get; set; }

        [Column("userid")]
        public int userID { get; set; }

        public required UserEntity User { get; set; }

        [Column("titlegroup")]
        public required string titleGroup { get; set; }

        public ICollection<NoteEntity> notes { get; set; } = new List<NoteEntity>();
    }
}
