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
        List<ContractCompanyViewModel> GetAll();
        PagedResult<ContractCompanyViewModel> GetAllPaging(int page, int pageSize, Status status);

        #endregion GET

        #region POST

        ContractCompany Add(ContractCompanyViewModel vm);

        #endregion POST

        #region PUT

        void Update(ContractCompanyViewModel vm);

        #endregion PUT



    }
}
