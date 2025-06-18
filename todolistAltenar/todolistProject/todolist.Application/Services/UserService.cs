using todolistProject.Core.Abstractions;
using todolistProject.Core.Models;

namespace todolist.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserCRUD _usersService;

        public UserService(IUserCRUD userCRUD)
        {
            _usersService = userCRUD;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _usersService.GetUsers();
        }

        public async Task<int> CreateUser(User user)
        {
            return await _usersService.CreateUser(user);
        }

        public async Task<int> UpdateUser(int idUser, string username, string userEmail, string userPassword)
        {
            return await _usersService.UpdateUser(idUser, username, userEmail, userPassword);
        }

        public async Task<int> DeleteUser(int idUser)
        {
            return await _usersService.DeleteUser(idUser);
        }
    }
}
