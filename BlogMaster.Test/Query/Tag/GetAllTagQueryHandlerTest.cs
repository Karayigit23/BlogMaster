using BlogMaster.Core.InterFaces;
using BlogMaster.Core.Query.Tag;
using Microsoft.Extensions.Logging;
using Moq;

namespace BlogMaster.Test.Query.Tag;

public class GetAllTagQueryHandlerTest
{
    private Mock<ITagRepository> _tagRepository;
    private Mock<ILogger<GetAllTagQuery.GetAllTagQueryHandler>> _logger;
    private GetAllTagQuery.GetAllTagQueryHandler _handler;

    public GetAllTagQueryHandlerTest()
    {
        _tagRepository = new Mock<ITagRepository>();
        _logger = new Mock<ILogger<GetAllTagQuery.GetAllTagQueryHandler>>();
        _handler = new GetAllTagQuery.GetAllTagQueryHandler(_tagRepository.Object, _logger.Object);
        
    }

    [SetUp]
        public void SetUp()
        {
           _tagRepository.Invocations.Clear();
        }

        [Test]
        public async Task handle_when_called_When_returns_AllTags()
        {
           
            var tags = new List<Core.Entity.Tag>
            {
                new Core.Entity.Tag { Id = 1, Name = "Tag 1" },
                new Core.Entity.Tag { Id = 2, Name = "Tag 2" },
                new Core.Entity.Tag { Id = 3, Name = "Tag 3" }
            };
            _tagRepository.Setup(x => x.GetAllTags()).ReturnsAsync(tags);

          
            var result = await _handler.Handle(new GetAllTagQuery(), CancellationToken.None);

           
            Assert.IsNotNull(result);
            Assert.AreEqual(tags.Count, result.Count);
            for (int i = 0; i < tags.Count; i++)
            {
                Assert.AreEqual(tags[i].Id, result[i].Id);
                Assert.AreEqual(tags[i].Name, result[i].Name);
            }
        }

        

   
     
    }

