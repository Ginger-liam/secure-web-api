using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Module3WebApi.WebApi.Controllers;
using Module3WebApi.WebApi.Models;
using Module3WebApi.WebApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Module3WebApi.Tests
{
    [TestClass]
    public sealed class Environment2DTests
    {
        private Mock<IEnvironment2DRepository> _mockRepository;
        private Mock<IObject2DRepository> _mockObject2DRepository;
        private Mock<IAuthenticationService> _mockAuthService;
        private Mock<ILogger<Environment2DController>> _mockLogger;
        private Environment2DController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockRepository = new Mock<IEnvironment2DRepository>();
            _mockObject2DRepository = new Mock<IObject2DRepository>();
            _mockAuthService = new Mock<IAuthenticationService>();
            _mockLogger = new Mock<ILogger<Environment2DController>>();
            _controller = new Environment2DController(_mockRepository.Object, _mockObject2DRepository.Object, _mockLogger.Object, _mockAuthService.Object);
        }

        [TestMethod]
        public async Task Get_ReturnsOkResult_WithEnvironments()
        {
            // Arrange
            var userId = Guid.NewGuid().ToString();
            var environments = new List<Environment2D>
            {
                new Environment2D { Id = Guid.NewGuid(), OwnerUserId = userId, Name = "Test Environment", MaxLength = 100, MaxHeight = 100 }
            };
            _mockAuthService.Setup(auth => auth.GetCurrentAuthenticatedUserId()).Returns(userId);
            _mockRepository.Setup(repo => repo.ReadAsync(userId)).ReturnsAsync(environments);

            // Act
            var result = await _controller.Get();

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(environments, okResult.Value);
        }

        [TestMethod]
        public async Task Add_ReturnsCreatedResult()
        {
            // Arrange
            var userId = Guid.NewGuid().ToString();
            var environment2D = new Environment2D
            {
                Name = "New Environment",
                MaxLength = 100,
                MaxHeight = 100
            };
            _mockAuthService.Setup(auth => auth.GetCurrentAuthenticatedUserId()).Returns(userId);

            // Act
            var result = await _controller.Add(environment2D);

            // Assert
            Assert.IsInstanceOfType(result, typeof(CreatedResult));
        }

        [TestMethod]
        public async Task Update_ReturnsOkResult_WithUpdatedEnvironment2D()
        {
            // Arrange
            var environment2DId = Guid.NewGuid();
            var existingEnvironment2D = new Environment2D
            {
                Id = environment2DId,
                OwnerUserId = Guid.NewGuid().ToString(),
                Name = "Old Environment",
                MaxLength = 100,
                MaxHeight = 100
            };
            var newEnvironment2D = new Environment2D
            {
                Name = "Updated Environment",
                MaxLength = 200,
                MaxHeight = 200
            };
            _mockRepository.Setup(repo => repo.ReadAsync(environment2DId)).ReturnsAsync(existingEnvironment2D);
            _mockRepository.Setup(repo => repo.UpdateAsync(existingEnvironment2D)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Update(environment2DId, newEnvironment2D);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(newEnvironment2D.Name, ((Environment2D)okResult.Value).Name);
            Assert.AreEqual(newEnvironment2D.MaxLength, ((Environment2D)okResult.Value).MaxLength);
            Assert.AreEqual(newEnvironment2D.MaxHeight, ((Environment2D)okResult.Value).MaxHeight);
        }

        [TestMethod]
        public async Task Delete_ReturnsOkResult()
        {
            // Arrange
            var environment2DId = Guid.NewGuid();
            var existingEnvironment2D = new Environment2D
            {
                Id = environment2DId,
                OwnerUserId = Guid.NewGuid().ToString(),
                Name = "Environment to Delete",
                MaxLength = 100,
                MaxHeight = 100
            };
            var object2Ds = new List<Object2D>
            {
                new Object2D { Id = Guid.NewGuid(), EnvironmentId = environment2DId, PrefabId = 1, PositionX = 0.0f, PositionY = 0.0f, ScaleX = 1.0f, ScaleY = 1.0f, RotationZ = 0.0f, SortingLayer = 0 }
            };
            _mockRepository.Setup(repo => repo.ReadAsync(environment2DId)).ReturnsAsync(existingEnvironment2D);
            _mockObject2DRepository.Setup(repo => repo.ReadAllAsync(environment2DId)).ReturnsAsync(object2Ds);
            _mockObject2DRepository.Setup(repo => repo.DeleteAsync(It.IsAny<Guid>())).Returns(Task.CompletedTask);
            _mockRepository.Setup(repo => repo.DeleteAsync(environment2DId)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Delete(environment2DId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }
    }
}