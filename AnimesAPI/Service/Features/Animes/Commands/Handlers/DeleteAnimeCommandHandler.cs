using Animes.Application.Exceptions;
using Animes.Application.Features.Animes.Commands;
using Animes.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Animes.Application.Features.Animes.Commands.Handlers
{
    public class DeleteAnimeCommandHandler : IRequestHandler<DeleteAnimeCommand, bool>
    {
        private readonly IAnimeRepository _animeRepository;
        private readonly ILogger<DeleteAnimeCommandHandler> _logger;

        public DeleteAnimeCommandHandler(IAnimeRepository animeRepository, ILogger<DeleteAnimeCommandHandler> logger)
        {
            _animeRepository = animeRepository;
            _logger = logger;
        }

        public async Task<bool> Handle(DeleteAnimeCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Verificando se o anime com Id {AnimeId} existe para exclusão.", request.Id);
            var animeToDelete = await _animeRepository.GetByIdAsync(request.Id);
            if (animeToDelete == null)
            {
                throw new NotFoundException($"Anime com Id {request.Id} não encontrado para exclusão.");
            }

            var result = await _animeRepository.DeleteAsync(request.Id);
            _logger.LogInformation("Anime com Id {AnimeId} foi excluído do banco de dados.", request.Id);
            return result;
        }
    }
}