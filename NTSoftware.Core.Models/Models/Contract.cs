using NTSoftware.Core.Models.DomainEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NTSoftware.Core.Models.Models
{ [Table("Contracts")]
   public class Contract: DomainEntity<int>
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
