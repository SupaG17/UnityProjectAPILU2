using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ProjectMap.WebApi;

namespace SystemTests;

public class SystemTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public SystemTest(WebApplicationFactory<Program> factory)
    {
        var customFactory = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                // Remove the existing DbContext registration
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<ProjectMap.WebApi.DataAccess.GameContext>));
                if (descriptor != null)
                    services.Remove(descriptor);

                // Add in-memory database for testing
                services.AddDbContext<ProjectMap.WebApi.DataAccess.GameContext>(options =>
                {
                    options.UseInMemoryDatabase("TestDb");
                });

                // Configure JWT for tests
                services.PostConfigure<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "test-issuer",
                        ValidAudience = "test-audience",
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes("test-key-at-least-16-chars-long-for-tests"))
                    };
                });
            });

            // Add test configuration
            builder.ConfigureAppConfiguration((context, config) =>
            {
                var testConfig = new Dictionary<string, string>
                {
                    {"Jwt:Key", "test-key-at-least-16-chars-long-for-tests"},
                    {"Jwt:Issuer", "test-issuer"},
                    {"Jwt:Audience", "test-audience"},
                    {"ConnectionStrings:SqlConnectionString", "TestDb"}
                };

                config.AddInMemoryCollection(testConfig);
            });
        });

        _client = customFactory.CreateClient();
    }

    [Fact]
    public async Task AuthController_Register_Endpoint_Exists()
    {

        var response = await _client.GetAsync("/api/auth/register");

        Assert.Equal(HttpStatusCode.MethodNotAllowed, response.StatusCode);
    }

    [Fact]
    public async Task AuthController_Login_Endpoint_Exists()
    {
        var response = await _client.GetAsync("/api/auth/login");

        Assert.Equal(HttpStatusCode.MethodNotAllowed, response.StatusCode);
    }

    [Fact]
    public async Task EnvironmentController_Endpoints_Exist()
    {
        // Testing GET endpoint
        var response = await _client.GetAsync("/api/environments");

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task GameObjectController_Endpoint_Exists()
    {
        // Testing POST endpoint with GET to check existence
        var response = await _client.GetAsync("/api/gameobject/update");

        Assert.Equal(HttpStatusCode.MethodNotAllowed, response.StatusCode);
    }

}