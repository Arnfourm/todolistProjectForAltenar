namespace todolistProject.Core.Models
{
    public class Note
    {
        public Guid idNote { get; set; }
        public User user { get; set; }
        public string titleNote { get; set; }
        public string noteContent { get; set; }
        public Group noteGroup { get; set; }
        
        public Note(Guid idNote, User user, string titleNote, string noteContent, Group noteGroup)
        {
            this.idNote = idNote;
            this.user = user;
            this.titleNote = titleNote;
            this.noteContent = noteContent;
            this.noteGroup = noteGroup;
        }
    }
}
