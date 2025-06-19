using Microsoft.AspNetCore.Mvc;
using todolistProject.Core.Abstractions;
using todolistProject.API.Contracts;
using todolistProject.Core.Models;

namespace todolistProject.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<List<UsersResponse>>> GetNotes()
        {
            var users = await _userService.GetAllUsers();

            var response = users.Select(user => new UsersResponse(user.idUser, user.username, user.userEmail, user.userPassword));

            return Ok(response);
        }

        [HttpGet("{userId:Guid}/getUserById")]
        public async Task<ActionResult<UsersResponse>> GetUserById(Guid userId)
        {
            var user = await _userService.GetUserById(userId);

            var response = new UsersResponse(user.idUser, user.username, user.userEmail, user.userPassword);

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateUser([FromBody] UsersRequest request)
        {
            var newUser = new User(
                Guid.NewGuid(),
                request.username,
                request.userEmail,
                request.userPassword
            );

            await _userService.CreateUser(newUser);

            return Ok(newUser);
        }

        [HttpPut("{idUser:guid}")]
        public async Task<ActionResult<Guid>> UpdateUser(Guid idUser, [FromBody] UsersRequest request)
        {
            var userId = await _userService.UpdateUser(idUser, request.username, request.userEmail, request.userPassword);

            return Ok(userId);
        }

        [HttpDelete("{idUser:guid}")]
        public async Task<ActionResult<Guid>> DeleteUser(Guid idUser)
        {
            return Ok(await _userService.DeleteUser(idUser));
        }
    }
}