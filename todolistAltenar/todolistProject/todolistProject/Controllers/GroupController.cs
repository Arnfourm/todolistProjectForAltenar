using Microsoft.AspNetCore.Mvc;
using todolistProject.Core.Abstractions;
using todolistProject.API.Contracts;
using todolistProject.Core.Models;

namespace todolistProject.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _GroupService;
        private readonly IUserService _userService;

        public GroupController(IGroupService groupService, IUserService userService)
        {
            _GroupService = groupService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<List<GroupsResponse>>> GetGroups()
        {
            var groups = await _GroupService.GetAllGroups();

            var response = groups.Select(group => new GroupsResponse(group.idGroup, group.user.idUser, group.titleGroup));

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateGroup([FromBody] GroupsRequest request)
        {
            var allUsers = await _userService.GetAllUsers();
            var user = allUsers.FirstOrDefault(user => user.idUser == request.userID);

            if (user == null)
            {
                return NotFound();
            }

            var newGroup = new Group(
                Guid.NewGuid(),
                user,
                request.titleGroup
            );

            await _GroupService.CreateGroup(newGroup);

            return Ok(newGroup);
        }

        [HttpPut("{idGroup:guid}")]
        public async Task<ActionResult<Guid>> UpdateGroup(Guid idGroup, [FromBody] GroupsRequest request)
        {
            var groupId = await _GroupService.UpdateGroup(idGroup, request.titleGroup);

            return Ok(groupId);
        }

        [HttpDelete("{idGroup:guid}")]
        public async Task<ActionResult<Guid>> DeleteGroup(Guid idGroup)
        {
            return Ok(await _GroupService.DeleteGroup(idGroup));
        }
    }
}