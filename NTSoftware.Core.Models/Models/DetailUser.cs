using NTSoftware.Core.Models.DomainEntity;
using NTSoftware.Core.Models.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace NTSoftware.Core.Models.Models
{
    public class DetailUser : DomainEntity<int>
    {
        public string Name { set; get; }
        public string EmployeeKey { get; set; }
        public string IdentityCard { set; get; }
        public DateTime Birthday { set; get; }
        public Gender Gender { set; get; }
        public string PhoneNumber { set; get; }
        public string Address { set; get; }
    }

}
