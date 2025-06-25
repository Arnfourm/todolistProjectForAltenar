using todolistProject.Core.Models;
using todolistProject.Core.Abstractions;

namespace todolist.Application.Services
{
    public class NotesService : INotesService
    {
        private readonly INoteCRUD _notesService;

        public NotesService(INoteCRUD notesService)
        {
            _notesService = notesService;
        }

        public async Task<List<Note>> GetAllNotes()
        {
            return await _notesService.GetNotes();
        }

        public async Task<Note> GetNoteById(Guid idNote)
        {
            return await _notesService.GetNoteById(idNote);
        }

        public async Task<List<Note>> GetNoteByUserId(Guid userId)
        {
            return await _notesService.GetNoteByUserId(userId);
        }

        public async Task<Guid> CreateNote(Note currentNote)
        {
            return await _notesService.CreateNote(currentNote);
        }

        public async Task<Guid> UpdateTitleNote(Guid idNote, string titleNote)
        {
            return await _notesService.UpdateTitleNote(idNote, titleNote);
        }

        public async Task<Guid> DeleteNote(Guid idNote)
        {
            return await _notesService.DeleteNote(idNote);
        }

        public async Task<Guid> UpdateContentNote(Guid idNote, string noteContent)
        {
            return await _notesService.UpdateContentNote(idNote, noteContent);
        }
    }
}
