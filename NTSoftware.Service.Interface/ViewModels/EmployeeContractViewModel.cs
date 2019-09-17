using NTSoftware.Core.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NTSoftware.Service.Interface.ViewModels
{
   public class EmployeeContractViewModel
    {
        public int Id { set; get; }
        [Required]
        public string ContractNumber { set; get; }
        public string Source { get; set; }
        public int EmployeeId { set; get; }
        public int RuleId { set; get; }
        public DateTime StrartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Status Status { get; set; }
        public int CompanyId { set; get; }
    }
}
