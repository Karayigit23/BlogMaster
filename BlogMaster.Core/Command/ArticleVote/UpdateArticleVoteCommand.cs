using BlogMaster.Core.Command.User;
using BlogMaster.Core.InterFaces;
using MediatR;

namespace BlogMaster.Core.Command.ArticleVote;

public class UpdateArticleVoteCommand:IRequest<Entity.ArticleVote> 
{
    public int Id { get; set; }
    public int ArticleId { get; set; }
    public int UserId { get; set; }
    public bool Like { get; set; }
    public bool Dislike { get; set; }
}
public class UpdateArticleVoteCommandHandler : IRequestHandler<UpdateArticleVoteCommand, Entity.ArticleVote>
{
    private readonly IArticleVoteRepository _articleVoteRepository;

    public UpdateArticleVoteCommandHandler(IArticleVoteRepository articleVoteRepository)
    {
        _articleVoteRepository = articleVoteRepository;
    }
    public async Task<Entity.ArticleVote> Handle(UpdateArticleVoteCommand request, CancellationToken cancellationToken)
    {
        
        var vote = await _articleVoteRepository.GetById(request.Id);
        if (vote==null)
        {
            /// throw new exaption () hata fırlat eşleşiyosa çünkü eşleşen 
        }
        else
        {
            vote.ArticleId = request.ArticleId;
            vote.UserId = request.UserId;
            vote.Like = request.Like;
            vote.Dislike = request.Dislike;
            

        }
      
        await _articleVoteRepository.UpdateVote(vote);
        return vote;

    }
}