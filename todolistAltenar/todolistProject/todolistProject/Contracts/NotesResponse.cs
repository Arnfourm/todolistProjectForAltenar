namespace todolistProject.API.Contracts
{
    public record NotesResponse
    (
        int noteID,
        int userID,
        string titleNote,
        int noteStorageID,
        int groupID
    );
}
