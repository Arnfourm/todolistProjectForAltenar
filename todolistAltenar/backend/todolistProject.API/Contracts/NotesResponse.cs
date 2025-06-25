namespace todolistProject.API.Contracts
{
    public record NotesResponse
    (
        Guid noteID,
        Guid userID,
        string titleNote,
        string noteContent,
        Guid groupID
    );
}
