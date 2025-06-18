namespace todolistProject.API.Contracts
{
    public record GroupsRequest
    (
        Guid userID,
        string titleGroup
    );
}
