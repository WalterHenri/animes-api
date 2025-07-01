using Animes.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animes.Application.Features.Animes.Commands
{
    public class CreateAnimeCommand : IRequest<AnimeDto>
    {
        public required string Name { get; set; }
        public required string Summary { get; set; }
        public int? DirectorId { get; set; }
    }
}

