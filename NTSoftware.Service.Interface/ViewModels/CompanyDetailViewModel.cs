using System;
using System.Collections.Generic;
using System.Text;

namespace NTSoftware.Service.Interface.ViewModels
{
    public class CompanyDetailViewModel
    {
        public int Id { get; set; }
        public string CompanyName { set; get; }
        public string PhoneNumber { set; get; }
        public string Logo { set; get; }

        public string EmailRepresentative { set; get; }
        public string RepresentativeName { set; get; }
        public string PositionRepresentative { set; get; }
        public string Address { set; get; }
        public Guid CreatedBy { get; set; }
        public Guid UpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
