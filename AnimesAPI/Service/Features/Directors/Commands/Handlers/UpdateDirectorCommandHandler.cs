using Animes.Application.DTOs;
using Animes.Application.Features.Directors.Commands;
using Animes.Domain.Entities;
using Animes.Domain.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace Animes.Application.Features.Directors.Commands.Handlers
{
    public class UpdateDirectorCommandHandler : IRequestHandler<UpdateDirectorCommand, DirectorDto>
    {
        private readonly IDirectorRepository _directorRepository;
        private readonly IMapper _mapper;

        public UpdateDirectorCommandHandler(IDirectorRepository directorRepository, IMapper mapper)
        {
            _directorRepository = directorRepository;
            _mapper = mapper;
        }

        public async Task<DirectorDto?> Handle(UpdateDirectorCommand request, CancellationToken cancellationToken)
        {
            var directorToUpdate = await _directorRepository.GetByIdAsync(request.Id);
            if (directorToUpdate == null) return null;

            directorToUpdate.Name = request.Name;

            var updatedDirector = await _directorRepository.UpdateAsync(directorToUpdate);
            return _mapper.Map<DirectorDto>(updatedDirector);
        }
    }
}