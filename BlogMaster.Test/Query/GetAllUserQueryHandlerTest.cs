using BlogMaster.Core.InterFaces;
using BlogMaster.Core.Query.User;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace BlogMaster.Test.Query;

public class GetAllUserQueryHandlerTest
    {
        private readonly Mock<IUserRepository> _userRepository;
        private readonly Mock<ILogger<GetAllUserQuery.GetAllUserQueryHandler>> _logger;
        private readonly GetAllUserQuery.GetAllUserQueryHandler _handler;

        public GetAllUserQueryHandlerTest()
        {
            _userRepository = new Mock<IUserRepository>();
            _logger = new Mock<ILogger<GetAllUserQuery.GetAllUserQueryHandler>>();

            _handler = new GetAllUserQuery.GetAllUserQueryHandler(_userRepository.Object, _logger.Object);
        }

        [SetUp]
        public void SetUp()
        {
            _userRepository.Invocations.Clear();
        }

        [Test]
        public async Task Handle_WhenCalled_ReturnsAllUsers()
        {

            var users = new List<Core.Entity.User>()
            {
                new Core.Entity.User
                {
                    Id = 1, UserName = "user1", FirstName = "Hasan", LastName = "ateş", Email = "user1@test.com",
                    Password = "password1"
                },
                new Core.Entity.User
                {
                    Id = 2, UserName = "user2", FirstName = "Gizem", LastName = "kuş", Email = "user2@test.com",
                    Password = "password2"
                },
                new Core.Entity.User
                {
                    Id = 3, UserName = "user3", FirstName = "Murat", LastName = "atlı", Email = "user3@test.com",
                    Password = "password3"
                }
            };

            _userRepository.Setup(x => x.GetAllUsers()).ReturnsAsync(users);





            var result = await _handler.Handle(new GetAllUserQuery(), CancellationToken.None);


            Assert.AreEqual(users.Count, result.Count);
        }

    }
