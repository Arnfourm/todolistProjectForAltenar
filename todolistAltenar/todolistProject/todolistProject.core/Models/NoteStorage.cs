namespace todolistProject.Core.Models
{
    public class NoteStorage
    {
        public int idNoteStorage { get; set; }
        public string filenameNote {  get; set; }
        public string dirPathNote { get; set; }

        public NoteStorage(int idNoteStorage, string filenameNote, string dirPathNote)
        {
            this.idNoteStorage = idNoteStorage;
            this.filenameNote = filenameNote;
            this.dirPathNote = dirPathNote;
        }
    }
}
