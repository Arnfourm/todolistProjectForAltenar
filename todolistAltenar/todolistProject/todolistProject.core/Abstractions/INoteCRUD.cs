using todolistProject.Core.Models;

namespace todolistProject.dataAccess.CRUD
{
    public interface INoteCRUD
    {
        Task<int> CreateNote(Note currentNote);
        Task<int> DeleteNote(int idNote);
        Task<List<Note>> GetNotes();
        Task<int> UpdateNote(int idNote, string titleNote, string titleGroup);
    }
}