using todolistProject.Core.Models;

namespace todolistProject.Core.Abstractions
{
    public interface IUserCRUD
    {
        Task<Guid> CreateUser(User user);
        Task<Guid> DeleteUser(Guid idUser);
        Task<List<User>> GetUsers();
        Task<Guid> UpdateUser(Guid idUser, string username, string userEmail, string userPassword);
    }
}