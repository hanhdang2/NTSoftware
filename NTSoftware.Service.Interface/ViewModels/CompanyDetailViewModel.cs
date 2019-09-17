using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NTSoftware.Service.Interface.ViewModels
{
   public class CompanyDetailViewModel
    {
        
        public int Id { set; get; }
        [Required]
        public string CompanyName { set; get; }
        public string PhoneNumber { set; get; }
        public string Logo { set; get; }

        public string EmailRepresentative { set; get; }
        public string RepresentativeName { set; get; }
        public string PositionRepresentative { set; get; }
        public string Address { set; get; }
    }
   
}
