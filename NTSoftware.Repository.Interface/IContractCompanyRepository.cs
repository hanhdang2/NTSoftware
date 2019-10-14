using NTSoftware.Core.Models.Models;
using NTSoftware.Core.Shared.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace NTSoftware.Repository.Interface
{
    public interface IContractCompanyRepository : IRepository<ContractCompany, int>
    {
        string GetLastestContractNumber(int companyId);
    }
}
