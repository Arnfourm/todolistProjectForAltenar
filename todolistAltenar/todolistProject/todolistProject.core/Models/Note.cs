namespace todolistProject.Core.Models
{
    public class Note
    {
        public int idNote { get; set; }
        public string titleNote { get; set; }
        public string notePath { get; set; }
        public string titleGroup { get; set; }

        public Note(int idNote, string titleNote, string notePath, string titleGroup)
        {
            this.idNote = idNote;
            this.titleNote = titleNote;
            this.notePath = notePath;
            this.titleGroup = titleGroup;
        }
           
        public static Note Create(int idNote, string titleNote, string notePath, string titleGroup)
        {
            return new Note(idNote, titleNote, notePath, titleGroup);
        }
    }
}
