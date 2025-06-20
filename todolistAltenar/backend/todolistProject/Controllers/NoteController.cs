using Microsoft.AspNetCore.Mvc;
using todolistProject.Core.Abstractions;
using todolistProject.API.Contracts;
using todolistProject.Core.Models;

namespace todolistProject.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NoteController : ControllerBase
    {
        private readonly INotesService _notesService;
        private readonly IUserService _userService;
        private readonly IGroupService _groupService;
        private readonly INoteStorageService _noteStorageService;

        public NoteController(
            INotesService notesService,
            IUserService userService, 
            IGroupService groupService, 
            INoteStorageService noteStorageService)
        {
            _notesService = notesService;
            _userService = userService;
            _groupService = groupService;
            _noteStorageService = noteStorageService;
        }

        [HttpGet]
        public async Task<ActionResult<List<NotesResponse>>> GetNotes()
        {
            var notes = await _notesService.GetAllNotes();

            var response = notes.Select(note => new NotesResponse(
                note.idNote,
                note.user.idUser,
                note.titleNote,
                note.noteStorage.idNoteStorage,
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
                note.noteStorage.idNoteStorage,
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
                note.noteStorage.idNoteStorage,
                note.noteGroup.idGroup)
            );

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateNote([FromBody] NotesRequest request)
        {
            var user = await _userService.GetUserById(request.userID);

            var group = await _groupService.GetGroupById(request.groupID);

            var newNoteStorage = new NoteStorage(
                Guid.NewGuid(),
                Guid.NewGuid().ToString(),
                @"D:\noteStorage"
            );

            await _noteStorageService.CreateNoteStorage(newNoteStorage);

            var newNote = new Note(
                Guid.NewGuid(),
                user,
                request.titleNote,
                newNoteStorage,
                group
            );

            await _notesService.CreateNote(newNote);

            return Ok(newNote);
        }

        [HttpPut("{idNote:Guid}")]
        public async Task<ActionResult<Guid>> UpdateNote(Guid idNote, [FromBody] NotesRequest request)
        {
            var group = await _groupService.GetGroupById(request.groupID);

            var noteId = await _notesService.UpdateNote(idNote, request.titleNote, group.idGroup);

            return Ok(noteId);
        }

        [HttpDelete("{idNote:Guid}")]
        public async Task<ActionResult<Guid>> DeleteNote(Guid idNote)
        {
            var note = await _notesService.GetNoteById(idNote);

            await _notesService.DeleteNote(idNote);
            await _noteStorageService.DeleteNoteStorage(note.noteStorage.idNoteStorage);

            return Ok(idNote);
        }

        [HttpGet("Content/{idNote:Guid}")]
        public async Task<ActionResult<NotesContentResponse>> GetNoteContent(Guid idNote)
        {
            var note = await _notesService.GetNoteById(idNote);

            string noteContnt = await _noteStorageService.GetNoteStorageContentById(note.noteStorage.idNoteStorage);

            return Ok(new NotesContentResponse(idNote, noteContnt));
        }

        [HttpPut("Content/{idNote:Guid}")]
        public async Task<ActionResult<Guid>> SaveNoteContext(Guid idNote, [FromBody] NotesContentRequest request)
        {
            var note = await _notesService.GetNoteById(idNote);

            await _noteStorageService.SaveNoteContect(note.noteStorage.idNoteStorage, request.noteContent);

            return note.idNote;
        }
    }
}