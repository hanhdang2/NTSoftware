using NTSoftware.Core.Models.Models;
using NTSoftware.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace NTSoftware.Repository.Repository
{
    public class DepartmentRepository : NTRepository<Department, int>, IDepartmentRepository
    {
        private AppDbContext _appContext;
        public DepartmentRepository(AppDbContext context) : base(context)
        {
            _appContext = context;
        }
    }
}
