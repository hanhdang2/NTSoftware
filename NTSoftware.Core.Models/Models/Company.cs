using NTSoftware.Core.Models.DomainEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NTSoftware.Core.Models.Models
{
    [Table("Company")]
    public class Company : DomainEntity<int>
    {
        public string NameCompany { set; get; }
        public string Logo { set; get; }
        public string PhoneNumber { set; get; }
        public string EmailRepresentative { set; get; }
        public string Address { set; get; }
        public string PositionRepresentative { set; get; }
        public string RepresentativeName { set; get; }
       
      

    }
}
