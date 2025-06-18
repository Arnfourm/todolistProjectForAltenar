using todolistProject.Core.Models;
using todolistProject.Core.Abstractions;

namespace todolist.Application.Services
{
    public class GroupService : IGroupService
    {
        private readonly IGroupCRUD _groupsService;

        public GroupService(IGroupCRUD groupsService)
        {
            _groupsService = groupsService;
        }

        public async Task<List<Group>> GetAllGroups()
        {
            return await _groupsService.GetGroups();
        }

        public async Task<int> CreateGroup(Group group)
        {
            return await _groupsService.CreateGroup(group);
        }

        public async Task<int> UpdateGroup(int idGroup, string titleGroup)
        {
            return await _groupsService.UpdateGroup(idGroup, titleGroup);
        }

        public async Task<int> DeleteGroup(int idGroup)
        {
            return await _groupsService.DeleteGroup(idGroup);
        }
    }
}
