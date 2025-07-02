using Animes.Application.DTOs;
using Animes.Application.Features.Animes.Commands.Handlers;
using Animes.Application.Features.Animes.Queries;
using Animes.Application.Features.Animes.Queries.Handlers;
using Animes.Application.Mappers;
using Animes.Domain.Entities;
using Animes.Domain.Interfaces.Repositories;
using Animes.Infrastructure.Persistence.Contexts;
using Animes.Infrastructure.Persistence.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Animes.Tests.Application.Animes.Handlers
{
    public class GetAnimesQueryHandlerTests : IDisposable
    {
        private readonly AnimeDbContext context;
        private readonly IAnimeRepository animeRepository;
        private readonly GetAnimesQueryHandler handler;
        private readonly IMapper mapper;
        private readonly Mock<ILogger<GetAnimesQueryHandler>> loggerMock;

        public GetAnimesQueryHandlerTests()
        {
            var builder = new DbContextOptionsBuilder<AnimeDbContext>();
            builder.UseInMemoryDatabase("InMemoryTestingDatabase");
            var options = builder.Options;
            context = new AnimeDbContext(options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated(); 

            animeRepository = new AnimeRepository(context);
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AnimeProfile());
            });
            mapper = mappingConfig.CreateMapper();
            loggerMock = new Mock<ILogger<GetAnimesQueryHandler>>();
            handler = new GetAnimesQueryHandler(animeRepository, mapper, loggerMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnAllAnimes_WhenAnimesExist()
        {
            // Arrange
            await SeedDatabase(); // Popula o banco de dados com dados de teste

            var query = new GetAnimesQuery();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<AnimeDto>>(result);
            Assert.Equal(2, result.Count());

            var anime1 = result.FirstOrDefault(a => a.Name == "Fullmetal Alchemist: Brotherhood");
            Assert.NotNull(anime1);
            Assert.Equal("Yasuhiro Irie", anime1.DirectorName);

            var anime2 = result.FirstOrDefault(a => a.Name == "Attack on Titan");
            Assert.NotNull(anime2);
            Assert.Equal("Tetsurō Araki", anime2.DirectorName);
        }

        [Fact]
        public async Task Handle_ShouldReturnEmptyList_WhenNoAnimesExist()
        {
            // Arrange
            var query = new GetAnimesQuery();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<AnimeDto>>(result);
            Assert.Empty(result);
        }

        private async Task SeedDatabase()
        {
            var director1 = new Director { Name = "Yasuhiro Irie" };
            var director2 = new Director { Name = "Tetsurō Araki" };

            await context.Directors.AddRangeAsync(director1, director2);
            await context.SaveChangesAsync();

            var anime1 = new Anime
            {
                Name = "Fullmetal Alchemist: Brotherhood",
                Summary = "A história de dois irmãos alquimistas em busca da pedra filosofal.",
                DirectorId = director1.Id
            };
            var anime2 = new Anime
            {
                Name = "Attack on Titan",
                Summary = "A humanidade luta pela sobrevivência contra titãs devoradores de humanos.",
                DirectorId = director2.Id
            };

            await context.Animes.AddRangeAsync(anime1, anime2);
            await context.SaveChangesAsync();
        }

        // Garante que o banco de dados em memória seja descartado após cada teste
        public void Dispose()
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}