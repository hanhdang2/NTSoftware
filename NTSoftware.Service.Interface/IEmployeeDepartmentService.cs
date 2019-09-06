using NTSoftware.Core.Shared.Dtos;
using NTSoftware.Service.Interface.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace NTSoftware.Service.Interface
{
   public interface IEmployeeDepartmentService
    {
        #region GET
        EmployeeDepartmentViewModel GetById(int id);
        List<EmployeeDepartmentViewModel> GetAll();
        PagedResult<EmployeeDepartmentViewModel> GetAllPaging(int page, int pageSize);
        #endregion GET
    }
}
