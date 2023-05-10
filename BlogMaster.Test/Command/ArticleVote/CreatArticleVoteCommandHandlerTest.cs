using BlogMaster.Core.Command.ArticleVote;
using BlogMaster.Core.InterFaces;
using Moq;

namespace BlogMaster.Test.Command.ArticleVote;

public class CreatArticleVoteCommandHandlerTest
{
    
    private Mock<IArticleVoteRepository> _articleVoteRepositoryMock;
    private CreateArticleVoteCommandHandler _handler;

    public CreatArticleVoteCommandHandlerTest()
    {
        _articleVoteRepositoryMock = new Mock<IArticleVoteRepository>();
        _handler = new CreateArticleVoteCommandHandler(_articleVoteRepositoryMock.Object);
    }

        [SetUp]
        public void Setup()
        {
            _articleVoteRepositoryMock.Invocations.Clear();
        }

        [Test]
        public async Task CreateArticleVoteCommandHandler_Should_CreateNewArticleVote()
        {
            // Arrange
            var articleVote = new Core.Entity.ArticleVote
            {
                Id = 1,
                ArticleId = 2,
                UserId = 3,
                Like = true,
                Dislike = false
            };
            var command = new CreateArticleVoteCommand
            {
                ArticleId = articleVote.ArticleId,
                UserId = articleVote.UserId,
                Like = articleVote.Like,
                Dislike = articleVote.Dislike
            };
            _articleVoteRepositoryMock.Setup(x => x.AddVote(It.IsAny<Core.Entity.ArticleVote>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _articleVoteRepositoryMock.Verify(x => x.GetById(It.IsAny<int>()), Times.Once);
        }

}