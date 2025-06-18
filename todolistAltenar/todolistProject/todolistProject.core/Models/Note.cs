namespace todolistProject.Core.Models
{
    public class Note
    {
        public Guid idNote { get; set; }
        public User user { get; set; }
        public string titleNote { get; set; }
        public NoteStorage noteStorage { get; set; }
        public Group noteGroup { get; set; }

        public Note(Guid idNote, User user, string titleNote, NoteStorage noteStorage, Group noteGroup)
        {
            this.idNote = idNote;
            this.user = user;
            this.titleNote = titleNote;
            this.noteStorage = noteStorage;
            this.noteGroup = noteGroup;
        }
    }
}
