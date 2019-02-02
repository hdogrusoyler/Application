using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Application.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Application.Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext
    {
        private TContext _dbContext;
 
        public EfEntityRepositoryBase(TContext dbContext) 
        {
            _dbContext = dbContext;
        }
 
        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            return _dbContext.Set<TEntity>().SingleOrDefault(filter);
        }

        public List<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null, 
            Func<List<TEntity>, List<TEntity>> orderBy = null, 
            int page = 1, 
            int pageSize = 0, 
            params Expression<Func<TEntity, object>>[] includedProperties)
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();

            if (filter != null)
            { query = query.Where(filter); }

            foreach (var includeProperty in includedProperties)
            { query = query.Include(includeProperty); }

            if (pageSize > 0)
            { query = query.Take(pageSize).Skip((page - 1) * pageSize); }

            if (orderBy != null)
            { return orderBy(query.ToList()); }

            else
            { return query.ToList(); }
        }
 
        public void Add(TEntity entity)
        {
            var addedEntity = _dbContext.Entry(entity);
            addedEntity.State = EntityState.Added;
            _dbContext.SaveChanges();
        }
 
        public void Update(TEntity entity)
        {
            var updatedEntity = _dbContext.Entry(entity);
            updatedEntity.State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
 
        public void Delete(TEntity entity)
        {
            var deleteEntity = _dbContext.Entry(entity);
            deleteEntity.State = EntityState.Deleted;
            _dbContext.SaveChanges();
        }
        
    }
}
