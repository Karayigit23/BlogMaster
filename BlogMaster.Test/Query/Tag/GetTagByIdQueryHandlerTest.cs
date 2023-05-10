using BlogMaster.Core.Exception;
using BlogMaster.Core.InterFaces;
using BlogMaster.Core.Query.Tag;
using Microsoft.Extensions.Logging;
using Moq;

namespace BlogMaster.Test.Query.Tag;

public class GetTagByIdQueryHandlerTest
{
    private Mock<ITagRepository> _tagRepository;
    private Mock<ILogger<GetTagByIdQuery.GetTagByIdQueryHandler>> _logger;
    private GetTagByIdQuery.GetTagByIdQueryHandler _handler;

    public GetTagByIdQueryHandlerTest()
    {
        _tagRepository = new Mock<ITagRepository>();
        _logger = new Mock<ILogger<GetTagByIdQuery.GetTagByIdQueryHandler>>();
        _handler = new GetTagByIdQuery.GetTagByIdQueryHandler(_tagRepository.Object, _logger.Object);
    }

    [SetUp]
    public void SetUp()
    {
        _tagRepository.Invocations.Clear();
    }

    [Test]
    public async Task Handle_ReturnsTag_WhenTagExists()
    {
        // Arrange
        var tag = new Core.Entity.Tag { Id = 1, Name = "Test Tag" };
        _tagRepository.Setup(x => x.GetTagById(tag.Id)).ReturnsAsync(tag);

        // Act
        var result = await _handler.Handle(new GetTagByIdQuery { Id = tag.Id }, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.AreEqual(tag.Id, result.Id);
        Assert.AreEqual(tag.Name, result.Name);
    }

    [Test]
    public void Handle_ThrowsException_WhenTagDoesNotExist()
    {
        // Arrange
        var tagId = 1;
        _tagRepository.Setup(x => x.GetTagById(tagId)).ReturnsAsync((Core.Entity.Tag)null);

        // Assert
        var exception = Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(new GetTagByIdQuery { Id = tagId }, CancellationToken.None));
        Assert.AreEqual($"tag not found tagId: {tagId}", exception.Message);
    }
}
