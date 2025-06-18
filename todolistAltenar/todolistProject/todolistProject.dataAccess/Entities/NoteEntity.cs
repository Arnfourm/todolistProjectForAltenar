using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace todolistProject.dataAccess.Entities
{
    [Table("Note")]
    public class NoteEntity
    {
        [Key]
        [Column("idnote")]
        public int idNote { get; set; }

        [Column("userid")]
        public int userID { get; set; }
        public required UserEntity user { get; set; }

        [Column("titlenote")]
        public required string titleNote { get; set; }

        [Column("notestorageid")]
        public required int noteStorageID { get; set; }
        public required NoteStorageEntity noteStorage { get; set; }

        [Column("groupid")]
        public int groupID { get; set; }
        public required GroupEntity group { get; set; }
    }
}
