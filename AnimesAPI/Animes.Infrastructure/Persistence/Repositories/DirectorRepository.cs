using Animes.Domain.Entities;
using Animes.Domain.Interfaces.Repositories;
using Animes.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Animes.Infrastructure.Persistence.Repositories;

public class DirectorRepository : IDirectorRepository
{
    private readonly AnimeDbContext _context;

    public DirectorRepository(AnimeDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Director>> GetDirectorsAsync(int? id, string? name)
    {
        var query = _context.Directors.AsQueryable();

        if (id.HasValue)
        {
            query = query.Where(a => a.Id == id.Value);
        }

        if (!string.IsNullOrEmpty(name))
        {
            query = query.Where(a => a.Name.Contains(name));
        }

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<Director>> GetAllAsync()
    {
        return await _context.Directors.ToListAsync();
    }

    public async Task<Director?> GetByIdAsync(int id)
    {
        return await _context.Directors.FindAsync(id);
    }

    public async Task<Director> CreateAsync(Director entity)
    {
        await _context.Directors.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<Director?> UpdateAsync(Director entity)
    {
        _context.Directors.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var director = await GetByIdAsync(id);
        if (director == null) return false;

        _context.Directors.Remove(director);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Director>> GetAllAsNoTrackingAsync()
    {
        return await _context.Directors.AsNoTracking().ToListAsync();
    }
}