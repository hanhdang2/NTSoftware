using NTSoftware.Core.Models.DomainEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NTSoftware.Core.Models.Models
{
    [Table("Department")]
    public class Department :DomainEntity<int>
    {
        public string Name { set; get; }
        public int PhoneNumber { set; get; }
        public string Address { set; get; }
        public string Describe { set; get; }
        public int CompanyId { set; get; }

    }
}
