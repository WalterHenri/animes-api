using Animes.Application.DTOs;
using Animes.Application.Exceptions;
using Animes.Application.Features.Animes.Commands;
using Animes.Domain.Entities;
using Animes.Domain.Interfaces.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Animes.Application.Features.Animes.Commands.Handlers
{
    public class CreateAnimeCommandHandler : IRequestHandler<CreateAnimeCommand, AnimeDto>
    {
        private readonly IAnimeRepository _animeRepository;
        private readonly IDirectorRepository _directorRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateAnimeCommandHandler> _logger;

        public CreateAnimeCommandHandler(IAnimeRepository animeRepository, IDirectorRepository directorRepository, IMapper mapper, ILogger<CreateAnimeCommandHandler> logger)
        {
            _animeRepository = animeRepository;
            _directorRepository = directorRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<AnimeDto> Handle(CreateAnimeCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Verificando se o diretor com Id {DirectorId} existe.", request.DirectorId);
            if (request.DirectorId.HasValue && await _directorRepository.GetByIdAsync(request.DirectorId.Value) == null)
            {
                throw new NotFoundException($"Diretor com Id {request.DirectorId} não encontrado.");
            }

            _logger.LogInformation("Verificando se já existe um anime com o nome {AnimeName}.", request.Name);
            var existingAnime = (await _animeRepository.GetAnimesAsync(null, request.Name, null)).FirstOrDefault();
            if (existingAnime != null)
            {
                throw new DuplicatedEntityException($"Já existe um anime com o nome '{request.Name}'.");
            }

            var anime = new Anime
            {
                Name = request.Name,
                Summary = request.Summary,
                DirectorId = request.DirectorId
            };

            var createdAnime = await _animeRepository.CreateAsync(anime);
            _logger.LogInformation("Anime {AnimeName} (Id: {AnimeId}) foi criado no banco de dados.", createdAnime.Name, createdAnime.Id);

            var animeWithDirector = await _animeRepository.GetByIdAsync(createdAnime.Id);
            return _mapper.Map<AnimeDto>(animeWithDirector);
        }
    }
}