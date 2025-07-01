using MediatR;

namespace Animes.Application.Features.Directors.Commands;

public class DeleteDirectorCommand : IRequest<bool>
{
    public int Id { get; set; }
}