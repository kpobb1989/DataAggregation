using System.Linq.Expressions;

namespace Sample.Abstractions.DAL.Interfaces
{
    public interface IRepository<TEntity> where TEntity : DbEntity
    {
        IQueryable<TEntity> GetQueryable(
                    Expression<Func<TEntity, bool>>? filter = null,
                    string includeProperties = "",
                    Expression<Func<TEntity, object>>? orderBy = null,
                    bool asNoTracking = false);
        void Add(TEntity entity);

        void AddRange(IEnumerable<TEntity> entities);

        void Update(TEntity entity);

        void Remove(Expression<Func<TEntity, bool>> filter);

        void Remove(TEntity entity);

        void RemoveRange(IEnumerable<TEntity> entities);

        Task CommitsAsync(CancellationToken cancellationToken = default);
    }
}
