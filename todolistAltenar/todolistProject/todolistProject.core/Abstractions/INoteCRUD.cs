using todolistProject.Core.Models;

namespace todolistProject.Core.Abstractions
{
    public interface INoteCRUD
    {
        Task<Guid> CreateNote(Note note);
        Task<Guid> DeleteNote(Guid idNote);
        Task<List<Note>> GetNotes();
        Task<Guid> UpdateNote(Guid idNote, string titleNote, Guid groupID);
    }
}