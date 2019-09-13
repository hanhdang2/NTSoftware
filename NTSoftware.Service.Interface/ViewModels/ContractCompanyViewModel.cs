using NTSoftware.Core.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NTSoftware.Service.Interface.ViewModels
{
    public class ContractCompanyViewModel
    {

        public int Id { set; get; }
        [Required]
        public string NumberContract { set; get; }
        public Status Status { set; get; }
        [Required]
        public int RuleId { set; get; }
        [Required]
        public int CreatorId { set; get; }
        [Required]
        public int CompanyId { set; get; }
        public DateTime StartDate { set; get; }
        public DateTime EndDate { get; set; }

    }
  
}
