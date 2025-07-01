

using Animes.Application.DTOs;
using Animes.Application.Features.Directors.Queries;
using Animes.Domain.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace Animes.Application.Features.Directors.Queries.Handlers
{
    public class GetDirectorsQueryHandler : IRequestHandler<GetDirectorsQuery, IEnumerable<DirectorDto>>
    {
        private readonly IDirectorRepository _directorRepository;
        private readonly IMapper _mapper;

        public GetDirectorsQueryHandler(IDirectorRepository directorRepository, IMapper mapper)
        {
            _directorRepository = directorRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DirectorDto>> Handle(GetDirectorsQuery request, CancellationToken cancellationToken)
        {
            var director = await _directorRepository.GetDirectorsAsync(request.Id, request.Name);
            return _mapper.Map<IEnumerable<DirectorDto>>(director);
        }
    }
}