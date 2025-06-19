using Microsoft.EntityFrameworkCore;
using todolistProject.Core.Models;
using todolistProject.dataAccess.Entities;
using todolistProject.Core.Abstractions;

namespace todolistProject.dataAccess.CRUD
{
    public class GroupCRUD : IGroupCRUD
    {
        private readonly todolistDbContext _dbContext;

        public GroupCRUD(todolistDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Group>> GetGroups()
        {
            var groupsList = await _dbContext.Groups
                .Include(group => group.user)
                .ToListAsync();

            var groupListReturn = groupsList
                .Select(group => new Group(
                    group.idGroup,
                    new User(group.user.idUser, group.user.username, group.user.userEmail, group.user.userPassword),
                    group.titleGroup))
                .ToList();

            return groupListReturn;
        }

        public async Task<Group> GetGroupById(Guid idGroup)
        {
            var group = await _dbContext.Groups
                .Include(group => group.user)
                .SingleOrDefaultAsync(group => group.idGroup == idGroup);

            if (group == null) {
                throw new Exception($"Group not found. ID: {idGroup}");
            }

            var groupReturn = new Group(
                group.idGroup,
                new User(group.user.idUser, group.user.username, group.user.userEmail, group.user.userPassword),
                group.titleGroup
            );

            return groupReturn;
        }

        public async Task<List<Group>> GetGroupByUserId(Guid userId)
        {
            var groupsList = await _dbContext.Groups
                .Include (group => group.user)
                .Where(group => group.userID == userId)
                .ToListAsync();

            var groupListReturn = groupsList
                .Select(group => new Group(
                    group.idGroup,
                    new User(group.user.idUser, group.user.username, group.user.userEmail, group.user.userPassword),
                    group.titleGroup))
                .ToList();

            return groupListReturn;
        }

        public async Task<Guid> CreateGroup(Group group)
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
                user = userEntity
            };

            await _dbContext.Groups.AddAsync(groupEntity);
            await _dbContext.SaveChangesAsync();

            return groupEntity.idGroup;
        }

        public async Task<Guid> UpdateGroup(Guid idGroup, string titleGroup)
        {
            await _dbContext.Groups
                .Where(group => group.idGroup == idGroup)
                .ExecuteUpdateAsync(update => update
                    .SetProperty(group => group.titleGroup, group => titleGroup));

            await _dbContext.SaveChangesAsync();

            return idGroup;
        }

        public async Task<Guid> DeleteGroup(Guid idGroup)
        {
            await _dbContext.Groups
                .Where(group => group.idGroup == idGroup)
                .ExecuteDeleteAsync();

            await _dbContext.SaveChangesAsync();

            return idGroup;
        }
    }
}
