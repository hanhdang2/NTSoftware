using NTSoftware.Core.Models.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace NTSoftware.Service.Interface.ViewModels
{
    public class ContractCompanyViewModel
    {
        public int Id { get; set; }
        public string EmailRepresentativeA { set; get; }
        public string RepresentativeNameA { set; get; }
        public string PositionRepresentativeA { set; get; }
        public string EmailRepresentativeB { set; get; }
        public string RepresentativeNameB { set; get; }
        public string PositionRepresentativeB { set; get; }
        public string Address { set; get; }
        public string ContractNumber { set; get; }
        public string ContentRule { set; get; }
        public DateTime StartDate { set; get; }
        public DateTime? EndDate { get; set; }
        public Status Status { set; get; }
        public int CompanyId { set; get; }
        public decimal PriceContract { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid UpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
