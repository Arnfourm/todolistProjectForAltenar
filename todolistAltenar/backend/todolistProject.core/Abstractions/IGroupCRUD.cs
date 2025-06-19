using todolistProject.Core.Models;

namespace todolistProject.Core.Abstractions
{
    public interface IGroupCRUD
    {
        Task<Guid> CreateGroup(Group group);
        Task<Guid> DeleteGroup(Guid idGroup);
        Task<List<Group>> GetGroups();
        Task<Group> GetGroupById(Guid idGroup);
        Task<Guid> UpdateGroup(Guid idGroup, string titleGroup);
    }
}