using BlogMaster.Core.InterFaces;
using BlogMaster.Core.Query.Article;
using BlogMaster.Core.Query.ArticleVote;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace BlogMaster.Test.Query.ArticleVote;

public class GetArticleVoteByIdQueryHandlerTest
{

    private Mock<IArticleVoteRepository> _articleVoteRepositoryMock;
    private Mock<ILogger<GetArticleVoteByIdQueryHandler>> _loggerMock;
    private GetArticleVoteByIdQueryHandler _getArticleVoteByIdQueryHandler;

    public GetArticleVoteByIdQueryHandlerTest()
    {
        _articleVoteRepositoryMock = new Mock<IArticleVoteRepository>();
        _loggerMock = new Mock<ILogger<GetArticleVoteByIdQueryHandler>>();
        _getArticleVoteByIdQueryHandler =
            new GetArticleVoteByIdQueryHandler(_articleVoteRepositoryMock.Object, _loggerMock.Object);
    }

    [SetUp]
    public void Setup()
    {
        _articleVoteRepositoryMock.Invocations.Clear();
    }

    [Test]
    public async Task GetArticleVoteByIdQueryHandler_ShouldReturnArticleVote_WhenArticleVoteExists()
    {
        // Arrange
        var articleVoteId = 1;
        var articleVote = new Core.Entity.ArticleVote() { Id = articleVoteId };
        _articleVoteRepositoryMock.Setup(x => x.GetById(articleVoteId)).ReturnsAsync(articleVote);

        // Act
        var query = new GetArticleVoteByIdQuery() { Id = articleVoteId };
        var result = await _getArticleVoteByIdQueryHandler.Handle(query, new CancellationToken());

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(articleVoteId);
        _articleVoteRepositoryMock.Verify(x => x.GetById(articleVoteId), Times.Once);
    }

    [Test]
    public void GetArticleVoteByIdQueryHandler_ShouldThrowArticleNotFoundException_WhenArticleVoteNotFound()
    {
        // Arrange
        var articleVoteId = 1;
        Core.Entity.ArticleVote articleVote = null;
        _articleVoteRepositoryMock.Setup(x => x.GetById(articleVoteId)).ReturnsAsync(articleVote);

        // Act
        var query = new GetArticleVoteByIdQuery() { Id = articleVoteId };
        Func<Task> action = async () => await _getArticleVoteByIdQueryHandler.Handle(query, new CancellationToken());

        // Assert
        action.Should().ThrowAsync<Exception>();
        _articleVoteRepositoryMock.Verify(x => x.GetById(articleVoteId), Times.Once);
    }

    
    [Test]
    public void GetArticleVoteByIdQueryHandler_ShouldThrowException_WhenArticleVoteNotFound()
    {
        var articleVoteId = 1;
        Core.Entity.ArticleVote articleVote = null;
        _articleVoteRepositoryMock.Setup(x => x.GetById(articleVoteId)).ReturnsAsync(articleVote);

        var query = new GetArticleVoteByIdQuery() { Id = articleVoteId };
        Func<Task> action = async () => await _getArticleVoteByIdQueryHandler.Handle(query, new CancellationToken());

        action.Should().ThrowAsync<Exception>().WithMessage($"article not found articleId: {articleVoteId}");
        _articleVoteRepositoryMock.Verify(x => x.GetById(articleVoteId), Times.Once);
    }

}
