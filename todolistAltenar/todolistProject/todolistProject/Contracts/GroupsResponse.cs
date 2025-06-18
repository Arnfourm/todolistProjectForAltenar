namespace todolistProject.API.Contracts
{
    public record GroupsResponse
    (
        Guid idGroup,
        Guid userID,
        string titleGroup
    );
}