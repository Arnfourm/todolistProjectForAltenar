using Microsoft.EntityFrameworkCore;
using todolistProject.Core.Models;
using todolistProject.dataAccess.Entities;
using todolistProject.Core.Abstractions;

namespace todolistProject.dataAccess.CRUD
{
    public class NoteCRUD : INoteCRUD
    {
        private readonly todolistDbContext _dbContext;

        public NoteCRUD(todolistDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Note>> GetNotes()
        {
            var notesList = await _dbContext.Notes
                .Include(note => note.user)
                .Include(note => note.noteStorage)
                .Include(note => note.group)
                .ToListAsync();

            var notesListReturn = notesList
                .Select(note => new Note(
                    note.idNote,
                    new User(note.user.idUser, note.user.username, note.user.userEmail, note.user.userPassword),
                    note.titleNote,
                    new NoteStorage(note.noteStorage.idNoteStorage, note.noteStorage.filenameNote, note.noteStorage.dirPathNote),
                    new Group(
                        note.group.idGroup,
                        new User(note.user.idUser, note.user.username, note.user.userEmail, note.user.userPassword),
                        note.group.titleGroup
                    )))
                .ToList();

            return notesListReturn;
        }

        public async Task<Note> GetNoteById(Guid idNote)
        {
            var note = await _dbContext.Notes
                .Include(note => note.user)
                .Include(note => note.noteStorage)
                .Include(note => note.group)
                .FirstOrDefaultAsync(note => note.idNote == idNote);


            if (note == null)
            {
                throw new Exception($"Note not found. ID: {idNote}");
            }

            var noteToReturn = new Note(
                note.idNote,
                new User(note.user.idUser, note.user.username, note.user.userEmail, note.user.userPassword),
                note.titleNote,
                new NoteStorage(note.noteStorage.idNoteStorage, note.noteStorage.filenameNote, note.noteStorage.dirPathNote),
                new Group(
                    note.group.idGroup,
                    new User(note.user.idUser, note.user.username, note.user.userEmail, note.user.userPassword),
                    note.group.titleGroup
                )
            );

            return noteToReturn;
        }

        public async Task<List<Note>> GetNoteByUserId(Guid userId)
        {
            var notesList = await _dbContext.Notes
                .Include(note => note.user)
                .Include(note => note.noteStorage)
                .Include(note => note.group)
                .Where(note => note.idNote == userId)
                .ToListAsync();

            var notesListReturn = notesList
                .Select(note => new Note(
                    note.idNote,
                    new User(note.user.idUser, note.user.username, note.user.userEmail, note.user.userPassword),
                    note.titleNote,
                    new NoteStorage(note.noteStorage.idNoteStorage, note.noteStorage.filenameNote, note.noteStorage.dirPathNote),
                    new Group(
                        note.group.idGroup,
                        new User(note.user.idUser, note.user.username, note.user.userEmail, note.user.userPassword),
                        note.group.titleGroup
                    )))
                .ToList();

            return notesListReturn;
        }

        public async Task<Guid> CreateNote(Note note)
        {
            var userEntity = await _dbContext.Users.FindAsync(note.user.idUser);
            var noteStorageEntity = await _dbContext.NoteStorages.FindAsync(note.noteStorage.idNoteStorage);
            var groupEntity = await _dbContext.Groups.FindAsync(note.noteGroup.idGroup);

            if (userEntity == null)
            {
                throw new Exception($"User not found. ID: {note.user.idUser}");
            }

            if (noteStorageEntity == null)
            {
                throw new Exception($"NoteStorage not found. ID: {note.noteStorage.idNoteStorage}");
            }

            if (groupEntity == null)
            {
                throw new Exception($"Group not found. ID: {note.noteGroup.idGroup}");
            }

            var noteEntity = new NoteEntity
            {
                idNote = note.idNote,
                userID = note.user.idUser,
                titleNote = note.titleNote,
                noteStorageID = note.noteStorage.idNoteStorage,
                groupID = note.noteGroup.idGroup,
                user = userEntity,
                noteStorage = noteStorageEntity,
                group = groupEntity
            };

            await _dbContext.Notes.AddAsync(noteEntity);
            await _dbContext.SaveChangesAsync();

            return noteEntity.idNote;
        }

        public async Task<Guid> UpdateNote(Guid idNote, string titleNote, Guid groupID)
        {
            await _dbContext.Notes
                .Where(note => note.idNote == idNote)
                .ExecuteUpdateAsync(update => update
                    .SetProperty(note => note.titleNote, note => titleNote)
                    .SetProperty(note => note.groupID, note => groupID));

            await _dbContext.SaveChangesAsync();

            return idNote;
        }

        public async Task<Guid> DeleteNote(Guid idNote)
        {
            await _dbContext.Notes
                .Where(note => note.idNote == idNote)
                .ExecuteDeleteAsync();

            await _dbContext.SaveChangesAsync();

            return idNote;
        }
    }
}
