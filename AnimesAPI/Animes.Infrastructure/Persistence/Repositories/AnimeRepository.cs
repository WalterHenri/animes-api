using Animes.Domain.Entities;
using Animes.Domain.Interfaces.Repositories;
using Animes.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;


namespace Animes.Infrastructure.Persistence.Repositories
{
    public class AnimeRepository(AnimeDbContext context) : IAnimeRepository
    {

        private readonly AnimeDbContext context = context;

        public async Task<IEnumerable<Anime>> GetAllAsync()
        {
            return await context.Animes.Include(a => a.Director).ToListAsync();
        }

        public async Task<Anime?> GetByIdAsync(int id)
        {
            return await context.Animes.Include(a => a.Director).FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Anime>> GetAnimesAsync(int? id, string? name, string? director)
        {
            var query = context.Animes.Include(a => a.Director).AsQueryable();

            if (id.HasValue)
            {
                query = query.Where(a => a.Id == id.Value);
            }

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(a => a.Name.Contains(name));
            }

            if (!string.IsNullOrEmpty(director))
            {
                query = query.Where(a => a.Director != null && a.Director.Name.Contains(director));
            }

            return await query.ToListAsync();
        }

        public async Task<Anime> CreateAsync(Anime entity)
        {
            _ = await context.Directors.FindAsync(entity.DirectorId)
                ?? throw new Exception("The directorId is null or doesnt exist in our database");
            await context.Animes.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<Anime?> UpdateAsync(Anime entity)
        {
            context.Animes.Update(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var anime = await GetByIdAsync(id);
            if (anime == null) return false;

            context.Animes.Remove(anime);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Anime>> GetAllAsNoTrackingAsync()
        {
            return await context.Animes.Include(a => a.Director).AsNoTracking().ToListAsync();
        }
    }
}
