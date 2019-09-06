using NTSoftware.Core.Models.Models;
using NTSoftware.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace NTSoftware.Repository.Repository
{
   public class EmployeeDepartmentRepository :NTRepository<EmployeeDepartment,int>,IEmployeeDepartmentRepository
    {
        private AppDbContext _appContext;
        public EmployeeDepartmentRepository(AppDbContext context) : base(context)
        {
            _appContext = context;
        }
    }
}
