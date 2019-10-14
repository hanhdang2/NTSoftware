using AutoMapper;
using NTSoftware.Core.Models.Models;
using NTSoftware.Core.Models.Models.NTSoftware.Core.Models.Models;
using NTSoftware.Service.Interface.ViewModels;
using System;
using System.Collections.Generic;

using System.Text;

namespace NTSoftware.Service.AutoMapper
{
   public class AutoMapperProfile :Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ContractCompany, ContractCompanyViewModel>().ReverseMap();
            CreateMap<CompanyDetail, CompanyDetailViewModel>().ReverseMap();
            CreateMap<DetailUser,DetailUserViewModel>().ReverseMap();
            CreateMap<DetailUserViewModel, EmployeeViewModel>().ReverseMap();
            CreateMap<CompanyFullViewModel, EmployeeViewModel>().ReverseMap();
            CreateMap<CompanyFullViewModel, CompanyDetailViewModel>().ReverseMap();
            CreateMap<CompanyFullViewModel, ContractCompanyViewModel>().ReverseMap();

            CreateMap<EmployeeFullViewModel, EmployeeContractViewModel>().ReverseMap();
            CreateMap<EmployeeFullViewModel, DetailUserViewModel>().ReverseMap();

            CreateMap<Project, ProjectViewModel>().ReverseMap();
            CreateMap<Department, DepartmentViewModel>().ReverseMap();
            CreateMap<AppUserViewModel, EmployeeViewModel>().ReverseMap();
            CreateMap<AppUser, AppUserViewModel>().ReverseMap();
            CreateMap<AppUserViewModel, AppUser>().ReverseMap();
            CreateMap<EmployeeContract, EmployeeContractViewModel>().ReverseMap();
            CreateMap<EmployeeProject, EmployeeProjectViewModel>().ReverseMap();
            CreateMap<RuleViewModel, Rule>().ReverseMap();
        }
    }
}
