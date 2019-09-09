using System;
using System.Collections.Generic;
using System.Text;

namespace NTSoftware.Service.Interface.ViewModels
{
    public class ContractCompanyViewModel
    {
        public int Id { set; get; }
        public int RuleId { set; get; }
        public int Source { set; get; }
        public Guid UpdatePersonId { set; get; }
        public int CompanyID { set; get; }
        public int EmployeeId { set; get; }
        public int UserId { set; get; }
        public DateTime StartDate { set; get; }
        public DateTime EndDate { get; set; }

    }
    public class ContractCompanyCreateViewModel
    {
        public int RuleId { set; get; }
        public int Source { set; get; }
        public Guid UpdatePersonId { set; get; }
        public int CompanyID { set; get; }
        public int EmployeeId { set; get; }
        public int UserId { set; get; }
        public DateTime StartDate { set; get; }
        public DateTime EndDate { get; set; }

    }
}
