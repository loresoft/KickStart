using System.Linq.Expressions;

namespace Test.Core;

public interface IReadOnlyRepository<TEntity>
{
    TEntity Get(Expression<Func<TEntity, bool>> filter);
    ICollection<TEntity> GetAll();
}