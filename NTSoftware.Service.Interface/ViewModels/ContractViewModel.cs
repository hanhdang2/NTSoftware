using System;
using System.Collections.Generic;
using System.Text;

namespace NTSoftware.Service.Interface.ViewModels
{
    public class ContractViewModel
    {
        public int Id { set; get; }
        public int ContractNumber { set; get; }
        public string Content { set; get; }
        public string Status { set; get; }
        public int CompanyID { set; get; }
        public int EmployeeId { set; get; }
        public int UserId { set; get; }
        public DateTime StartDate { set; get; }
        public DateTime EndDate { get; set; }
    }
    public class ContractCreateViewModel
    {
        public int ContractNumber { set; get; }
        public string Content { set; get; }
        public string Status { set; get; }
        public int CompanyID { set; get; }
        public int EmployeeId { set; get; }
        public int UserId { set; get; }
        public DateTime StartDate { set; get; }
        public DateTime EndDate { get; set; }
    }
}
