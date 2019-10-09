using Microsoft.EntityFrameworkCore;
using NTSoftware.Core.Models.Models;
using NTSoftware.Core.Shared.Constants;
using NTSoftware.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace NTSoftware.Repository.Repository
{
    public class EmployeeProjectRepository : IEmployeeProjectRepository, IDisposable
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<EmployeeProject> _entities;

        public EmployeeProjectRepository(AppDbContext context)
        {
            _context = context;
            _entities = context.Set<EmployeeProject>();
        }
        public void Add(EmployeeProject entity)
        {
            _entities.Add(entity);
        }

        public void AddRange(IEnumerable<EmployeeProject> entities)
        {
            _entities.AddRange(entities);
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

        public IEnumerable<EmployeeProject> Find(Expression<Func<EmployeeProject, bool>> predicate = null)
        {
            return _entities.Where(predicate).Where(x => x.DeleteFlag != StatusDelete.DELETED);
        }

        public IQueryable<EmployeeProject> FindAll(params Expression<Func<EmployeeProject, object>>[] includeProperties)
        {
            IQueryable<EmployeeProject> items = _context.Set<EmployeeProject>();
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    items = items.Include(includeProperty);
                }
            }
            return items.Where(x => x.DeleteFlag != StatusDelete.DELETED);
        }

        public IQueryable<EmployeeProject> FindAll(Expression<Func<EmployeeProject, bool>> predicate, params Expression<Func<EmployeeProject, object>>[] includeProperties)
        {
            IQueryable<EmployeeProject> items = _context.Set<EmployeeProject>();
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    items = items.Include(includeProperty);
                }
            }
            return items.Where(predicate).Where(x => x.DeleteFlag != StatusDelete.DELETED);
        }

       
        public IEnumerable<EmployeeProject> GetAll()
        {
            return _entities.ToList();
        }
        public void Remove(int id, Guid userId)
        {
            throw new NotImplementedException();
        }

        public void RemoveFlg(EmployeeProject entity)
        {
            entity.DeleteFlag = StatusDelete.DELETED;
            _entities.Update(entity);
        }

        public void RemoveRange(IEnumerable<EmployeeProject> entities)
        {
            foreach (var item in entities)
            {
                item.DeleteFlag = StatusDelete.DELETED;
                _entities.Update(item);
            }
        }

        public void Update(EmployeeProject entity)
        {
            _entities.Update(entity);
        }

        public void UpdateRange(IEnumerable<EmployeeProject> entities)
        {
            _entities.UpdateRange(entities);
        }
    }
}
