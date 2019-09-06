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
            CreateMap<Contract, ContractViewModel>().ReverseMap();
            CreateMap<Company, CompanyViewModel>().ReverseMap();
            CreateMap<Employee,EmployeeViewModel>().ReverseMap();
            CreateMap<Project, ProjectViewModel>().ReverseMap();
            CreateMap<Department, DepartmentViewModel>().ReverseMap();
            CreateMap<AppUser, AppUserViewModel>().ReverseMap();
            CreateMap<AppUserViewModel, AppUser>().ReverseMap();

            CreateMap<EmployeeContract, EmployeeContractViewModel>().ReverseMap();
            CreateMap<EmployeeDepartment, EmployeeDepartmentViewModel>().ReverseMap();
        }
    }
}
