using Animes.Application.DTOs;
using Animes.Application.Exceptions;
using Animes.Application.Features.Directors.Commands;
using Animes.Domain.Interfaces.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Animes.Application.Features.Directors.Commands.Handlers
{
    public class UpdateDirectorCommandHandler : IRequestHandler<UpdateDirectorCommand, DirectorDto>
    {
        private readonly IDirectorRepository _directorRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateDirectorCommandHandler> _logger;

        public UpdateDirectorCommandHandler(IDirectorRepository directorRepository, IMapper mapper, ILogger<UpdateDirectorCommandHandler> logger)
        {
            _directorRepository = directorRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<DirectorDto> Handle(UpdateDirectorCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Buscando diretor com Id {DirectorId} para atualização.", request.Id);
            var directorToUpdate = await _directorRepository.GetByIdAsync(request.Id);
            if (directorToUpdate == null)
            {
                throw new NotFoundException($"Diretor com Id {request.Id} não encontrado.");
            }

            _mapper.Map(request, directorToUpdate);

            await _directorRepository.UpdateAsync(directorToUpdate);
            _logger.LogInformation("Diretor com Id {DirectorId} foi atualizado no banco de dados.", request.Id);

            return _mapper.Map<DirectorDto>(directorToUpdate);
        }
    }
}