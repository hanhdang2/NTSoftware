using System;
using System.Collections.Generic;
using System.Text;

namespace NTSoftware.Service.Interface.ViewModels
{
   public class EmployeeViewModel 
    {
        public int Id { set; get; }
        public int EmployeeKey { set; get; }
        public string Name { set; get; }
        public string CMT { get; set; }
        public DateTime Birthday { get; set; }
        public string Gender { get; set; }
        public string Address { set; get; }
        public int PhoneNumber { set; get; }
        public string Email { set; get; }
        public int DepartmentId { set; get; }
    }

}
