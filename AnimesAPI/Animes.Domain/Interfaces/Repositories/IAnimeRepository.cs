
using Animes.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animes.Domain.Interfaces.Repositories
{
    public interface IAnimeRepository : IRepository<Anime, int>
    {
        public Task<IEnumerable<Anime>> GetAnimesAsync(int? id, string? name, string? director);
    }
    
}
