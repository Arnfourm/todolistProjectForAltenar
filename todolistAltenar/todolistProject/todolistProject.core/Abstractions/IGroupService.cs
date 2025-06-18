using todolistProject.Core.Models;

namespace todolistProject.Core.Abstractions
{
    public interface IGroupService
    {
        Task<int> CreateGroup(Group group);
        Task<int> DeleteGroup(int idGroup);
        Task<List<Group>> GetAllGroups();
        Task<int> UpdateGroup(int idGroup, string titleGroup);
    }
}