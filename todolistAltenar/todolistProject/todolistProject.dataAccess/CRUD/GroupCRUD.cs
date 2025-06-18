using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using todolistProject.Core.Models;
using todolistProject.dataAccess.Entities;

namespace todolistProject.dataAccess.CRUD
{
    public class GroupCRUD
    {
        private readonly todolistDbContext _dbContext;

        public GroupCRUD(todolistDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Group>> GetGroups()
        {
            var groupsList = await _dbContext.Groups
                .Include(group => group.User)
                .ToListAsync();

            var groupListReturn = groupsList
                .Select(group => new Group(
                    group.idGroup, 
                    new User(group.User.idUser, group.User.username, group.User.userEmail, group.User.userPassword),
                    group.titleGroup))
                .ToList();

            return groupListReturn;
        }

        public async Task<int> CreateGroup(Group group)
        {
            var userEntity = await _dbContext.Users.FindAsync(group.user.idUser);
            if (userEntity == null)
            {
                throw new Exception($"User not found. ID: {group.user.idUser}");
            }

            var groupEntity = new GroupEntity
            {
                idGroup = group.idGroup,
                userID = group.user.idUser,
                titleGroup = group.titleGroup,
                User = userEntity
            };

            await _dbContext.Groups.AddAsync(groupEntity);
            await _dbContext.SaveChangesAsync();

            return groupEntity.idGroup;
        }

        public async Task<int> UpdateGroup(int idGroup, string titleGroup)
        {
            await _dbContext.Groups
                .Where(group => group.idGroup == idGroup)
                .ExecuteUpdateAsync(update => update
                    .SetProperty(group => group.titleGroup, group => titleGroup);

            await _dbContext.SaveChangesAsync();

            return idGroup;
        }

        public async Task<int> DeleteGroup(int idGroup)
        {
            await _dbContext.Groups
                .Where(group => group.idGroup == idGroup)
                .ExecuteDeleteAsync();

            await _dbContext.SaveChangesAsync();

            return idGroup;
        }
    }
}
