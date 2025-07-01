
using Animes.Domain.Entities;

namespace Animes.Domain.Interfaces.Repositories
{
    public interface IDirectorRepository : IRepository<Director, int>
    {
        public Task<IEnumerable<Director>> GetDirectorsAsync(int? id, string? name);
    }

}
