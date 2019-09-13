using NTSoftware.Core.Models.DomainEntity;
using NTSoftware.Core.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NTSoftware.Core.Models.Models
{  public class ContractCompany: DomainEntity<int>
    {
        public string NumberContract { set; get; }
        public Status Status { set; get; }
        public int RuleId { set; get; }
        public int CreatorId { set; get; }
        public int CompanyId { set; get; }
        public DateTime StartDate { set; get; }
        public DateTime EndDate { get; set; }
       

    }
}
