using NUnit.Framework;
using MyApp.Controllers;
using MyApp.Services;
using MyApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MyApp.Tests.Integration
{
    [TestFixture]
    public class UserProfileControllerTests
    {
        private UserProfileController _controller;
        private Mock<IUserProfileService> _mockService;

        [SetUp]
        public void Setup()
        {
            _mockService = new Mock<IUserProfileService>();
            _controller = new UserProfileController(_mockService.Object);
        }

        [Test]
        public async Task GetUserProfile_ShouldReturnOkResult()
        {
            var userProfile = new UserProfile { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" };
            _mockService.Setup(s => s.GetUserProfileAsync(1)).ReturnsAsync(userProfile);

            var result = await _controller.GetUserProfile(1) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(userProfile, result.Value);
        }

        // Additional tests for CreateUserProfile, UpdateUserProfile, and DeleteUserProfile
    }
}