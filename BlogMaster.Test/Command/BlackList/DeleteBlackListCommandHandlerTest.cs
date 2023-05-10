using BlogMaster.Core.Command.BlackList;
using BlogMaster.Core.Exception;
using BlogMaster.Core.InterFaces;
using Moq;

namespace BlogMaster.Test.Command.BlackList;

public class DeleteBlackListCommandHandlerTest
{
    private readonly Mock<IBlacklistRepository> _blacklistRepositoryMock;
    private readonly DeleteBlackListCommandHandler _handler;

        public DeleteBlackListCommandHandlerTest()
        {
            _blacklistRepositoryMock = new Mock<IBlacklistRepository>();
            _handler = new DeleteBlackListCommandHandler(_blacklistRepositoryMock.Object);
        }

        [SetUp]
        public void SetUp()
        {
            _blacklistRepositoryMock.Invocations.Clear();
        }

        [Test]
        public async Task Handle_ValidId_BlacklistDeleted()
        {
            // Arrange
            var id = 1;
            var blacklist = new Core.Entity.BlackList { Id = id };
            _blacklistRepositoryMock
                .Setup(x => x.GetBlacklistById(id))
                .ReturnsAsync(blacklist);

            // Act
            await _handler.Handle(new DeleteBlackListCommand { Id = id }, CancellationToken.None);

            // Assert
            _blacklistRepositoryMock.Verify(x => x.DeleteBlaclist(blacklist), Times.Once);
        }

        [Test]
        public async Task Handle_InvalidId_ThrowsException()
        {
            // Arrange
            var id = -1;
            _blacklistRepositoryMock
                .Setup(x => x.GetBlacklistById(id))
                .ReturnsAsync((Core.Entity.BlackList)null);

            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(new DeleteBlackListCommand { Id = id }, CancellationToken.None));
        }
    }
