using Animes.Application.Features.Directors.Commands;
using Animes.Domain.Interfaces.Repositories;
using MediatR;

namespace Animes.Application.Features.Directors.Commands.Handlers
{
    public class DeleteDirectorCommandHandler : IRequestHandler<DeleteDirectorCommand, bool>
    {
        private readonly IDirectorRepository _directorRepository;

        public DeleteDirectorCommandHandler(IDirectorRepository directorRepository)
        {
            _directorRepository = directorRepository;
        }

        public async Task<bool> Handle(DeleteDirectorCommand request, CancellationToken cancellationToken)
        {
            return await _directorRepository.DeleteAsync(request.Id);
        }
    }
}