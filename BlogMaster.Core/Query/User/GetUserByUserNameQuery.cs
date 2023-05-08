using BlogMaster.Core.InterFaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BlogMaster.Core.Query.User;

public class GetUserByUserNameQuery:IRequest<Entity.User>
{
    public string UserName { get; set; }
}

public class GetUserByUserNameQueryHandler : IRequestHandler<GetUserByUserNameQuery, Entity.User>
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<GetUserByIdQueryHandler> _logger;

    public GetUserByUserNameQueryHandler(IUserRepository userRepository, ILogger<GetUserByIdQueryHandler>
        logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<Entity.User> Handle(GetUserByUserNameQuery request, CancellationToken cancellationToken)
    {

        _logger.LogInformation(message: $"{request.UserName} User came");
        var result = await _userRepository.GetUserByUsername(request.UserName);
        if (result == null)
        {

            //  throw new UserNotFoundException($"user not found userId: {request.Id}");
        }

        return result;
    }
}    