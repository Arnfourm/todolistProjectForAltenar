using todolistProject.Core.Models;

namespace todolistProject.Core.Abstractions
{
    public interface INoteStorageCRUD
    {
        Task<Guid> CreateNoteStorage(NoteStorage noteStorage);
        Task<Guid> DeleteNoteStorage(Guid idNoteStorage);
    }
}