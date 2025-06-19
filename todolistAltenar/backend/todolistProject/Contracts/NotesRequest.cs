namespace todolistProject.API.Contracts
{
    public record NotesRequest
    (
        Guid userID,
        string titleNote,
        Guid groupID
    );
}
