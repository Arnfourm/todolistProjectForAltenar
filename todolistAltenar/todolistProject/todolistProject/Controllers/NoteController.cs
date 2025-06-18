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

        public NoteController(INotesService notesService, IUserService userService, IGroupService groupService)
        {
            _notesService = notesService;
            _userService = userService;
            _groupService = groupService;
        }

        [HttpGet]
        public async Task<ActionResult<List<NotesResponse>>> GetNotes()
        {
            var notes = await _notesService.GetAllNotes();

            var response = notes.Select(note => new NotesResponse(note.idNote, note.user.idUser, note.titleNote, note.noteStorage.idNoteStorage, note.noteGroup.idGroup));

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateNote([FromBody] NotesRequest request)
        {
            var allUsers = await _userService.GetAllUsers();
            var user = allUsers.FirstOrDefault(user => user.idUser == request.userID);

            if (user == null) {
                return NotFound();
            }

            var allGroups = await _groupService.GetAllGroups();
            var group = allGroups.FirstOrDefault(group => group.idGroup == request.groupID);

            if (group == null) {
                return NotFound();
            }

            var noteStorage = new NoteStorage(
                1,
                "123213213123",
                "asdsadasd"
            );

            var newNote = new Note(
                1000,
                user,
                request.titleNote,
                noteStorage,
                group
            );

            await _notesService.CreateNote(newNote);

            return Ok(newNote);
        }
    }
}
