namespace Test.Core
{
    public interface IRepository<TEntity> : IReadOnlyRepository<TEntity>
    {
        TEntity Add(TEntity entity);

        bool Update(TEntity entity);

        bool Delete(TEntity entity);
    }
}