using NTSoftware.Core.Models.DomainEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NTSoftware.Core.Models.Models
{  public class ContractCompany: DomainEntity<int>
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
