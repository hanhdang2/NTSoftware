using NTSoftware.Core.Models.Models;
using NTSoftware.Core.Shared.Dtos;
using NTSoftware.Service.Interface.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace NTSoftware.Service.Interface
{
    public interface IDepartmentService
    {
        #region GET
        DepartmentViewModel GetById(int id);
        List<DepartmentViewModel> GetAll();
        PagedResult<DepartmentViewModel> GetAllPaging(int page, int pageSize);
        Department Add(DepartmentViewModel vm);
       
        #endregion GET
    }
}
