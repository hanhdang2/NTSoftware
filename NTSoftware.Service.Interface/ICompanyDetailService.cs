using NTSoftware.Core.Models.Models;
using NTSoftware.Core.Shared.Dtos;
using NTSoftware.Service.Interface.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NTSoftware.Service.Interface
{
    public interface ICompanyDetailService
    {
        #region GET
        GenericResult GetById(int id);
        GenericResult CheckCompanyExpried(int id);
        PagedResult<CompanyDetailViewModel> GetAllPaging(int page, int pageSize, string namecompany, string phonenumber, string address, string representativename, string positionrepresentative);

        #endregion GET

        #region POST

        GenericResult Add(CompanyDetailViewModel Vm);

        #endregion POST

        #region PUT

        GenericResult Update(CompanyDetailViewModel Vm);

        #endregion PUT

        #region DELETE

        GenericResult DeleteCompany(int id);

        #endregion DELETE
    }
}
