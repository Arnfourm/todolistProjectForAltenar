using todolistProject.Core.Models;

namespace todolistProject.Core.Abstractions
{
    public interface IGroupCRUD
    {
        Task<int> CreateGroup(Group group);
        Task<int> DeleteGroup(int idGroup);
        Task<List<Group>> GetGroups();
        Task<int> UpdateGroup(int idGroup, string titleGroup);
    }
}