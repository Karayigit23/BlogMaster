using BlogMaster.Core.Command.Tag;
using BlogMaster.Core.Exception;
using BlogMaster.Core.InterFaces;
using BlogMaster.Test.Command.User;
using Microsoft.Extensions.Logging;
using Moq;

namespace BlogMaster.Test.Command.Tag;

public class UpdateTagCommandHandlerTest
{
    private Mock<ITagRepository> _tagRepository;
    private Mock<ILogger<UpdateTagHandler>> _logger;
    private UpdateTagHandler _handler;

    public UpdateTagCommandHandlerTest()
    {
        _tagRepository = new Mock<ITagRepository>();
        _logger = new Mock<ILogger<UpdateTagHandler>>();
        _handler = new UpdateTagHandler(_tagRepository.Object);
    }


    [SetUp]
        public void SetUp()
        {
            _tagRepository.Invocations.Clear();
        }

        [Test]
        public async Task Handle_WhenTagExists_UpdatesTag()
        {
        
            var tag = new Core.Entity.Tag
            {
                Id = 1,
                Name = "TestTag"
            };
            var command = new UpdateTagCommand
            {
                Id = tag.Id,
                Name = "NewTestTag"
            };
            _tagRepository.Setup(x => x.GetTagById(tag.Id)).ReturnsAsync(tag);

           
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(tag.Id, result.Id);
            Assert.AreEqual(command.Name, result.Name);
            _tagRepository.Verify(x => x.UpdateTag(tag), Times.Once);
        }

        [Test]
        public void Handle_WhenTagDoesNotExist_ThrowsTagNotFoundException()
        {
            // Arrange
            var command = new UpdateTagCommand
            {
                Id = 1,
                Name = "NewTestTag"
            };
            _tagRepository.Setup(x => x.GetTagById(command.Id)).ReturnsAsync((Core.Entity.Tag)null);

            // Assert
            Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }


//tagnotfound