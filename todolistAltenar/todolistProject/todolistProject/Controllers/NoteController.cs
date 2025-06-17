using Microsoft.AspNetCore.Mvc;
using todolist.Application.Services;
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

            var response = notes.Select(note => new NotesResponse(note.idNote, note.titleNote, note.notePath, note.titleGroup));

            return Ok(response);
        }
    }
}
