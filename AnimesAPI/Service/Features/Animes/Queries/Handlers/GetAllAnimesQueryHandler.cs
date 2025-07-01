using Animes.Application.DTOs;
using Animes.Domain.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace Animes.Application.Features.Animes.Queries.Handlers;

public class GetAllAnimesQueryHandler : IRequestHandler<GetAllAnimesQuery, IEnumerable<AnimeDto>>
{
    private readonly IAnimeRepository animeRepository;
    private readonly IMapper mapper;

    public GetAllAnimesQueryHandler(IAnimeRepository animeRepository, IMapper mapper)
    {
        this.animeRepository = animeRepository;
        this.mapper = mapper;
    }

    public async Task<IEnumerable<AnimeDto>> Handle(GetAllAnimesQuery request, CancellationToken cancellationToken)
    {
        var animes = await animeRepository.GetAllAsync();

        return mapper.Map<IEnumerable<AnimeDto>>(animes);

        
    }
}