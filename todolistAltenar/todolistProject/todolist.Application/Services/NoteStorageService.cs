using todolistProject.Core.Models;
using todolistProject.Core.Abstractions;

namespace todolist.Application.Services
{
    public class NoteStorageService : INoteStorageService
    {
        private readonly INoteStorageCRUD _noteStorageService;

        public NoteStorageService(INoteStorageCRUD noteStorageService)
        {
            _noteStorageService = noteStorageService;
        }

        public async Task<Guid> CreateNoteStorage(NoteStorage noteStorage)
        {
            string notePath = $@"{noteStorage.dirPathNote}/{noteStorage.filenameNote}.txt";
            FileInfo fileInfo = new FileInfo(notePath);

            var createStream = fileInfo.Create();
            createStream.Dispose();
           
            return await _noteStorageService.CreateNoteStorage(noteStorage);
        }

        public async Task<Guid> DeleteNoteStorage(Guid idNoteStorage)
        {
            var noteStorage = await _noteStorageService.GetNoteStorageById(idNoteStorage);

            string notePath = $@"{noteStorage.dirPathNote}/{noteStorage.filenameNote}.txt";
            FileInfo fileInfo = new FileInfo(notePath);

            if (fileInfo.Exists)
            {
                fileInfo.Delete();
            }

            return await _noteStorageService.DeleteNoteStorage(idNoteStorage);
        }

        public async Task<string> GetNoteStorageContentById(Guid idNoteStorage)
        {
            var noteStorage = await _noteStorageService.GetNoteStorageById(idNoteStorage);

            string notePath = $@"{noteStorage.dirPathNote}/{noteStorage.filenameNote}.txt";
            string fileContent = string.Empty;

            try
            {
                fileContent = await File.ReadAllTextAsync(notePath);
            }
            catch (Exception exeption)
            {
                throw new Exception($"couldn't take content: {exeption}");
            }

            return fileContent;
        }

        public async Task SaveNoteContect(Guid idNoteStorage, string contentToSave)
        {
            var noteStorage = await _noteStorageService.GetNoteStorageById(idNoteStorage);

            if (noteStorage == null)
            {
                throw new Exception("noteStorage didn't found");
            }

            string notePath = $@"{noteStorage.dirPathNote}/{noteStorage.filenameNote}.txt";

            await File.WriteAllTextAsync(notePath, contentToSave);
        }
    }
}
