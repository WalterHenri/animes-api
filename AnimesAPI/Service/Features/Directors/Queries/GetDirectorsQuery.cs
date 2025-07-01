using Animes.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animes.Application.Features.Directors.Queries
{
    public class GetDirectorsQuery : IRequest<IEnumerable<DirectorDto>>
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
    }
}
