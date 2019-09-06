using System;
using System.Collections.Generic;
using System.Text;

namespace NTSoftware.Service.Interface.ViewModels
{
    public class DepartmentViewModel
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public int PhoneNumber { set; get; }
        public string Email { set; get; }
        public string Address { set; get; }
        public string Describe { set; get; }
        public int ManagerId { set; get; }
        public int CompanyId { set; get; }
    }
    public class DepartmentCreateViewModel
    {
        public string Name { set; get; }
        public int PhoneNumber { set; get; }
        public string Email { set; get; }
        public string Address { set; get; }
        public string Describe { set; get; }
        public int ManagerId { set; get; }
        public int CompanyId { set; get; }
    }
}
