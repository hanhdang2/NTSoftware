using NTSoftware.Core.Models.Models;
using NTSoftware.Core.Shared.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace NTSoftware.Repository.Interface
{
   public interface IEmployeeProjectRepository 
    {
        void Add(EmployeeProject entity);
        void AddRange(IEnumerable<EmployeeProject> entities);

        void Update(EmployeeProject entity);
        void UpdateRange(IEnumerable<EmployeeProject> entities);

        void RemoveFlg(EmployeeProject entity);
        void RemoveRange(IEnumerable<EmployeeProject> entities);
        void Remove(int id, Guid userId);

        int Count();

        IEnumerable<EmployeeProject> Find(Expression<Func<EmployeeProject, bool>> predicate = null);
        IEnumerable<EmployeeProject> GetAll();

        IQueryable<EmployeeProject> FindAll(params Expression<Func<EmployeeProject, object>>[] includeProperties);

        IQueryable<EmployeeProject> FindAll(Expression<Func<EmployeeProject, bool>> predicate, params Expression<Func<EmployeeProject, object>>[] includeProperties);
    }
}
