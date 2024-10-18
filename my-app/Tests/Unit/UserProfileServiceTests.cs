using NUnit.Framework;
using Moq;
using MyApp.Services;
using MyApp.Data;
using MyApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MyApp.Tests.Unit
{
    [TestFixture]
    public class UserProfileServiceTests
    {
        private Mock<UserProfileDbContext> _mockContext;
        private IUserProfileService _userProfileService;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<UserProfileDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _mockContext = new Mock<UserProfileDbContext>(options);
            _userProfileService = new UserProfileService(_mockContext.Object);
        }

        [Test]
        public async Task GetUserProfileAsync_ShouldReturnUserProfile()
        {
            var userProfile = new UserProfile { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" };
            _mockContext.Setup(m => m.UserProfiles.FindAsync(1)).ReturnsAsync(userProfile);

            var result = await _userProfileService.GetUserProfileAsync(1);

            Assert.AreEqual(userProfile, result);
        }

        // Additional tests for CreateUserProfileAsync, UpdateUserProfileAsync, and DeleteUserProfileAsync
    }
}