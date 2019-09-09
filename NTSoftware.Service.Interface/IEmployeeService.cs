using NTSoftware.Core.Models.Models;
using NTSoftware.Core.Shared.Dtos;
using NTSoftware.Service.Interface.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace NTSoftware.Service.Interface
{
   public interface IEmployeeService
    {
        EmployeeViewModel GetById(int id);
        List<EmployeeViewModel> GetAll();
        PagedResult<EmployeeViewModel> GetAllPaging(int page, int pageSize);
        Employee Add(EmployeeViewModel vm);
        void Update(EmployeeViewModel vm);
    }
}
