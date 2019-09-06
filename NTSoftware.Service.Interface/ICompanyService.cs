using NTSoftware.Core.Models.Models;
using NTSoftware.Core.Shared.Dtos;
using NTSoftware.Service.Interface.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace NTSoftware.Service.Interface
{
   public interface ICompanyService
    {
        #region GET
        CompanyViewModel GetById(int id);
        List <CompanyViewModel>GetAll();
        PagedResult<CompanyViewModel> GetAllPaging(int page, int pageSize);
        Company Add(CompanyCreateViewModel vm);
        #endregion GET
    }
}
