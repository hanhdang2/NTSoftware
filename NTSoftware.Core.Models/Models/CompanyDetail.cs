using NTSoftware.Core.Models.DomainEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NTSoftware.Core.Models.Models
{
    [Table("CompanyDetail")]
    public class CompanyDetail : DomainEntity<int>
    {
        public string CompanyName { set; get; }
        public string PhoneNumber { set; get; }
        public string Logo { set; get; }

        public string EmailRepresentative { set; get; }
        public string RepresentativeName { set; get; }
        public string PositionRepresentative { set; get; }
        public string Address  { set; get; }
    }
}
