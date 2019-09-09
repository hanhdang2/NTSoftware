using NTSoftware.Core.Models.Models;
using NTSoftware.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace NTSoftware.Repository.Repository
{
   public class ContractCompanyRepository :NTRepository<ContractCompany,int>,IContractCompanyRepository
    {
        private AppDbContext _appContext;
        public ContractCompanyRepository(AppDbContext context) : base(context)
        {
            _appContext = context;
        }
    }
}
