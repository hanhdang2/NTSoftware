using NTSoftware.Core.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NTSoftware.Service.Interface.ViewModels
{
    public class ContractCompanyViewModel
    {
       
        public string ContractNumber { set; get; }
        public int RuleId { set; get; }
        public DateTime StartDate { set; get; }
        public DateTime? EndDate { get; set; }
        public Status Status { set; get; }
        public int CompanyId { set; get; }

    }
  
}
