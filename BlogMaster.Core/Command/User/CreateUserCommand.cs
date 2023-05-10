using BlogMaster.Core.Exception;
using BlogMaster.Core.InterFaces;
using MediatR;

namespace BlogMaster.Core.Command.User;

public class CreateUserCommand:IRequest<Entity.User>
{
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}

public class CreateUserHandler : IRequestHandler<CreateUserCommand, Entity.User>
{
    private readonly IUserRepository _userRepository;

    public CreateUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;

    }

    public async Task<Entity.User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        if (await _userRepository.GetUserByUsername(request.UserName) != null)
        {
            throw new ControlException($"A user with username {request.UserName} already exists.");
        }
        var user = new Entity.User
        {
            UserName = request.UserName,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Password = request.Password
        };

        await _userRepository.AddUser(user);
        return user;
    }
}  