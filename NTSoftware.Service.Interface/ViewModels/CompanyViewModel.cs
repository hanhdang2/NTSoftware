using System;
using System.Collections.Generic;
using System.Text;

namespace NTSoftware.Service.Interface.ViewModels
{
   public class CompanyViewModel
    {
        public int Id { set; get; }
        public string NameCompany { set; get; }
        public string Logo { set; get; }
        public int PhoneNumber { set; get; }
        public string Email { set; get; }
        public string Address { set; get; }
        public int RepresentativeId { set; get; }
    }
    public class CompanyCreateViewModel
    {
        public string NameCompany { set; get; }
        public string Logo { set; get; }
        public int PhoneNumber { set; get; }
        public string Email { set; get; }
        public string Address { set; get; }
        public int RepresentativeId { set; get; }
    }
}
