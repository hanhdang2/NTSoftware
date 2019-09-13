using NTSoftware.Core.Models.DomainEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NTSoftware.Core.Models.Models
{
    [Table("Employees")]
   public class Employee :DomainEntity<int>
    {
        public string EmployeeKey { set; get; }
        public string Name { set; get; }
        public string CMT { get; set; }
        public DateTime Birthday { get; set; }
        public string Gender { get; set; }
        public string Address { set; get; }
        public int PhoneNumber { set; get; }
        public string Email { set; get; }
        public int DepartmentId { set; get; }
        public string Position { set; get; }

    }
}
