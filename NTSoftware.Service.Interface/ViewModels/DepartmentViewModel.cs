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
        public string Name { set; get; }
        [Required]
        public int PhoneNumber { set; get; }
        [Required]
        public string Address { set; get; }
        [Required]
        public string Describe { set; get; }
        public int CompanyId { set; get; }
        [Required]
        public string Email { set; get; }
        public int ManagerId { set; get; }
    }
}
