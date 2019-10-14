using NTSoftware.Core.Models.Models;
using NTSoftware.Core.Shared.Dtos;
using NTSoftware.Service.Interface.ViewModels;

namespace NTSoftware.Service.Interface
{
    public interface ICompanyDetailService
    {
        #region GET
        CompanyDetailViewModel GetById(int id);
        GenericResult CheckCompanyExpried(int id);
        PagedResult<CompanyDetailViewModel> GetAllPaging(int page, int pageSize, string namecompany, string phonenumber, string address, string representativename, string positionrepresentative);

        #endregion GET

        #region POST

        CompanyDetail Add(CompanyDetailViewModel Vm);

        #endregion POST

        #region PUT

        void Update(CompanyDetailViewModel Vm);

        #endregion PUT

        #region DELETE

        void DeleteCompany(int id);

        #endregion DELETE
    }
}
