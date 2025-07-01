using Animes.Application.DTOs;
using Animes.Domain.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace Animes.Application.Features.Animes.Queries.Handlers;

public class GetAnimesQueryHandler : IRequestHandler<GetAnimesQuery, IEnumerable<AnimeDto>>
{
    private readonly IAnimeRepository animeRepository;
    private readonly IMapper mapper;

    public GetAnimesQueryHandler(IAnimeRepository animeRepository, IMapper mapper)
    {
        this.animeRepository = animeRepository;
        this.mapper = mapper;
    }

    public async Task<IEnumerable<AnimeDto>> Handle(GetAnimesQuery request, CancellationToken cancellationToken)
    {
        var animes = await animeRepository.GetAnimesAsync(request.Id, request.Name, request.Director);
        return mapper.Map<IEnumerable<AnimeDto>>(animes);
    }
}