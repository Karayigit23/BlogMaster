using BlogMaster.Core.Entity;
using BlogMaster.Core.Exception;
using BlogMaster.Core.InterFaces;
using BlogMaster.Core.Query.User;
using Microsoft.Extensions.Logging;
using Moq;

namespace BlogMaster.Test.Query;

public class GetUserByIdQueryHandlerTest
{
    private Mock<IUserRepository> _userRepository;
    private Mock<ILogger<GetUserByIdQueryHandler>> _logger;
    private GetUserByIdQueryHandler _handler;

    public GetUserByIdQueryHandlerTest()
    {
        _userRepository = new Mock<IUserRepository>();
        _logger= new Mock<ILogger<GetUserByIdQueryHandler>>();
        _handler = new GetUserByIdQueryHandler(_userRepository.Object, _logger.Object);
    }

    [SetUp]
        public void SetUp()
        {
            _userRepository.Invocations.Clear();
        }

        [Test]
        public async Task handle_when_user_exists_then_returns_user()
        {
          
            var userId = 1;
            var expectedUser = new User { Id = userId, UserName = "Testuser", FirstName = "Test", LastName = "User", Email = "testuser@gmail.com", Password = "testpassword" };
            _userRepository.Setup(x => x.GetUserById(userId)).ReturnsAsync(expectedUser);

           
            var result = await _handler.Handle(new GetUserByIdQuery { Id = userId }, CancellationToken.None);

         
            Assert.AreEqual(expectedUser, result);
          
        }

        [Test]
        public void handle_when_user_does_not_exist_throws_exception()
        {
            // Arrange
                var userId = 1;
                _userRepository.Setup(x => x.GetUserById(userId)).ReturnsAsync((User)null);

                // Act + Assert
                var ex = Assert.ThrowsAsync<NotFoundException>(() =>
                    _handler.Handle(new GetUserByIdQuery { Id = userId }, CancellationToken.None));
                Assert.AreEqual($"user not found userId: {userId}", ex.Message);
            


        }
}

