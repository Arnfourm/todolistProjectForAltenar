using todolistProject.Core.Models;

namespace todolistProject.Core.Abstractions
{
    public interface INotesService
    {
        Task<Guid> CreateNote(Note currentNote);
        Task<Guid> DeleteNote(Guid idNote);
        Task<List<Note>> GetAllNotes();
        Task<Note> GetNoteById(Guid idNote);
        Task<List<Note>> GetNoteByUserId(Guid userId);
        Task<Guid> UpdateTitleNote(Guid idNote, string titleNote);
        Task<Guid> UpdateContentNote(Guid idNote, string contentNote);
    }
}