using Animes.Application.Features.Animes.Commands;
using Animes.Application.Features.Animes.Commands.Handlers;
using Animes.Application.Mappers;
using Animes.Domain.Entities;
using Animes.Domain.Interfaces.Repositories;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Animes.Application.Tests.Features.Animes.Commands
{
    public class CreateAnimeCommandHandlerTests
    {
        private readonly Mock<IAnimeRepository> _animeRepositoryMock;
        private readonly Mock<IDirectorRepository> _directorRepositoryMock;
        private readonly Mock<ILogger<CreateAnimeCommandHandler>> _loggerMock;
        private readonly IMapper _mapper;
        private readonly CreateAnimeCommandHandler _handler;

        public CreateAnimeCommandHandlerTests()
        {
            _animeRepositoryMock = new Mock<IAnimeRepository>();
            _directorRepositoryMock = new Mock<IDirectorRepository>();
            _loggerMock = new Mock<ILogger<CreateAnimeCommandHandler>>();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<AnimeProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            _handler = new CreateAnimeCommandHandler(
                _animeRepositoryMock.Object,
                _directorRepositoryMock.Object,
                _mapper,
                _loggerMock.Object
            );
        }

        [Fact]
        public async Task Handle_ShouldCreateAndReturnAnimeDto()
        {
            // Arrange
            var command = new CreateAnimeCommand
            {
                Name = "Naruto",
                Summary = "A story about a ninja.",
                DirectorId = 1
            };

            var anime = new Anime
            {
                Id = 1,
                Name = command.Name,
                Summary = command.Summary,
                DirectorId = command.DirectorId,
                Director = new Director { Id = 1, Name = "Hayato Date" }
            };

            _directorRepositoryMock.Setup(r => r.GetByIdAsync(command.DirectorId.Value)).ReturnsAsync(new Director());
            _animeRepositoryMock.Setup(r => r.GetAnimesAsync(null, command.Name, null)).ReturnsAsync(new List<Anime>());
            _animeRepositoryMock.Setup(r => r.CreateAsync(It.IsAny<Anime>())).ReturnsAsync(anime);
            _animeRepositoryMock.Setup(r => r.GetByIdAsync(anime.Id)).ReturnsAsync(anime);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(command.Name, result.Name);
            Assert.Equal(command.Summary, result.Summary);
            Assert.Equal("Hayato Date", result.DirectorName);
            _animeRepositoryMock.Verify(r => r.CreateAsync(It.IsAny<Anime>()), Times.Once);
        }
    }
}