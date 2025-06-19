using Microsoft.EntityFrameworkCore;
using todolistProject.Core.Models;
using todolistProject.dataAccess.Entities;
using todolistProject.Core.Abstractions;

namespace todolistProject.dataAccess.CRUD
{
    public class NoteStorageCRUD : INoteStorageCRUD
    {
        private readonly todolistDbContext _dbContext;

        public NoteStorageCRUD(todolistDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<NoteStorage> GetNoteStorageById(Guid noteStorageId)
        {
            var noteStorage = await _dbContext.NoteStorages.FindAsync(noteStorageId);

            if (noteStorage == null)
            {
                throw new Exception($"NoteStorage not found. ID: {noteStorageId}");
            }

            var noteStorageReturn = new NoteStorage(
                noteStorage.idNoteStorage,
                noteStorage.filenameNote,
                noteStorage.dirPathNote
            );

            return noteStorageReturn;
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
