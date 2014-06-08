using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Test.Core
{
    public interface IRepository<TEntity>
    {
        TEntity Add(TEntity entity);

        bool Update(TEntity entity);

        bool Delete(TEntity entity);

        TEntity Get(Expression<Func<TEntity, bool>> filter);

        ICollection<TEntity> GetAll();
    }
}