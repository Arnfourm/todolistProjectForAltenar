using todolistProject.Core.Models;

namespace todolistProject.Core.Abstractions
{
    public interface IGroupService
    {
        Task<Guid> CreateGroup(Group group);
        Task<Guid> DeleteGroup(Guid idGroup);
        Task<List<Group>> GetAllGroups();
        Task<Guid> UpdateGroup(Guid idGroup, string titleGroup);
    }
}