namespace todolistProject.API.Contracts
{
    public record NotesRequest
    (
        Guid userID,
        string titleNote,
        string noteContent,
        Guid groupID
    );
}
