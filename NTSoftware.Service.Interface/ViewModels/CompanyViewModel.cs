using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NTSoftware.Service.Interface.ViewModels
{
   public class CompanyViewModel
    {
        
        public int Id { set; get; }
        [Required]
        public string NameCompany { set; get; }
        public string Logo { set; get; }
        [Required]
        public string PhoneNumber { set; get; }
        [Required]
        public string EmailRepresentative { set; get; }
        [Required]
        public string Address { set; get; }
        [Required]
        public string PositionRepresentative { set; get; }
        [Required]
        public string RepresentativeName { set; get; }
    }
   
}
