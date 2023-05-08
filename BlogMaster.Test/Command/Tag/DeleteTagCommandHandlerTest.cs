using BlogMaster.Core.Command.Tag;
using BlogMaster.Core.InterFaces;
using Moq;

namespace BlogMaster.Test.Command.Tag;

public class DeleteTagCommandHandlerTest
{
    private Mock<ITagRepository> _tagRepository;
    private DeleteTagHandler _handler;

    public DeleteTagCommandHandlerTest()
    {
        _tagRepository = new Mock<ITagRepository>();
        _handler = new DeleteTagHandler(_tagRepository.Object);
    }

    [SetUp]
        public void Setup()
        {
           _tagRepository.Invocations.Clear();
        }

        [Test]
        public async Task Handle_WhenTagExists_DeletesTagFromRepository()
        {
           
            var tagId = 1;
            var tag = new Core.Entity.Tag { Id = tagId };
            _tagRepository.Setup(r => r.GetTagById(tagId)).ReturnsAsync(tag);

           
            await _handler.Handle(new DeleteTagCommand { Id = tagId }, CancellationToken.None);

            
            _tagRepository.Verify(r => r.DeleteTag(tag), Times.Once);
        }

        [Test]
        public void Handle_WhenTagDoesNotExist_ThrowsTagNotFoundException()
        {
          
            var tagId = 1;
            _tagRepository.Setup(r => r.GetTagById(tagId)).ReturnsAsync(null as Core.Entity.Tag);

            
            Assert.ThrowsAsync<Exception>(() => _handler.Handle(new DeleteTagCommand { Id = tagId }, CancellationToken.None));
        }
    }

