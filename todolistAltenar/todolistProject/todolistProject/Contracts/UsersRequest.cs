namespace todolistProject.API.Contracts
{
    public record UsersRequest
    (
        string username,
        string userEmail,
        string userPassword
    );
}
