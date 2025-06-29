using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using ProjectMap.WebApi.Controllers;
using ProjectMap.WebApi.DataAccess;
using ProjectMap.WebApi.Entities;
using ProjectMap.WebApi.Requests;
using Xunit;

namespace ProjectMap.WebApi.Tests
{
    public class GameObjectControllerTests
    {
        //maakt een mock van de DbSet zodat deze gebruikt kan worden in onze tests
        private static Mock<DbSet<T>> CreateMockDbSet<T>(IQueryable<T> data) where T : class
        {
            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            return mockSet;
        }
        //maakt een wereld aan en een gameobject
        [Fact]
        public void UpdateGameObjects_UpdatesGameObjects()
        {
            // Arrange
            var env = new Environment2D { Id = 1, Name = "Env1", MaxLength = 10, MaxHeight = 10, UserId = "user1", BackgroundColor = "#fff" };
            var environments = new List<Environment2D> { env }.AsQueryable();
            var gameObjects = new List<GameObject>
            {
                new GameObject { Id = 1, Xposition = 1, Yposition = 1, PrefabId = "p1", Environment2DId = 1 }
            }.AsQueryable();

            var mockEnvSet = CreateMockDbSet(environments);
            mockEnvSet.Setup(m => m.Find(It.IsAny<object[]>()))
                .Returns<object[]>(ids => environments.FirstOrDefault(e => e.Id == (int)ids[0]));


            var mockGoSet = CreateMockDbSet(gameObjects);
            mockGoSet.Setup(m => m.RemoveRange(It.IsAny<IEnumerable<GameObject>>()));
            mockGoSet.Setup(m => m.Add(It.IsAny<GameObject>()));

            var mockContext = new Mock<GameContext>(new DbContextOptions<GameContext>());
            mockContext.Setup(c => c.Environments).Returns(mockEnvSet.Object);
            mockContext.Setup(c => c.GameObjects).Returns(mockGoSet.Object);

            var controller = new GameObjectController(mockContext.Object);

            var dto = new UpdateGameObjectsDto
            {
                Environment2DId = 1,
                GameObjects = new List<CreateGameObjectDto>
                {
                    new CreateGameObjectDto { Xposition = 2, Yposition = 2, PrefabId = "p2", EnvironmentId = 1 }
                }
            };

            // Act
            var result = controller.UpdateGameObjects(dto);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
        //chekct of de wereld bestaat
        [Fact]
        public void UpdateGameObjects_EnvironmentNotFound_ReturnsNotFound()
        {
            // Arrange
            var environments = new List<Environment2D>().AsQueryable();
            var mockEnvSet = CreateMockDbSet(environments);

            mockEnvSet.Setup(m => m.Find(It.IsAny<object[]>()))
                .Returns<object[]>(ids => environments.FirstOrDefault(e => e.Id == (int)ids[0]));


            var mockGoSet = CreateMockDbSet(new List<GameObject>().AsQueryable());

            var mockContext = new Mock<GameContext>(new DbContextOptions<GameContext>());
            mockContext.Setup(c => c.Environments).Returns(mockEnvSet.Object);
            mockContext.Setup(c => c.GameObjects).Returns(mockGoSet.Object);

            var controller = new GameObjectController(mockContext.Object);

            var dto = new UpdateGameObjectsDto
            {
                Environment2DId = 1,
                GameObjects = new List<CreateGameObjectDto>()
            };

            // Act
            var result = controller.UpdateGameObjects(dto);

            // Assert
            var notFound = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Contains("Environment not found", notFound.Value?.ToString() ?? string.Empty);
        }
    }
}

//easter egg
// // Dit is een test voor de easter egg