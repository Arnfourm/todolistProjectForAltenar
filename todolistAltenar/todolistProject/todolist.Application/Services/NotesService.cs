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

        public async Task<int> CreateNote(Note currentNote)
        {
            return await _notesService.CreateNote(currentNote);
        }

        public async Task<int> UpdateNote(int idNote, string titleNote, int groupID)
        {
            return await _notesService.UpdateNote(idNote, titleNote, groupID);
        }

        public async Task<int> DeleteNote(int idNote)
        {
            return await _notesService.DeleteNote(idNote);
        }
    }
}
