using BlogMaster.Core.InterFaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BlogMaster.Core.Query.User;

public class GetAllUserQuery:IRequest<List<Entity.User>>
{
    public class GetAllUserQueryHandler : IRequestHandler<GetAllUserQuery, List<Entity.User>>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<GetAllUserQueryHandler> _logger;

        public GetAllUserQueryHandler(IUserRepository userRepository, ILogger<GetAllUserQueryHandler> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }



        public async Task<List<Entity.User>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(message: "All the User have came");
            return await _userRepository.GetAllUsers();

        }


    }
}