using Microsoft.EntityFrameworkCore;
using todolistProject.Core.Models;
using todolistProject.dataAccess.Entities;
using todolistProject.Core.Abstractions;

namespace todolistProject.dataAccess.CRUD
{
    public class UserCRUD : IUserCRUD
    {
        private readonly todolistDbContext _dbContext;

        public UserCRUD(todolistDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<User>> GetUsers()
        {
            var usersList = await _dbContext.Users.ToListAsync();

            var userListReturn = usersList
                .Select(user => new User(user.idUser, user.username, user.userEmail, user.userPassword))
                .ToList();

            return userListReturn;
        }

        public async Task<Guid> CreateUser(User user)
        {
            var userEntity = new UserEntity
            {
                idUser = user.idUser,
                username = user.username,
                userEmail = user.userEmail,
                userPassword = user.userPassword
            };

            await _dbContext.Users.AddAsync(userEntity);
            await _dbContext.SaveChangesAsync();

            return userEntity.idUser;
        }

        public async Task<Guid> UpdateUser(Guid idUser, string username, string userEmail, string userPassword)
        {
            await _dbContext.Users
                .Where(user => user.idUser == idUser)
                .ExecuteUpdateAsync(update => update
                    .SetProperty(user => user.username, user => username)
                    .SetProperty(user => user.userEmail, user => userEmail)
                    .SetProperty(user => user.userPassword, user => userPassword));

            await _dbContext.SaveChangesAsync();

            return idUser;
        }

        public async Task<Guid> DeleteUser(Guid idUser)
        {
            await _dbContext.Users
                .Where(user => user.idUser == idUser)
                .ExecuteDeleteAsync();

            await _dbContext.SaveChangesAsync();

            return idUser;
        }
    }
}
