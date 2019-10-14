using NTSoftware.Core.Models.Enum;
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
        #region GET

        ContractCompanyViewModel GetById(int id);
        PagedResult<ContractCompanyViewModel> GetAllPaging(int page,int companyId, int pageSize, Status status);

        #endregion GET

        #region POST

        ContractCompany Add(ContractCompanyViewModel vm, string companyCode);

        #endregion POST

        #region PUT

        void Update(ContractCompanyViewModel vm);

        #endregion PUT

        #region DELETE

        void Delete(int id);

        #endregion DELETE

    }
}
