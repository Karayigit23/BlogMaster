using BlogMaster.Core.Entity;
using BlogMaster.Core.InterFaces;
using BlogMaster.Core.Query.User;
using Microsoft.Extensions.Logging;
using Moq;

namespace BlogMaster.Test.Query;

   
public class GetUserByUserNameQueryHandlerTest
    {
        private Mock<IUserRepository> _userRepository;
        private Mock<ILogger<GetUserByUserNameQueryHandler>> _logger;
        private GetUserByUserNameQueryHandler _handler;

        public GetUserByUserNameQueryHandlerTest()
        {
            _userRepository = new Mock<IUserRepository>();
            _logger = new Mock<ILogger<GetUserByUserNameQueryHandler>>();
            _handler = new GetUserByUserNameQueryHandler(_userRepository.Object,_logger.Object);
        }

        [SetUp]
        public void SetUp()
        {
            _userRepository.Invocations.Clear();
          
        }

        [Test]
        public async Task Handle_WhenUserExists_ReturnsUser()
        {

            var expectedUser = new User { Id = 1, UserName = "testuser", FirstName = "Test", LastName = "User", Email = "testuser@example.com", Password = "testpassword" };
            _userRepository.Setup(x => x.GetUserByUsername(expectedUser.UserName)).ReturnsAsync(expectedUser);

            // Act
            var result = await _handler.Handle(new GetUserByUserNameQuery { UserName = expectedUser.UserName }, CancellationToken.None);

           
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedUser, result);
            _userRepository.Verify(x => x.GetUserByUsername(expectedUser.UserName), Times.Once);
        }

        [Test]
        public void Handle_WhenUserDoesNotExist_ThrowsUserNotFoundException()
        {
          
            var nonExistingUserName = "nonexistinguser";
            _userRepository.Setup(x => x.GetUserByUsername(nonExistingUserName)).ReturnsAsync((User)null);
            
            
            
            //exceptionları buraya gir
            var ex = Assert.ThrowsAsync<Exception>(() => _handler.Handle(new GetUserByUserNameQuery { UserName = nonExistingUserName }, CancellationToken.None));
            Assert.AreEqual($"user not found userName: {nonExistingUserName}", ex.Message);
            _userRepository.Verify(x => x.GetUserByUsername(nonExistingUserName), Times.Once);
        }
    }

