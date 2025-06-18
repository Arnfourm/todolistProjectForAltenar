using Microsoft.EntityFrameworkCore;
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


    }
}
