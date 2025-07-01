using Animes.Application.Features.Animes.Commands;
using Animes.Domain.Interfaces.Repositories;
using MediatR;

namespace Animes.Application.Features.Animes.Commands.Handlers
{
    public class DeleteAnimeCommandHandler : IRequestHandler<DeleteAnimeCommand, bool>
    {
        private readonly IAnimeRepository _animeRepository;

        public DeleteAnimeCommandHandler(IAnimeRepository animeRepository)
        {
            _animeRepository = animeRepository;
        }

        public async Task<bool> Handle(DeleteAnimeCommand request, CancellationToken cancellationToken)
        {
            return await _animeRepository.DeleteAsync(request.Id);
        }
    }
}