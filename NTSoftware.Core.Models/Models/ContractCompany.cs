using NTSoftware.Core.Models.DomainEntity;
using NTSoftware.Core.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NTSoftware.Core.Models.Models
{
    public class ContractCompany : DomainEntity<int>
    {
        public string ContractNumberS { set; get; }
        public int RuleId { set; get; }
        public DateTime StartDate { set; get; }
        public DateTime? EndDate { get; set; }
        public Status Status { set; get; }
        public int CompanyId { set; get; }
    }
}
