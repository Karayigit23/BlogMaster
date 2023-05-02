using System.Threading.Tasks;
using BlogMaster.Core.Command.User;
using Microsoft.AspNetCore.Mvc;
using BlogMaster.Core.Entity;
using BlogMaster.Core.InterFaces;
using BlogMaster.Core.Query.User;
using MediatR;

namespace BlogMaster.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<List<User>> GetAllUsers()
        {
            var user = await _mediator.Send(new GetAllUserQuery());
            return user;
        }

        [HttpGet("{userId}")]
        public async Task<User> GetUserById(int userId)
        {
            var user = await _mediator.Send(request: new GetUserByIdQuery { Id = userId });
            return user;
        }

        [HttpGet("username/{username}")]
        public async Task<User> GetUserByUsername(string username)
        {
             var user=await _mediator.Send(request: new GetUserByUserNameQuery { UserName = username });
             return user;
        }

        [HttpPost]
        public async Task AddUser([FromBody] User user)
        {
            await _mediator.Send(user);
        }

        [HttpPut("{userId}")]
        public async Task UpdateUser(int userId, [FromBody] User user)
        {
            user.Id = userId;
            await _mediator.Send(user);
        }

        [HttpDelete("{userId}")]
        public async Task DeleteUser(int userId)
        {
            await _mediator.Send(new DeleteUserCommand { Id = userId });
            
        }
    }
}
