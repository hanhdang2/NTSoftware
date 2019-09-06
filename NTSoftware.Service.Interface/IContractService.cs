using NTSoftware.Core.Models.Models;
using NTSoftware.Core.Shared.Dtos;
using NTSoftware.Service.Interface.ViewModels;
using System;
using System.Collections.Generic;

using System.Text;

namespace NTSoftware.Service.Interface
{
   public interface IContractService
    {
    
        ContractViewModel GetById(int id);
        List<ContractViewModel> GetAll();
        PagedResult<ContractViewModel> GetAllPaging(int page, int pageSize);
        Contract Add(ContractViewModel vm);
    }
}
