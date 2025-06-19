using todolistProject.Core.Models;

namespace todolistProject.Core.Abstractions
{
    public interface INoteStorageService
    {
        Task<Guid> CreateNoteStorage(NoteStorage noteStorage);
        Task<Guid> DeleteNoteStorage(Guid idNoteStorage);
        Task<string> GetNoteStorageContentById(Guid idNoteStorage);
        Task SaveNoteContect(Guid idNoteStorage, string contentToSave);
    }
}