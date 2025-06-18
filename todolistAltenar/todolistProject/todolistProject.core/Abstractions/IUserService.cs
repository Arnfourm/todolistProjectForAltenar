using todolistProject.Core.Models;

namespace todolistProject.Core.Abstractions
{
    public interface IUserService
    {
        Task<int> CreateUser(User user);
        Task<int> DeleteUser(int idUser);
        Task<List<User>> GetAllUsers();
        Task<int> UpdateUser(int idUser, string username, string userEmail, string userPassword);
    }
}