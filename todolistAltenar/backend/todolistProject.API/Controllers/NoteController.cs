using Microsoft.AspNetCore.Mvc;
using todolistProject.Core.Abstractions;
using todolistProject.API.Contracts;
using todolistProject.Core.Models;
using Asp.Versioning;

namespace todolistProject.API.Controllers
{
    [ApiVersion(1.0)]
    [ApiController]
    [Route("api/[controller]")]
    public class NoteController : ControllerBase
    {
        private readonly INotesService _notesService;
        private readonly IUserService _userService;
        private readonly IGroupService _groupService;

        public NoteController(
            INotesService notesService,
            IUserService userService, 
            IGroupService groupService)
        {
            _notesService = notesService;
            _userService = userService;
            _groupService = groupService;
        }

        [HttpGet]
        public async Task<ActionResult<List<NotesResponse>>> GetNotes()
        {
            var notes = await _notesService.GetAllNotes();

            var response = notes.Select(note => new NotesResponse(
                note.idNote,
                note.user.idUser,
                note.titleNote,
                note.noteContent,
                note.noteGroup.idGroup)
            );

            return Ok(response);
        }

        [HttpGet("ById/{idNote:Guid}")]
        public async Task<ActionResult<NotesResponse>> GetNoteById(Guid idNote)
        {
            var note = await _notesService.GetNoteById(idNote);

            var responce = new NotesResponse(
                note.idNote,
                note.user.idUser,
                note.titleNote,
                note.noteContent,
                note.noteGroup.idGroup
            );

            return Ok(responce);
        }

        [HttpGet("ByUserId/{idUser:Guid}")]
        public async Task<ActionResult<List<Note>>> GetNoteByUserId(Guid idUser)
        {
            var notes = await _notesService.GetAllNotes();

            var response = notes.Select(note => new NotesResponse(
                note.idNote,
                note.user.idUser,
                note.titleNote,
                note.noteContent,
                note.noteGroup.idGroup)
            );

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateNote([FromBody] NotesRequest request)
        {
            var user = await _userService.GetUserById(request.userID);

            var group = await _groupService.GetGroupById(request.groupID);

            var newNote = new Note(
                Guid.NewGuid(),
                user,
                request.titleNote,
                "",
                group
            );

            await _notesService.CreateNote(newNote);

            return Ok(newNote);
        }

        [HttpPut("{idNote:Guid}")]
        public async Task<ActionResult<Guid>> UpdateTitleNote(Guid idNote, [FromBody] NotesTitleRequest request)
        {
            //Добавить смену группы таска
            var group = await _groupService.GetGroupById(request.groupId);

            var noteId = await _notesService.UpdateTitleNote(idNote, request.titleNote);

            return Ok(noteId);
        }

        [HttpDelete("{idNote:Guid}")]
        public async Task<ActionResult<Guid>> DeleteNote(Guid idNote)
        {
            var note = await _notesService.GetNoteById(idNote);

            await _notesService.DeleteNote(idNote);

            return Ok(idNote);
        }

        [HttpPut("Content/{idNote:Guid}")]
        public async Task<ActionResult<Guid>> SaveNoteContext(Guid idNote, [FromBody] NotesContentRequest request)
        {
            var note = await _notesService.GetNoteById(idNote);

            await _notesService.UpdateContentNote(idNote, request.noteContent);

            return note.idNote;
        }
    }
}