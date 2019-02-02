using Application.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Application.Core.DataAccess
{
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        T Get(Expression<Func<T, bool>> filter = null);
        List<T> GetList(Expression<Func<T, bool>> filter = null, 
            Func<List<T>, List<T>> orderBy = null,
            int page = 1, 
            int pageSize = 0, 
            params Expression<Func<T, object>>[] includedProperties);

        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
