namespace todolistProject.API.Contracts
{
    public record NotesRequest
    (
        int userID,
        string titleNote,
        int noteStorageID,
        int groupID
    );
}
