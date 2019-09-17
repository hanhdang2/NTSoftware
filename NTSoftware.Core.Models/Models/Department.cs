using NTSoftware.Core.Models.DomainEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NTSoftware.Core.Models.Models
{
    [Table("Department")]
    public class Department : DomainEntity<int>
    {
        public string DepartmentName { set; get; }
        public string PhoneNumber { set; get; }
        public string Email { set; get; }
        public string Address { set; get; }
        public string Description { set; get; }
        public Guid ManagerId { set; get; }
        public int CompanyId { set; get; }
    }
}
