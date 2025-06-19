namespace todolistProject.API.Contracts
{
    public record UsersResponse
    (
        Guid idUser,
        string username,
        string userEmail,
        string userPassword
    );
}
