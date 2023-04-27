using BlogMaster.Core.Entity;
using BlogMaster.Core.InterFaces;
using MediatR;

namespace BlogMaster.Core.Command.ArticleVote;

public class CreateArticleVoteCommand:IRequest<Entity.ArticleVote> 
{
    public int Id { get; set; }
    public int ArticleId { get; set; }
    public int UserId { get; set; }
    public bool Like { get; set; }
    public bool Dislike { get; set; }

}

public class CreateArticleVoteCommandHandler : IRequestHandler<CreateArticleVoteCommand, Entity.ArticleVote>
{
    private readonly IArticleVoteRepository _articlevoteRepository;

    public CreateArticleVoteCommandHandler(IArticleVoteRepository articlevoteRepository)
    {
        _articlevoteRepository = articlevoteRepository;

    }



    //burada hata çıkma olasılığı çok yüksek
    public async Task<Entity.ArticleVote> Handle(CreateArticleVoteCommand request, CancellationToken cancellationToken)
    {
        var exvote = _articlevoteRepository.GetById(request.Id);
        if (exvote == null)
        {
            var vote = new Entity.ArticleVote
            {
                ArticleId = request.ArticleId,
                UserId = request.UserId,
                Like = request.Like,
                Dislike = request.Dislike
            };
            await _articlevoteRepository.AddVote(vote);
            return vote;
        }
        else
        {
            throw new Exception();//var olan bir veri oluşturulamaz

        }




    }
}
