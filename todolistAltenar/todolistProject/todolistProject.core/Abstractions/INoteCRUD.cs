using todolistProject.Core.Models;

namespace todolistProject.Core.Abstractions
{
    public interface INoteCRUD
    {
        Task<int> CreateNote(Note note);
        Task<int> DeleteNote(int idNote);
        Task<List<Note>> GetNotes();
        Task<int> UpdateNote(int idNote, string titleNote, int groupID);
    }
}