using System;
using System.Linq;
using System.Linq.Expressions;

namespace RandomUser.Business.Contract.Repository
{
    public interface IRepositoryBase<TEntity> : IDisposable where TEntity : class, IEntity
    {
        TEntity GetById(int id);
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetByCondition(Expression<Func<TEntity, bool>> expression);
        bool Add(TEntity entity);
        bool Update(TEntity entity);
        bool Remove(TEntity entity);
    }
}
