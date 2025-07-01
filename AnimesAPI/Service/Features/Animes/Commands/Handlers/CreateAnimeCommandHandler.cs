using Animes.Application.DTOs;
using Animes.Application.Features.Animes.Commands;
using Animes.Domain.Entities;
using Animes.Domain.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace Animes.Application.Features.Animes.Commands.Handlers
{
    public class CreateAnimeCommandHandler : IRequestHandler<CreateAnimeCommand, AnimeDto>
    {
        private readonly IAnimeRepository _animeRepository;
        private readonly IMapper _mapper;

        public CreateAnimeCommandHandler(IAnimeRepository animeRepository, IMapper mapper)
        {
            _animeRepository = animeRepository;
            _mapper = mapper;
        }

        public async Task<AnimeDto> Handle(CreateAnimeCommand request, CancellationToken cancellationToken)
        {
            var anime = new Anime
            {
                Name = request.Name,
                Summary = request.Summary,
                DirectorId = request.DirectorId
            };

            var createdAnime = await _animeRepository.CreateAsync(anime);
            var animeWithDirector = await _animeRepository.GetByIdAsync(createdAnime.Id);
            return _mapper.Map<AnimeDto>(animeWithDirector);
        }
    }
}