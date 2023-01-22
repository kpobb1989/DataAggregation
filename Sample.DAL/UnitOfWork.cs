using Sample.Abstractions.DAL;
using Sample.Abstractions.DAL.Interfaces;

namespace Sample.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SampleDbContext _dbContext;

        private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();

        public UnitOfWork(SampleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : DbEntity
        {
            var type = typeof(TEntity);

            if (!_repositories.ContainsKey(type))
            {
                _repositories[type] = new Repository<TEntity>(_dbContext);
            }

            return (IRepository<TEntity>)_repositories[type];
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
            => await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
