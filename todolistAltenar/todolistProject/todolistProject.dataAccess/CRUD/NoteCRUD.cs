using Microsoft.EntityFrameworkCore;
using todolistProject.Core.Models;
using todolistProject.dataAccess.Entities;

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
            var notesList = await _dbContext.Notes.ToListAsync();

            var notesListReturn = notesList
                .Select(note => Note.Create(note.idNote, note.titleNote, note.notePath, note.titleGroup))
                .ToList();

            return notesListReturn;
        }

        public async Task<int> CreateNote(Note note)
        {
            var noteEntity = new NoteEntity
            {
                idNote = note.idNote,
                titleNote = note.titleNote,
                notePath = note.notePath,
                titleGroup = note.titleGroup
            };

            await _dbContext.Notes.AddAsync(noteEntity);
            await _dbContext.SaveChangesAsync();

            return noteEntity.idNote;
        }

        public async Task<int> UpdateNote(int idNote, string titleNote, string titleGroup)
        {
            await _dbContext.Notes
                .Where(note => note.idNote == idNote)
                .ExecuteUpdateAsync(update => update
                    .SetProperty(note => note.titleNote, note => titleNote)
                    .SetProperty(note => note.titleGroup, note => titleGroup));

            await _dbContext.SaveChangesAsync();

            return idNote;
        }

        public async Task<int> DeleteNote(int idNote)
        {
            await _dbContext.Notes
                .Where(note => note.idNote == idNote)
                .ExecuteDeleteAsync();

            await _dbContext.SaveChangesAsync();

            return idNote;
        }
    }
}
