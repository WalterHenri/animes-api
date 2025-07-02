using Animes.Application.DTOs;
using Animes.Application.Features.Animes.Queries;
using Animes.Domain.Interfaces.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Animes.Application.Features.Animes.Queries.Handlers
{
    public class GetAnimesQueryHandler : IRequestHandler<GetAnimesQuery, IEnumerable<AnimeDto>>
    {
        private readonly IAnimeRepository _animeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAnimesQueryHandler> _logger;

        public GetAnimesQueryHandler(IAnimeRepository animeRepository, IMapper mapper, ILogger<GetAnimesQueryHandler> logger)
        {
            _animeRepository = animeRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<AnimeDto>> Handle(GetAnimesQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Executando busca de animes no repositório com os filtros: Id={AnimeId}, Name={AnimeName}, Director={DirectorName}", request.Id, request.Name, request.Director);

            var animes = await _animeRepository.GetAnimesAsync(request.Id, request.Name, request.Director);

            _logger.LogInformation("Mapeando {AnimeCount} animes para DTOs.", animes.Count());

            return _mapper.Map<IEnumerable<AnimeDto>>(animes);
        }
    }
}