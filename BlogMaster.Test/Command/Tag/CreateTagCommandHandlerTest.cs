using BlogMaster.Core.Command.Tag;
using BlogMaster.Core.Entity;
using BlogMaster.Core.InterFaces;
using BlogMaster.Test.Command.User;
using Moq;

namespace BlogMaster.Test.Command.Tag;

public class CreateTagCommandHandlerTest
{
    private Mock<ITagRepository> _tagRepository;
    private CreateTagHandler _handler;

    public CreateTagCommandHandlerTest()
    {
        _tagRepository = new Mock<ITagRepository>();
        _handler = new CreateTagHandler(_tagRepository.Object);

    }


    [SetUp]
        public void SetUp()
        {
            _tagRepository.Invocations.Clear();
        }

        [Test]
        public async Task Handle_WithValidRequest_ReturnsCreatedTag()
        {
           
            var request = new CreateTagCommand
            {
                Id = 1,
                Name = "Test Tag",
                ArticleId = 1
            };
            var expectedTag = new Core.Entity.Tag
            {
                Id = 1,
                Name = "Test Tag",
                ArticleTags = new List<ArticleTag> { new ArticleTag { ArticleId = 1 } }
            };
            _tagRepository.Setup(x => x.AddTag(It.IsAny<Core.Entity.Tag>())).ReturnsAsync(expectedTag);

          
            var result = await _handler.Handle(request, CancellationToken.None);

        
            Assert.AreEqual(expectedTag.Id, result.Id);
            Assert.AreEqual(expectedTag.Name, result.Name);
            Assert.AreEqual(expectedTag.ArticleTags.Count, result.ArticleTags.Count);
            Assert.AreEqual(expectedTag.ArticleTags.First().ArticleId, result.ArticleTags.First().ArticleId);

            _tagRepository.Verify(x => x.AddTag(It.IsAny<Core.Entity.Tag>()), Times.Once);
        }
    }

