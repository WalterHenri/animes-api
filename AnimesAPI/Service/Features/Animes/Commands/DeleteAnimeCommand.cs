using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animes.Application.Features.Animes.Commands
{
    public class DeleteAnimeCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
