using todolistProject.Core.Models;

namespace todolist.Application.Services
{
    public interface INotesService
    {
        Task<int> CreateNote(Note currentNote);

        Task<int> DeleteNote(int idNote);

        Task<List<Note>> GetAllNotes();

        Task<int> UpdateNote(int idNote, string titleNote, string titleGroup);
    }
}