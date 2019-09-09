using NTSoftware.Core.Models.Models;
using NTSoftware.Core.Shared.Dtos;
using NTSoftware.Service.Interface.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace NTSoftware.Service.Interface
{
   public interface IEmployeeProjectService
    {
     
        EmployeeProjectViewModel GetById(Guid id);
        List<EmployeeProjectViewModel> GetAll();
        PagedResult<EmployeeProjectViewModel> GetAllPaging(int page, int pageSize);
        EmployeeProject Add(EmployeeProjectViewModel vm);
    }
}
