using NTSoftware.Core.Models.DomainEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NTSoftware.Core.Models.Models
{
    public class EmployeeContract : DomainEntity<int>
    {
        public int EmployeeId { set; get; }
        public int RuleId { set; get; }
        public string Source { set; get; }
        public int CompanyId { set; get; }
        public Guid UpdatePersonId { set; get; }
        public DateTime StrartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}
