using NTSoftware.Core.Models.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace NTSoftware.Service.Interface.ViewModels
{
    public class EmployeeFullViewModel
    {
        public int Id { get; set; }
        public string ContractNumber { set; get; }
        public string EmailRepresentativeA { set; get; }
        public string RepresentativeNameA { set; get; }
        public string PositionRepresentativeA { set; get; }
        public string EmailRepresentativeB { set; get; }
        public string RepresentativeNameB { set; get; }
        public string ContentRule { get; set; }
        public Guid EmployeeId { set; get; }
        public DateTime StrartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Status Status { get; set; }
        public int CompanyId { set; get; }
        public decimal SalaryContract { get; set; }
        public EmployeeViewModel EmployeeViewModel { get; set; }
    }
}
