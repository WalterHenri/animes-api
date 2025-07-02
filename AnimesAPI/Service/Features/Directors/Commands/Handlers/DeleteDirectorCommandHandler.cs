using Animes.Application.Exceptions;
using Animes.Application.Features.Directors.Commands;
using Animes.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace Animes.Application.Features.Directors.Commands.Handlers
{
    public class DeleteDirectorCommandHandler : IRequestHandler<DeleteDirectorCommand, bool>
    {
        private readonly IDirectorRepository _directorRepository;
        private readonly ILogger<DeleteDirectorCommandHandler> _logger;

        public DeleteDirectorCommandHandler(IDirectorRepository directorRepository, ILogger<DeleteDirectorCommandHandler> logger)
        {
            _directorRepository = directorRepository;
            _logger = logger;
        }

        public async Task<bool> Handle(DeleteDirectorCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Buscando diretor com Id {DirectorId} para exclusão.", request.Id);
            var director = await _directorRepository.GetByIdAsync(request.Id);
            if (director == null)
            {
                throw new NotFoundException($"Diretor com Id {request.Id} não encontrado.");
            }

            if (director.Animes.Any())
            {
                throw new BadRequestException("Não é possível excluir um diretor que está associado a um ou mais animes.");
            }

            var result = await _directorRepository.DeleteAsync(request.Id);
            _logger.LogInformation("Diretor com Id {DirectorId} foi excluído.", request.Id);

            return result;
        }
    }
}