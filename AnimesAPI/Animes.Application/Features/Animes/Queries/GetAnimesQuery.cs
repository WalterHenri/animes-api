using Animes.Application.DTOs;
using MediatR;

namespace Animes.Application.Features.Animes.Queries
{
    public class GetAnimesQuery : IRequest<IEnumerable<AnimeDto>>
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Director { get; set; }
    }
}
