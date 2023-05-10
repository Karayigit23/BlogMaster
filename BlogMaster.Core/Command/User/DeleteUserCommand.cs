using BlogMaster.Core.Exception;
using BlogMaster.Core.InterFaces;
using MediatR;

namespace BlogMaster.Core.Command.User;

public class DeleteUserCommand:IRequest
{
    public int Id { get; set; }
}

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
{
    private readonly IUserRepository _userRepository;

    public DeleteUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var User = await _userRepository.GetUserById(request.Id);
       
        if (User == null)
        {
            throw new NotFoundException($"User with ID {request.Id} not found.");
        }
       
        await _userRepository.DeleteUser(User);
        return Unit.Value;
    }
}