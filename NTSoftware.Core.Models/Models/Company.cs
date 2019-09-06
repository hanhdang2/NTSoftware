using NTSoftware.Core.Models.DomainEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NTSoftware.Core.Models.Models
{
    [Table("Company")]
   public class Company:DomainEntity<int>
    {
        public string NameCompany { set; get; }
        public string Logo { set; get; }
        public int PhoneNumber { set; get; }
        public string Email { set; get; }
        public string Address { set; get; }
        public int RepresentativeId { set; get; }

    }
}
