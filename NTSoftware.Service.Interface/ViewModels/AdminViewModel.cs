using System;
using System.Collections.Generic;
using System.Text;

namespace NTSoftware.Service.Interface.ViewModels
{
  public  class AdminViewModel
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string EmployeeKey { set; get; }
        public string CMT { set; get; }
        public DateTime Birthday { set; get; }
        public string Gender { set; get; }
        public string Address { set; get; }
        public string PhoneNumber { set; get; }
        public string Email { set; get; }
        public int CompanyId { set; get; }
    }
}
