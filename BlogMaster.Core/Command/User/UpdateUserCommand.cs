using BlogMaster.Core.InterFaces;
using MediatR;

namespace BlogMaster.Core.Command.User;

public class UpdateUserCommand:IRequest<Entity.User>
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Entity.User>
{
    private readonly IUserRepository _userRepository;

    public UpdateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<Entity.User> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserById(request.Id);

        user.UserName = request.UserName;
        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.Email = request.Email;
        user.Password = request.Password;

        await _userRepository.UpdateUser(user);
        return user;

    }
}