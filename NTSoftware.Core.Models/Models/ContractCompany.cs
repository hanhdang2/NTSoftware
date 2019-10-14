using NTSoftware.Core.Models.DomainEntity;
using NTSoftware.Core.Models.Enum;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NTSoftware.Core.Models.Models
{
    public class ContractCompany : DomainEntity<int>
    {
        public string EmailRepresentativeA { set; get; }
        public string RepresentativeNameA { set; get; }
        public string PositionRepresentativeA { set; get; }
        public string EmailRepresentativeB { set; get; }
        public string RepresentativeNameB { set; get; }
        public string PositionRepresentativeB { set; get; }
        public string Address { set; get; }
        public string ContractNumber { set; get; }
        [Column(TypeName = "text")]
        public string ContentRule { set; get; }
        public DateTime StartDate { set; get; }
        public DateTime? EndDate { get; set; }
        public Status Status { set; get; }
        public int CompanyId { set; get; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal PriceContract { get; set; }
    }
}
