using Microsoft.EntityFrameworkCore;
using todolistProject.Core.Models;
using todolistProject.dataAccess.Entities;

namespace todolistProject.dataAccess.CRUD
{
    public class NoteStorageCRUD : INoteStorageCRUD
    {
        private readonly todolistDbContext _dbContext;

        public NoteStorageCRUD(todolistDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> CreateNoteStorage(NoteStorage noteStorage)
        {
            var noteStorageEntity = new NoteStorageEntity
            {
                idNoteStorage = noteStorage.idNoteStorage,
                filenameNote = noteStorage.filenameNote,
                dirPathNote = noteStorage.dirPathNote
            };

            await _dbContext.NoteStorages.AddAsync(noteStorageEntity);
            await _dbContext.SaveChangesAsync();

            return noteStorage.idNoteStorage;
        }

        public async Task<Guid> DeleteNoteStorage(Guid idNoteStorage)
        {
            await _dbContext.NoteStorages
                .Where(noteStorage => noteStorage.idNoteStorage == idNoteStorage)
                .ExecuteDeleteAsync();

            await _dbContext.SaveChangesAsync();

            return idNoteStorage;
        }
    }
}
