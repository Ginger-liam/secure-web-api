using Moq;
using Module3WebApi.WebApi.Controllers;
using Module3WebApi.WebApi.Models;
using Module3WebApi.WebApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Module3WebApi.Tests
{
    [TestClass]
    public sealed class Object2DTests
    {
        private Mock<IObject2DRepository> _mockRepository;
        private Mock<ILogger<Object2DController>> _mockLogger;
        private Object2DController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockRepository = new Mock<IObject2DRepository>();
            _mockLogger = new Mock<ILogger<Object2DController>>();
            _controller = new Object2DController(_mockRepository.Object, _mockLogger.Object);
        }

        [TestMethod]
        public async Task Get_ReturnsOkResult_WithObject2D()
        {
            // Arrange
            var environmentId = Guid.NewGuid();
            var object2D = new Object2D
            {
                Id = Guid.NewGuid(),
                EnvironmentId = environmentId,
                PrefabId = 1,
                PositionX = 0.0f,
                PositionY = 0.0f,
                ScaleX = 1.0f,
                ScaleY = 1.0f,
                RotationZ = 0.0f,
                SortingLayer = 0
            };
            _mockRepository.Setup(repo => repo.ReadAllAsync(environmentId)).ReturnsAsync(new List<Object2D> { object2D });

            // Act
            var result = await _controller.Get(environmentId);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(object2D, ((List<Object2D>)okResult.Value)[0]);
        }

        [TestMethod]
        public async Task Add_ReturnsCreatedResult()
        {
            // Arrange
            var object2D = new Object2D
            {
                EnvironmentId = Guid.NewGuid(),
                PrefabId = 1,
                PositionX = 0.0f,
                PositionY = 0.0f,
                ScaleX = 1.0f,
                ScaleY = 1.0f,
                RotationZ = 0.0f,
                SortingLayer = 0
            };
            _mockRepository.Setup(repo => repo.InsertAsync(It.IsAny<Object2D>())).ReturnsAsync(object2D);

            // Act
            var result = await _controller.Add(object2D);

            // Assert
            Assert.IsInstanceOfType(result, typeof(CreatedResult));
        }

        [TestMethod]
        public async Task Update_ReturnsOkResult_WithUpdatedObject2D()
        {
            // Arrange
            var object2DId = Guid.NewGuid();
            var existingObject2D = new Object2D
            {
                Id = object2DId,
                EnvironmentId = Guid.NewGuid(),
                PrefabId = 1,
                PositionX = 0.0f,
                PositionY = 0.0f,
                ScaleX = 1.0f,
                ScaleY = 1.0f,
                RotationZ = 0.0f,
                SortingLayer = 0
            };
            var newObject2D = new Object2D
            {
                EnvironmentId = Guid.NewGuid(),
                PrefabId = 2,
                PositionX = 1.0f,
                PositionY = 1.0f,
                ScaleX = 2.0f,
                ScaleY = 2.0f,
                RotationZ = 45.0f,
                SortingLayer = 1
            };
            _mockRepository.Setup(repo => repo.ReadAsync(object2DId)).ReturnsAsync(existingObject2D);
            _mockRepository.Setup(repo => repo.UpdateAsync(existingObject2D)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Update(object2DId, newObject2D);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(newObject2D.EnvironmentId, ((Object2D)okResult.Value).EnvironmentId);
            Assert.AreEqual(newObject2D.PrefabId, ((Object2D)okResult.Value).PrefabId);
            Assert.AreEqual(newObject2D.PositionX, ((Object2D)okResult.Value).PositionX);
            Assert.AreEqual(newObject2D.PositionY, ((Object2D)okResult.Value).PositionY);
            Assert.AreEqual(newObject2D.ScaleX, ((Object2D)okResult.Value).ScaleX);
            Assert.AreEqual(newObject2D.ScaleY, ((Object2D)okResult.Value).ScaleY);
            Assert.AreEqual(newObject2D.RotationZ, ((Object2D)okResult.Value).RotationZ);
            Assert.AreEqual(newObject2D.SortingLayer, ((Object2D)okResult.Value).SortingLayer);
        }

        [TestMethod]
        public async Task Delete_ReturnsOkResult()
        {
            // Arrange
            var object2DId = Guid.NewGuid();
            var existingObject2D = new Object2D
            {
                Id = object2DId,
                EnvironmentId = Guid.NewGuid(),
                PrefabId = 1,
                PositionX = 0.0f,
                PositionY = 0.0f,
                ScaleX = 1.0f,
                ScaleY = 1.0f,
                RotationZ = 0.0f,
                SortingLayer = 0
            };
            _mockRepository.Setup(repo => repo.ReadAsync(object2DId)).ReturnsAsync(existingObject2D);
            _mockRepository.Setup(repo => repo.DeleteAsync(object2DId)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Delete(object2DId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }
    }
}