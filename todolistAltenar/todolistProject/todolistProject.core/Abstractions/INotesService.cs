using todolistProject.Core.Models;

namespace todolistProject.Core.Abstractions
{
    public interface INotesService
    {
        Task<int> CreateNote(Note currentNote);
        Task<int> DeleteNote(int idNote);
        Task<List<Note>> GetAllNotes();
        Task<int> UpdateNote(int idNote, string titleNote, int groupID);
    }
}