using NTSoftware.Core.Models.DomainEntity;
using NTSoftware.Core.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NTSoftware.Core.Models.Models
{
    public class EmployeeContract : DomainEntity<int>
    {
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
