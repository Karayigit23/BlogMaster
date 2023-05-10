using BlogMaster.Core.Exception;
using BlogMaster.Core.InterFaces;
using BlogMaster.Core.Query.Tag;

using Microsoft.Extensions.Logging;
using Moq;


namespace BlogMaster.Test.Query.Tag
{
    public class GetTagsByArticleIdQueryHandlerTest
    {
        private Mock<ILogger<GetTagsByArticleIdQueryHandler>> _logger;
        private Mock<ITagRepository> _tagRepository;
        private GetTagsByArticleIdQueryHandler _handler;

        public GetTagsByArticleIdQueryHandlerTest()
        {

            _logger = new Mock<ILogger<GetTagsByArticleIdQueryHandler>>();
            _tagRepository = new Mock<ITagRepository>();
            _handler = new GetTagsByArticleIdQueryHandler(_tagRepository.Object, _logger.Object);
        }

        [SetUp]
        public void Setup()
        {
            _tagRepository.Invocations.Clear();
        }

        [Test]
        public async Task Handle_ReturnsTags_WhenTagsExistForArticleId()
        {

            int articleId = 1;
            List<Core.Entity.Tag> tags = new List<Core.Entity.Tag> { new Core.Entity.Tag(), new Core.Entity.Tag() };
            _tagRepository.Setup(x => x.GetTagsByArticleId(articleId)).ReturnsAsync((List<Core.Entity.Tag>)tags);

            var request = new GetTagsByArticleIdQuery { ArticleId = articleId };


            var result = await _handler.Handle(request, CancellationToken.None);


            Assert.IsNotNull(result);
            CollectionAssert.AreEqual(tags, result);
    
        }

        [Test]
        public async Task Handle_ThrowsException_WhenTagsDoNotExistForArticleId()
        {

            int articleId = 1;
            _tagRepository.Setup(x => x.GetTagsByArticleId(articleId)).ReturnsAsync(new List<Core.Entity.Tag>());

            var request = new GetTagsByArticleIdQuery { ArticleId = articleId };



            var exception =
                Assert.ThrowsAsync<NotFoundException>(async () => await _handler.Handle(request, CancellationToken.None));
        }
    }
}
