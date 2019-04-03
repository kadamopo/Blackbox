using System;
using System.Linq;
using System.Linq.Expressions;

namespace ServiceBusQueueClient.DAL
{
    public interface IRepository<TEntity>
    {
        IQueryable<TEntity> GetAll();
        TEntity GetById(int id);
        TEntity GetById(string id);
        IQueryable<TEntity> SearchFor(Expression<Func<TEntity, bool>> predicate);
        void Update(TEntity entity);
        void Insert(TEntity entity);
        void Delete(TEntity entity);
    }
}
