using NTSoftware.Core.Models.Models;
using NTSoftware.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace NTSoftware.Repository.Repository
{
    public class EmployeeProjectRepository : NTRepository<EmployeeProject, int>, IEmployeeProjectRepository
    {
        private AppDbContext _appContext;
        public EmployeeProjectRepository(AppDbContext context) : base(context)
        {
            _appContext = context;
        }
    }
}
