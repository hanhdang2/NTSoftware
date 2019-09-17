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
        #region GET

        EmployeeProjectViewModel GetById(int id);
        List<EmployeeProjectViewModel> GetAll();
        PagedResult<EmployeeProjectViewModel> GetAllPaging(int page, int pageSize);

        #endregion GET

        #region POST

        EmployeeProject Add(EmployeeProjectViewModel vm);

        #endregion POST

        #region PUT



        #endregion PUT



    }
}
