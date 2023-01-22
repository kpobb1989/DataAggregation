namespace Sample.Abstractions.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : DbEntity;

        Task CommitAsync(CancellationToken cancellationToken = default);
    }
}
