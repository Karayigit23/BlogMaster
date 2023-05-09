using BlogMaster.Core.Entity;
using BlogMaster.Core.InterFaces;
using BlogMaster.Core.Query.Comment;
using Microsoft.Extensions.Logging;
using Moq;

namespace BlogMaster.Test.Query.Comment;

public class GetCommentByArticleIdqQueryHandlerTest
{
    private Mock<ICommentRepository> _commentRepositoryMock;
    private Mock<ILogger<GetCommentsByArticleIdQueryHandler>> _loggerMock;
    private GetCommentsByArticleIdQueryHandler _handler;

    public GetCommentByArticleIdqQueryHandlerTest()
    {
        _commentRepositoryMock = new Mock<ICommentRepository>();
        _loggerMock = new Mock<ILogger<GetCommentsByArticleIdQueryHandler>>();
        _handler = new GetCommentsByArticleIdQueryHandler(_commentRepositoryMock.Object, _loggerMock.Object);
    }

    [SetUp]
    public void SetUp()
    {
        _commentRepositoryMock.Invocations.Clear();
    }

    [Test]
    public async Task Handle_ExistingArticleId_ReturnsComments()
    {
    
        int articleId = 123;
        var comments = new List<Core.Entity.Comment>()
        {
            new Core.Entity.Comment { Id = 1, ArticleId = articleId,Article = "Comment 1" },
            new Core.Entity.Comment { Id = 2, ArticleId = articleId, Article = "Comment 2" },
        };
        _commentRepositoryMock.Setup(m => m.GetCommentsByArticleId(articleId)).ReturnsAsync(comments);

    
        var result = await _handler.Handle(new GetCommentsByArticleIdQuery { ArticleId = articleId }, CancellationToken.None);

       
        Assert.IsNotNull(result);
        Assert.IsInstanceOf<List<Core.Entity.Comment>>(result);
        Assert.AreEqual(2, result.Count);
        Assert.AreEqual(comments[0].Id, result[0].Id);
        Assert.AreEqual(comments[0].ArticleId, result[0].ArticleId);
        Assert.AreEqual(comments[0].Article, result[0].Article);
        Assert.AreEqual(comments[1].Id, result[1].Id);
        Assert.AreEqual(comments[1].ArticleId, result[1].ArticleId);
        Assert.AreEqual(comments[1].Article, result[1].Article);
        
    }

    [Test]
    public void Handle_NonExistingArticleId_ThrowsException()
    {
        // Arrange
        int articleId = 456;
        _commentRepositoryMock.Setup(m => m.GetCommentsByArticleId(articleId)).ReturnsAsync((List<Core.Entity.Comment>)null);

        // Act + Assert
        Assert.ThrowsAsync<Exception>(async () => await _handler.Handle(new GetCommentsByArticleIdQuery { ArticleId = articleId }, CancellationToken.None));
    }

    [Test]
    public void Handle_NoCommentsForArticleId_ThrowsException()
    {
        // Arrange
        int articleId = 789;
        var comments = new List<Core.Entity.Comment>();
        _commentRepositoryMock.Setup(m => m.GetCommentsByArticleId(articleId)).ReturnsAsync(comments);

        // Act + Assert
        Assert.ThrowsAsync<Exception>(async () => await _handler.Handle(new GetCommentsByArticleIdQuery { ArticleId = articleId }, CancellationToken.None));
    }
}

