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

        [Column("titlenote")]
        public required string titleNote { get; set; }

        [Column("notestorageid")]
        public required string notePath { get; set; }

        [Column("groupid")]
        public required string titleGroup { get; set; }
    }
}
