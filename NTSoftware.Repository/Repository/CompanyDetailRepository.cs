using NTSoftware.Core.Models.Models;
using NTSoftware.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace NTSoftware.Repository.Repository
{
    public class CompanyRepository :NTRepository<CompanyDetail, int>,ICompanyRepository
    {
        private AppDbContext _appContext;
        public CompanyRepository(AppDbContext context) : base(context)
        {
            _appContext = context;
        }
    }
}
