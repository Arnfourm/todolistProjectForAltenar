namespace todolistProject.API.Contracts
{
    public record NotesTitleRequest
    (
        Guid groupId,
        string titleNote    
    );
}
