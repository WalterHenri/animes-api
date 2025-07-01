using Animes.Application.DTOs;
using MediatR;

namespace Animes.Application.Features.Directors.Commands;

public class UpdateDirectorCommand : IRequest<DirectorDto>
{
    public int Id { get; set; }
    public required string Name { get; set; }
}