using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NTSoftware.Service.Interface.ViewModels
{
    public class DepartmentViewModel
    {
        public int Id { set; get; }
        [Required]
        public string DepartmentName { set; get; }
        public string PhoneNumber { set; get; }
        public string Email { set; get; }
        public string Address { set; get; }
        public string Description { set; get; }
        public Guid ManagerId { set; get; }
        public int CompanyId { set; get; }
    }
 
}
