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

        EmployeeContractViewModel GetById(int id);
        PagedResult<EmployeeContractViewModel> GetAllPaging(int page, int pageSize,int companyId, Status status);

        #endregion GET

        #region POST

        EmployeeContract Add(EmployeeContractViewModel vm, string companyCode);

        #endregion POST

        #region PUT

        void Update(EmployeeContractViewModel vm);

        #endregion PUT

        #region 

        void Delete(int id);

        #endregion DELETE

    }
}
