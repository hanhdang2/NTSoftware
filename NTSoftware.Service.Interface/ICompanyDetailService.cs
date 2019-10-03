using NTSoftware.Core.Models.Models;
using NTSoftware.Core.Shared.Dtos;
using NTSoftware.Service.Interface.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace NTSoftware.Service.Interface
{
    public interface ICompanyDetailService
    {
        #region GET
        CompanyDetailViewModel GetById(int id);
        List<CompanyDetailViewModel> GetAll();
        PagedResult<CompanyDetailViewModel> GetAllPaging(int page, int pageSize, string namecompany, string phonenumber, string address, string representativename, string positionrepresentative);
        ContractCompanyModel GetCompanyContract(int id);
        #endregion GET

        #region POST
       
        CompanyDetail Add(CompanyDetailViewModel Vm);
       
        #endregion POST

        #region PUT

        void Update(CompanyDetailViewModel Vm);

        #endregion PUT
       
    }
}
