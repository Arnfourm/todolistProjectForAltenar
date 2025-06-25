using todolistProject.Core.Models;

namespace todolistProject.Core.Abstractions
{
    public interface INoteCRUD
    {
        Task<Guid> CreateNote(Note note);
        Task<Guid> DeleteNote(Guid idNote);
        Task<List<Note>> GetNotes();
        Task<Note> GetNoteById(Guid idNote);
        Task<List<Note>> GetNoteByUserId(Guid userId);
        Task<Guid> UpdateTitleNote(Guid idNote, string titleNote);
        Task<Guid> UpdateContentNote(Guid idNote, string contentNote);
    }
}