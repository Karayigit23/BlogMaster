using BlogMaster.Core.InterFaces;
using BlogMaster.Core.Query.Article;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace BlogMaster.Test.Command.Article;

public class SearchArticleQueryHandlerTest
{
    private Mock<IArticleRepository> _articleRepositoryMock;
    private Mock<ILogger<SearchArticleQuery.SearchArticleQueryHandler>> _loggerMock;
    private SearchArticleQuery.SearchArticleQueryHandler _searchArticleQueryHandler;

    public SearchArticleQueryHandlerTest()
    {
        
        _articleRepositoryMock = new Mock<IArticleRepository>();
        _loggerMock = new Mock<ILogger<SearchArticleQuery.SearchArticleQueryHandler>>();
        _searchArticleQueryHandler = new SearchArticleQuery.SearchArticleQueryHandler(_articleRepositoryMock.Object, _loggerMock.Object);
    }

    [SetUp]
    public void SetUp()
    {
        _articleRepositoryMock.Invocations.Clear();
    }

    [Test]
    public async Task SearchArticleQueryHandler_ShouldReturnEmptyList_WhenNoArticlesFound()
    {
        // Arrange
        var query = new SearchArticleQuery()
        {
            Keyword = "nonexistent"
        };
        _articleRepositoryMock.Setup(x => x.Search(null, query.Keyword, null, null, query.Page, query.Size))
                              .ReturnsAsync(new List<Core.Entity.Article>());

        // Act
        var result = await _searchArticleQueryHandler.Handle(query, new CancellationToken());

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEmpty();
        _articleRepositoryMock.Verify(x => x.Search(null, query.Keyword, null, null, query.Page, query.Size), Times.Once);
       
    }

    [Test]
    public async Task SearchArticleQueryHandler_ShouldReturnArticles_WhenFound()
    {
        // Arrange
        var query = new SearchArticleQuery()
        {
            Keyword = "keyword",
            Page = 1,
            Size = 10
        };
        var expectedArticles = new List<Core.Entity.Article>()
        {
            new Core.Entity.Article() { Id = 1, Title = "Title 1", Content = "Content 1" },
            new Core.Entity.Article() { Id = 2, Title = "Title 2", Content = "Content 2" },
            new Core.Entity.Article() { Id = 3, Title = "Title 3", Content = "Content 3" }
        };
        _articleRepositoryMock.Setup(x => x.Search(null, query.Keyword, null, null, query.Page, query.Size))
                              .ReturnsAsync(expectedArticles);

        // Act
        var result = await _searchArticleQueryHandler.Handle(query, new CancellationToken());

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expectedArticles);
        _articleRepositoryMock.Verify(x => x.Search(null, query.Keyword, null, null, query.Page, query.Size), Times.Once);
        
    }
}

