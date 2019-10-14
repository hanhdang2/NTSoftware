using System;
using System.Collections.Generic;
using System.Text;

namespace NTSoftware.Service.Interface.ViewModels
{
    public class CompanyFullViewModel
    {
        public int Id { get; set; }
        public string CompanyName { set; get; }
        public string PhoneNumber { set; get; }
        public string Logo { set; get; }
        public string CompanyCode { get; set; }
        public string EmailRepresentative { set; get; }
        public string RepresentativeName { set; get; }
        public string PositionRepresentative { set; get; }
        public string Address { set; get; }
        public EmployeeViewModel EmployeeVm { get; set; }
        public ContractCompanyViewModel ContractVm { get; set; }
    }
}
