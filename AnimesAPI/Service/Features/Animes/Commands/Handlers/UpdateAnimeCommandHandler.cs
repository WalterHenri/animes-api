using Animes.Application.DTOs;
using Animes.Application.Exceptions;
using Animes.Application.Features.Animes.Commands;
using Animes.Domain.Interfaces.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Animes.Application.Features.Animes.Commands.Handlers
{
    public class UpdateAnimeCommandHandler : IRequestHandler<UpdateAnimeCommand, AnimeDto>
    {
        private readonly IAnimeRepository _animeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateAnimeCommandHandler> _logger;

        public UpdateAnimeCommandHandler(IAnimeRepository animeRepository, IMapper mapper, ILogger<UpdateAnimeCommandHandler> logger)
        {
            _animeRepository = animeRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<AnimeDto> Handle(UpdateAnimeCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Buscando anime com Id {AnimeId} para atualização.", request.Id);
            var animeToUpdate = await _animeRepository.GetByIdAsync(request.Id);

            if (animeToUpdate == null)
            {
                throw new NotFoundException($"Anime com ID {request.Id} não encontrado.");
            }

            animeToUpdate.Name = request.Name;
            animeToUpdate.Summary = request.Summary;
            animeToUpdate.DirectorId = request.DirectorId;

            await _animeRepository.UpdateAsync(animeToUpdate);
            _logger.LogInformation("Anime com Id {AnimeId} foi atualizado no banco de dados.", request.Id);

            var updatedAnimeWithDirector = await _animeRepository.GetByIdAsync(request.Id);

            return _mapper.Map<AnimeDto>(updatedAnimeWithDirector);
        }
    }
}