using Microsoft.EntityFrameworkCore;
using NTSoftware.Core.Models.DomainEntity;
using NTSoftware.Core.Shared.Constants;
using NTSoftware.Core.Shared.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace NTSoftware.Repository
{
    public class NTRepository<TEntity, K> : IRepository<TEntity, K>, IDisposable where TEntity : DomainEntity<K>
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<TEntity> _entities;

        public NTRepository(AppDbContext context)
        {
            _context = context;
            _entities = context.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            _entities.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _entities.AddRange(entities);
        }

        public void Update(TEntity entity)
        {
            _entities.Update(entity);
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            _entities.UpdateRange(entities);
        }

        public int Count()
        {
            return _entities.Count();
        }

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate = null)
        {
            return _entities.Where(predicate).Where(x => x.DeleteFlag != StatusDelete.DELETED);
        }

        public IQueryable<TEntity> FindAll(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> items = _context.Set<TEntity>();
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    items = items.Include(includeProperty);
                }
            }
            return items.Where(x => x.DeleteFlag != StatusDelete.DELETED);
        }

        public IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> items = _context.Set<TEntity>();
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    items = items.Include(includeProperty);
                }
            }
            return items.Where(predicate).Where(x => x.DeleteFlag != StatusDelete.DELETED);
        }

        public TEntity FindById(K id, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return FindAll(includeProperties).SingleOrDefault(x => x.Id.Equals(id));
        }

        public TEntity FindSingle(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return FindAll(includeProperties).SingleOrDefault(predicate);
        }

        public TEntity Get(int id)
        {
            return _entities.Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _entities.ToList();
        }

        public TEntity GetSingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.SingleOrDefault(predicate);
        }

        public void Remove(K id)
        {
            RemoveFlg(FindById(id));
        }

        public void RemoveFlg(TEntity entity)
        {
            entity.DeleteFlag = StatusDelete.DELETED;
            _entities.Update(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _entities.RemoveRange(entities);
        }



    }
}
