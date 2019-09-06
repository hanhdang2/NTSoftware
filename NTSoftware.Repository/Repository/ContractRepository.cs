using NTSoftware.Core.Models.Models;
using NTSoftware.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace NTSoftware.Repository.Repository
{
   public class ContractRepository :NTRepository<Contract,int>,IContractRepository
    {
        private AppDbContext _appContext;
        public ContractRepository(AppDbContext context) : base(context)
        {
            _appContext = context;
        }
    }
}
