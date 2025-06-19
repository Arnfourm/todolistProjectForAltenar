using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace todolistProject.dataAccess.Entities
{
    [Table("NoteStorage")]
    public class NoteStorageEntity
    {
        [Key]
        [Column("idnotestorage")]
        public Guid idNoteStorage { get; set; }

        [Column("filenamenote")]
        public required string filenameNote { get; set; }

        [Column("dirpathnote")]
        public required string dirPathNote { get; set; }

        public NoteEntity? note { get; set; }
    }
}
