namespace todolistProject.API.Contracts
{
    public record NotesResponse
    (
        int noteId,
        string titleNote,
        string notePath,
        string titleGroup
    );
}
