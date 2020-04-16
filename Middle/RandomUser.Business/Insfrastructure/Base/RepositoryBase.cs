using Microsoft.EntityFrameworkCore;
using RandomUser.Business.Contract;
using RandomUser.Business.Contract.Repository;
using RandomUser.Business.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace RandomUser.Business.Insfrastructure.Base
{
    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class, IEntity
    {
        protected RepositoryContext RepositoryContext { get; set; }

        public RepositoryBase(RepositoryContext repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }

        public bool Add(TEntity entity)
        {
            RepositoryContext.Set<TEntity>().Add(entity);
            return true;
        }

        public TEntity GetById(int id)
        {
            return RepositoryContext.Set<TEntity>().SingleOrDefault(e => e.Id == id);
        }

        public IQueryable<TEntity> GetAll()
        {
            return RepositoryContext.Set<TEntity>().AsNoTracking();
        }

        public IQueryable<TEntity> GetByCondition(Expression<Func<TEntity, bool>> expression)
        {
            return RepositoryContext.Set<TEntity>().Where(expression).AsNoTracking();
        }

        public bool Update(TEntity entity)
        {
            RepositoryContext.Set<TEntity>().Update(entity);
            return true;
        }

        public bool Remove(TEntity entity)
        {
            RepositoryContext.Set<TEntity>().Remove(entity);
            return true;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
