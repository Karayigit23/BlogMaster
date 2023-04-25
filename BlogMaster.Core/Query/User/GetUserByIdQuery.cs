using BlogMaster.Core.InterFaces;
using BlogMaster.Core.Query.Article;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BlogMaster.Core.Query.User;

public class GetUserByIdQuery:IRequest<Entity.User>
{
 public int Id { get; set; }   
}

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Entity.User>
{
 private readonly IUserRepository _userRepository;
 private readonly Logger<GetUserByIdQueryHandler> _logger;
 public GetUserByIdQueryHandler(IUserRepository userRepository, Logger<GetUserByIdQueryHandler>
  logger)
 {
  _userRepository = userRepository;
  _logger = logger;
 }
 public async Task<Entity.User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
 {
  
  _logger.LogInformation(message:$"{request.Id} User came");
  var result = await _userRepository.GetUserById(request.Id);
  if (result==null)
  {
                
   //  throw new UserNotFoundException($"user not found userId: {request.Id}");
  }

  return result;
 }
}