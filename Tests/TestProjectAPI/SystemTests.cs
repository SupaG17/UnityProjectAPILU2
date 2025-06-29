using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Projectmap.WebApi;
//using Projectmap.WebApi.Requests;
using Xunit;

namespace Projectmap.WebApi.Tests.SystemTests
{

}
namespace TestProjectAPI
{
    public class EnvironmentControllerSystemTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public EnvironmentControllerSystemTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Post_ValidEnvironment_ReturnsCreated()
        {
            // Arrange
            var client = _factory.CreateClient();
            var dto = new CreateEnvironment2DDto
            {
                Name = "TestEnv",
                MaxLength = 10,
                MaxHeight = 10
            };

            // Act
            var response = await client.PostAsJsonAsync("/api/environments", dto);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task Get_Environments_ReturnsOk()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/environments");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Post_DuplicateEnvironment_ReturnsConflict()
        {
            // Arrange
            var client = _factory.CreateClient();
            var dto = new CreateEnvironment2DDto
            {
                Name = "DuplicateEnv",
                MaxLength = 10,
                MaxHeight = 10
            };

            // Act
            await client.PostAsJsonAsync("/api/environments", dto);
            var response = await client.PostAsJsonAsync("/api/environments", dto);

            // Assert
            Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
        }
    }

    internal class CreateEnvironment2DDto
    {
        public string Name { get; set; }
        public int MaxLength { get; set; }
        public int MaxHeight { get; set; }
    }
}
