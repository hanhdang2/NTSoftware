using NTSoftware.Core.Models.DomainEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NTSoftware.Core.Models.Models
{  
    [Table("EmployeeContract")]
    public class EmployeeContract:DomainEntity<int>
    {
        public int EmployeeId { set; get; }
        public int ProjectId { set; get; }
        public DateTime StrartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
