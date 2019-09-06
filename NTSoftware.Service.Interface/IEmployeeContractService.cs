using NTSoftware.Core.Models.Models;
using NTSoftware.Core.Shared.Dtos;
using NTSoftware.Service.Interface.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace NTSoftware.Service.Interface
{
   public interface IEmployeeContractService
    {
      
        EmployeeContractViewModel GetById(int id);
        List<EmployeeContractViewModel> GetAll();
        PagedResult<EmployeeContractViewModel> GetAllPaging(int page, int pageSize);
        EmployeeContract Add(EmployeeContractViewModel vm);
    }
}
