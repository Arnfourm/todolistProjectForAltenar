namespace todolistProject.API.Contracts
{
    public record NotesContentResponse
    (
        Guid noteId,
        string noteContent
    );
}
