using BlogMaster.Core.Command.ArticleVote;
using BlogMaster.Core.Exception;
using BlogMaster.Core.InterFaces;
using Moq;

namespace BlogMaster.Test.Command.ArticleVote;

public class UpdateArticleVoteCommandHandlerTest
{
    private Mock<IArticleVoteRepository> _articleVoteRepositoryMock;
    private UpdateArticleVoteCommandHandler _updateArticleVoteCommandHandler;

    public UpdateArticleVoteCommandHandlerTest()
    {
        _articleVoteRepositoryMock = new Mock<IArticleVoteRepository>();
        _updateArticleVoteCommandHandler = new UpdateArticleVoteCommandHandler(_articleVoteRepositoryMock.Object);
    }

    [SetUp]
    public void Setup()
    {
       _articleVoteRepositoryMock.Invocations.Clear();
    }

    [Test]
    public async Task Handle_ValidRequest_ReturnsUpdatedVote()
    {
        // Arrange
        var voteId = 1;
        var articleId = 2;
        var userId = 3;
        var like = true;
        var dislike = false;

        var voteToUpdate = new Core.Entity.ArticleVote
        {
            Id = voteId,
            ArticleId = 1,
            UserId = 1,
            Like = false,
            Dislike = false
        };

        var updateArticleVoteCommand = new UpdateArticleVoteCommand
        {
            Id = voteId,
            ArticleId = articleId,
            UserId = userId,
            Like = like,
            Dislike = dislike
        };

        _articleVoteRepositoryMock.Setup(x => x.GetById(voteId)).ReturnsAsync(voteToUpdate);

        // Act
        var updatedVote = await _updateArticleVoteCommandHandler.Handle(updateArticleVoteCommand, CancellationToken.None);

        // Assert
        Assert.AreEqual(articleId, updatedVote.ArticleId);
        Assert.AreEqual(userId, updatedVote.UserId);
        Assert.AreEqual(like, updatedVote.Like);
        Assert.AreEqual(dislike, updatedVote.Dislike);
        _articleVoteRepositoryMock.Verify(x => x.UpdateVote(It.IsAny<Core.Entity.ArticleVote>()), Times.Once);
    }

    [Test]
    public void Handle_InvalidId_ThrowsException()
    {
        // Arrange
        var voteId = 1;
        var articleId = 2;
        var userId = 3;
        var like = true;
        var dislike = false;

        var updateArticleVoteCommand = new UpdateArticleVoteCommand
        {
            Id = voteId,
            ArticleId = articleId,
            UserId = userId,
            Like = like,
            Dislike = dislike
        };

        _articleVoteRepositoryMock.Setup(x => x.GetById(voteId)).ReturnsAsync((Core.Entity.ArticleVote)null);

        // Assert
        Assert.ThrowsAsync<NotFoundException>(() => _updateArticleVoteCommandHandler.Handle(updateArticleVoteCommand, CancellationToken.None));
    }
}

