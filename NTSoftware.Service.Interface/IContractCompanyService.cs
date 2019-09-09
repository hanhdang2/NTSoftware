using NTSoftware.Core.Models.Models;
using NTSoftware.Core.Shared.Dtos;
using NTSoftware.Service.Interface.ViewModels;
using System;
using System.Collections.Generic;

using System.Text;

namespace NTSoftware.Service.Interface
{
   public interface IContractCompanyService
    {
    
        ContractCompanyViewModel GetById(int id);
        List<ContractCompanyViewModel> GetAll();
        PagedResult<ContractCompanyViewModel> GetAllPaging(int page, int pageSize);
        ContractCompany Add(ContractCompanyViewModel vm);
        void Update(ContractCompanyViewModel vm);
    }
}
