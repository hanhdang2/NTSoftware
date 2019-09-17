using NTSoftware.Core.Models.Models;
using NTSoftware.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace NTSoftware.Repository.Repository
{
    public class EmployeeContractRepository : NTRepository<EmployeeContract, int>, IEmployeeContractRepository
    {
        private AppDbContext _appContext;
        public EmployeeContractRepository(AppDbContext context) : base(context)
        {
            _appContext = context;
        }
    }
}
