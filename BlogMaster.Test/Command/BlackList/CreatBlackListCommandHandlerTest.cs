using BlogMaster.Core.Command.BlackList;
using BlogMaster.Core.Exception;
using BlogMaster.Core.InterFaces;
using Moq;

namespace BlogMaster.Test.Command.BlackList;

public class CreatBlackListCommandHandlerTest
{
private readonly Mock<IBlacklistRepository> _blacklistRepositoryMock;
    private readonly CreateBlackListCommandHandler _handler;

    public CreatBlackListCommandHandlerTest()
    {
        _blacklistRepositoryMock = new Mock<IBlacklistRepository>();
        _handler = new CreateBlackListCommandHandler(_blacklistRepositoryMock.Object);
    }

    [SetUp]
    public void SetUp()
    {
        _blacklistRepositoryMock.Invocations.Clear();
    }

    [Test]
    public async Task Handle_UserNotBlacklisted_ReturnsCreatedBlackList()
    {
        // Arrange
        var articleId = 1;
        var userId = 1;
        
        _blacklistRepositoryMock.Setup(x => x.IsArticleBlacklistedForUser(It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(false);

        _blacklistRepositoryMock.Setup(x => x.AddToBlacklist(It.IsAny<Core.Entity.BlackList>()))
            .Returns((Core.Entity.BlackList blacklist) => Task.FromResult<Core.Entity.BlackList>(new Core.Entity.BlackList()
            {
                ArticleId = blacklist.ArticleId,
                UserId = blacklist.UserId,
                BlacklistDate = DateTime.Now
            }));





        var command = new CreateBlackListCommand
        {
            ArticleId = articleId,
            UserId = userId
        };

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.IsInstanceOf<Core.Entity.BlackList>(result);
        Assert.AreEqual(articleId, result.ArticleId);
        Assert.AreEqual(userId, result.UserId);

        _blacklistRepositoryMock.Verify(x => x.IsArticleBlacklistedForUser(articleId, userId), Times.Once);
        _blacklistRepositoryMock.Verify(x => x.AddToBlacklist(It.IsAny<Core.Entity.BlackList>()), Times.Once);
    }


    [Test]
    public async Task Handle_UserAlreadyBlacklisted_ThrowsException()
    {
        // Arrange
        var articleId = 1;
        var userId = 1;
        _blacklistRepositoryMock.Setup(x => x.IsArticleBlacklistedForUser(articleId, userId)).ReturnsAsync(true);

        var command = new CreateBlackListCommand
        {
            ArticleId = articleId,
            UserId = userId
        };

        // Act & Assert
        var ex = Assert.ThrowsAsync<ControlException>(() => _handler.Handle(command, CancellationToken.None));
        Assert.AreEqual("This user has already been added to this article.", ex.Message);
    }
}

