using Animes.Application.DTOs;
using Animes.Application.Exceptions;
using Animes.Application.Features.Directors.Commands;
using Animes.Domain.Entities;
using Animes.Domain.Interfaces.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace Animes.Application.Features.Directors.Commands.Handlers
{
    public class CreateDirectorCommandHandler : IRequestHandler<CreateDirectorCommand, DirectorDto>
    {
        private readonly IDirectorRepository _directorRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateDirectorCommandHandler> _logger;

        public CreateDirectorCommandHandler(IDirectorRepository directorRepository, IMapper mapper, ILogger<CreateDirectorCommandHandler> logger)
        {
            _directorRepository = directorRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<DirectorDto> Handle(CreateDirectorCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Verificando se já existe um diretor com o nome {DirectorName}", request.Name);
            var existingDirector = (await _directorRepository.GetDirectorsAsync(null, request.Name)).FirstOrDefault();
            if (existingDirector != null)
            {
                throw new DuplicatedEntityException($"Já existe um diretor com o nome '{request.Name}'.");
            }

            var director = _mapper.Map<Director>(request);
            var createdDirector = await _directorRepository.CreateAsync(director);
            _logger.LogInformation("Diretor {DirectorName} (Id: {DirectorId}) foi criado no banco de dados.", createdDirector.Name, createdDirector.Id);

            return _mapper.Map<DirectorDto>(createdDirector);
        }
    }
}