using Microsoft.EntityFrameworkCore;

using Sample.Abstractions.DAL;
using Sample.Abstractions.DAL.Interfaces;

using System.Linq.Expressions;

namespace Sample.DAL
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : DbEntity
    {
        private readonly SampleDbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(SampleDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }
        public IQueryable<TEntity> Get(
            Expression<Func<TEntity, bool>>? filter = null,
            string includeProperties = "",
            Expression<Func<TEntity, object>>? orderBy = null,
            bool asNoTracking = false)
        {
            IQueryable<TEntity> query = _dbSet;

            if (asNoTracking)
            {
                query = query.AsNoTrackingWithIdentityResolution();
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = query.OrderBy(orderBy);
            }

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var propertyName in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(propertyName);
                }
            }

            return query;
        }

        public void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            foreach (TEntity entity in entities)
            {
                _dbSet.Add(entity);
            }
        }

        public void Update(TEntity entity)
        {
            if (_dbContext.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }

            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Remove(Expression<Func<TEntity, bool>> filter)
        {
            var entities = Get(filter);

            RemoveRange(entities);
        }

        public void Remove(TEntity entity)
        {
            if (_dbContext.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }

            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                if (_dbContext.Entry(entity).State == EntityState.Detached)
                {
                    _dbSet.Attach(entity);
                }
            }

            _dbSet.RemoveRange(entities);
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>>? filter = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.CountAsync();
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
            => await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
