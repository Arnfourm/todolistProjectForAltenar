using Microsoft.EntityFrameworkCore;
using todolistProject.Core.Models;

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
            var NotesList = await _dbContext.Notes.ToListAsync();

            return NotesList;
        }

        public async Task<int> CreateNote(Note currentNote)
        {
            await _dbContext.Notes.AddAsync(currentNote);
            await _dbContext.SaveChangesAsync();

            return currentNote.idNote;
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
