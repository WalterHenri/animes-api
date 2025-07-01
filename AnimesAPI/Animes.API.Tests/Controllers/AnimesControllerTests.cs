using Animes.Application.DTOs;
using Animes.Application.Features.Animes.Commands;
using Animes.Domain.Entities;
using Animes.Infrastructure.Persistence.Contexts;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace Animes.API.Tests.Controllers
{
    public class AnimesControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>, IAsyncLifetime
    {
        private readonly HttpClient _client;
        private readonly IServiceScopeFactory _scopeFactory;

        public AnimesControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
            _scopeFactory = factory.Services.GetRequiredService<IServiceScopeFactory>();
        }

        public async Task InitializeAsync()
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AnimeDbContext>();
            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();

            var director = new Director { Name = "Hayao Miyazaki" };
            context.Directors.Add(director);
            await context.SaveChangesAsync();

            context.Animes.Add(new Anime { Name = "Spirited Away", Summary = "...", DirectorId = director.Id });
            await context.SaveChangesAsync();
        }

        public Task DisposeAsync() => Task.CompletedTask;

        [Fact]
        public async Task Get_Animes_ShouldReturnOkAndListOfAnimes()
        {
            // Act
            var response = await _client.GetAsync("/api/v1/Animes");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var animes = await response.Content.ReadFromJsonAsync<List<AnimeDto>>();
            animes.Should().NotBeNull().And.HaveCount(1);
            animes.Should().Contain(a => a.Name == "Spirited Away");
        }

        [Fact]
        public async Task Post_Anime_WithValidData_ShouldReturnCreated()
        {
            // Arrange
            var newAnimeCommand = new CreateAnimeCommand
            {
                Name = "My Neighbor Totoro",
                Summary = "...",
                DirectorId = 1
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/v1/Animes", newAnimeCommand);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            var createdAnime = await response.Content.ReadFromJsonAsync<AnimeDto>();
            createdAnime.Should().NotBeNull();
            createdAnime.Name.Should().Be(newAnimeCommand.Name);
            createdAnime.DirectorName.Should().Be("Hayao Miyazaki");
        }
    }
}