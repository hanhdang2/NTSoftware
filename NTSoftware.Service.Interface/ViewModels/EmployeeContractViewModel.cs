using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NTSoftware.Service.Interface.ViewModels
{
   public class EmployeeContractViewModel
    {
        public int Id { set; get; }
        [Required]
        public string EmployeeKey { set; get; }
        [Required]
        public string Name { set; get; }
        [Required]
        public string CMT { get; set; }
        public DateTime Birthday { get; set; }
        public string Gender { get; set; }
        [Required]
        public string Address { set; get; }
        [Required]
        public int PhoneNumber { set; get; }
        [Required]
        public string Email { set; get; }
        public int DepartmentId { set; get; }
        [Required]
        public string Position { set; get; }
    }
}
