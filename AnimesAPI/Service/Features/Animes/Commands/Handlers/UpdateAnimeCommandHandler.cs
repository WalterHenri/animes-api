using Animes.Application.DTOs;
using Animes.Application.Features.Animes.Commands;
using Animes.Domain.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace Animes.Application.Features.Animes.Commands.Handlers
{
    public class UpdateAnimeCommandHandler : IRequestHandler<UpdateAnimeCommand, AnimeDto>
    {
        private readonly IAnimeRepository _animeRepository;
        private readonly IMapper _mapper;

        public UpdateAnimeCommandHandler(IAnimeRepository animeRepository, IMapper mapper)
        {
            _animeRepository = animeRepository;
            _mapper = mapper;
        }

        public async Task<AnimeDto?> Handle(UpdateAnimeCommand request, CancellationToken cancellationToken)
        {
            var animeToUpdate = await _animeRepository.GetByIdAsync(request.Id);

            if (animeToUpdate == null)
            {
                return null;
            }

            animeToUpdate.Name = request.Name;
            animeToUpdate.Summary = request.Summary;
            animeToUpdate.DirectorId = request.DirectorId;

            await _animeRepository.UpdateAsync(animeToUpdate);
            var updatedAnimeWithDirector = await _animeRepository.GetByIdAsync(request.Id);

            return _mapper.Map<AnimeDto>(updatedAnimeWithDirector);
        }
    }
}