using Microsoft.AspNetCore.Mvc;
using todolistProject.Core.Abstractions;
using todolistProject.API.Contracts;

namespace todolistProject.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NoteController : ControllerBase
    {
        private readonly INotesService _notesService;

        public NoteController(INotesService notesService)
        {
            _notesService = notesService;
        }

        [HttpGet]
        public async Task<ActionResult<List<NotesResponse>>> GetNotes()
        {
            var notes = await _notesService.GetAllNotes();

            var response = notes.Select(note => new NotesResponse(note.idNote, note.user.idUser, note.titleNote, note.noteStorage.idNoteStorage, note.noteGroup.idGroup));

            return Ok(response);
        }
    }
}
