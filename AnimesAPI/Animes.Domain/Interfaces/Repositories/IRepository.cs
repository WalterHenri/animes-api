namespace Animes.Domain.Interfaces.Repositories
{
    public interface IRepository<TEntity, TPrimaryKey> where TEntity : class where TPrimaryKey : struct
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetAllAsNoTrackingAsync();
        Task<TEntity?> GetByIdAsync(TPrimaryKey id);
        Task<TEntity> CreateAsync(TEntity entity);
        Task<TEntity?> UpdateAsync(TEntity entity);
        Task<bool> DeleteAsync(TPrimaryKey id);
    }
}
