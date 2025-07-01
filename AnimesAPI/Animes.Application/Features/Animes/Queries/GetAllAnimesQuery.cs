using Animes.Application.DTOs;
using MediatR;

namespace Animes.Application.Features.Animes.Queries;

public class GetAllAnimesQuery : IRequest<IEnumerable<AnimeDto>>
{
}