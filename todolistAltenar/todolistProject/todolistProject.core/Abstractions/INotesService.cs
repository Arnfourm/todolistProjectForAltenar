using todolistProject.Core.Models;

namespace todolistProject.Core.Abstractions
{
    public interface INotesService
    {
        Task<Guid> CreateNote(Note currentNote);
        Task<Guid> DeleteNote(Guid idNote);
        Task<List<Note>> GetAllNotes();
        Task<Note> GetNoteById(Guid idNote);
        Task<Guid> UpdateNote(Guid idNote, string titleNote, Guid groupID);
    }
}