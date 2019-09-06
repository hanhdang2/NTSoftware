using NTSoftware.Core.Models.Models;
using NTSoftware.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace NTSoftware.Repository.Repository
{
    public class EmployeeRepository: NTRepository<Employee,int>,IEmployeeRepository
    {
        private AppDbContext _appContext;
        public EmployeeRepository(AppDbContext context) : base(context)
        {
            _appContext = context;
        }
    }
}
