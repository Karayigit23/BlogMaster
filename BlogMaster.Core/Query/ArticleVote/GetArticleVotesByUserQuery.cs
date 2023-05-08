using BlogMaster.Core.InterFaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BlogMaster.Core.Query.ArticleVote;

public class GetArticleVotesByUserQuery : IRequest<List<Entity.ArticleVote>>
{
    public int UserId { get; set; }
}
public class GetUserVotesQueryHandler : IRequestHandler<GetArticleVotesByUserQuery, List<Entity.ArticleVote>> 
{
        private readonly IArticleVoteRepository _userVoteRepository;
        private readonly ILogger<GetUserVotesQueryHandler> _logger;

        public GetUserVotesQueryHandler(IArticleVoteRepository userRepository, ILogger<GetUserVotesQueryHandler>
            logger)
        {
            _userVoteRepository = userRepository;
            _logger = logger;
        }

        public async Task<List<Entity.ArticleVote>> Handle( GetArticleVotesByUserQuery request, CancellationToken cancellationToken)
        {

            _logger.LogInformation(message: $"{request.UserId} User came");
            var result = await _userVoteRepository.GetUserVotes(request.UserId);
            if (result == null)
            {

                //  throw new UserNotFoundException($"user not found userId: {request.Id}");
            }

            return result;
        }

    
}    
