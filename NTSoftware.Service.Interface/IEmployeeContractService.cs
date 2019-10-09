using NTSoftware.Core.Models.Enum;
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
        #region GET

        EmployeeContractDetailViewModel GetById(int id);
        PagedResult<EmployeeContractViewModel> GetAllPaging(int page, int pageSize, Status status);

        #endregion GET

        #region POST

        EmployeeContract Add(EmployeeContractDetailViewModel vm);

        #endregion POST

        #region PUT



        #endregion PUT

    }
}
