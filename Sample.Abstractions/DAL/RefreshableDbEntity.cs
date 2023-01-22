namespace Sample.Abstractions.DAL
{
    public abstract class RefreshableDbEntity : DbEntity
    {
        public DateTime? LastUpdate { get; set; }

        public override bool Equals(object? obj)
            => Equals(obj as DbEntity);

        public abstract bool Equals(DbEntity? other);
        public abstract void Update(DbEntity entity);
    }
}
