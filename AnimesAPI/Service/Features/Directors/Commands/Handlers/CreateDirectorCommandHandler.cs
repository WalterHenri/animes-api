using Animes.Application.DTOs;
using Animes.Application.Features.Directors.Commands;
using Animes.Domain.Entities;
using Animes.Domain.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace Animes.Application.Features.Directors.Commands.Handlers;

public class CreateDirectorCommandHandler : IRequestHandler<CreateDirectorCommand, DirectorDto>
{
    private readonly IDirectorRepository _directorRepository;
    private readonly IMapper _mapper;

    public CreateDirectorCommandHandler(IDirectorRepository directorRepository, IMapper mapper)
    {
        _directorRepository = directorRepository;
        _mapper = mapper;
    }

    public async Task<DirectorDto> Handle(CreateDirectorCommand request, CancellationToken cancellationToken)
    {
        var director = _mapper.Map<Director>(request);
        var createdDirector = await _directorRepository.CreateAsync(director);
        return _mapper.Map<DirectorDto>(createdDirector);
    }
}