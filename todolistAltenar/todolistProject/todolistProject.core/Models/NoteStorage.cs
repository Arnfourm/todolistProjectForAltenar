namespace todolistProject.Core.Models
{
    public class NoteStorage
    {
        public Guid idNoteStorage { get; set; }
        public string filenameNote {  get; set; }
        public string dirPathNote { get; set; }

        public NoteStorage(Guid idNoteStorage, string filenameNote, string dirPathNote)
        {
            this.idNoteStorage = idNoteStorage;
            this.filenameNote = filenameNote;
            this.dirPathNote = dirPathNote;
        }
    }
}
