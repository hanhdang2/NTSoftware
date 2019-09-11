using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NTSoftware.Service.Interface.ViewModels
{
  public  class AdminViewModel
    {

        public int Id { set; get; }
        [Required]
        public string Name { set; get; }
        [MaxLength(50)]
        public string EmployeeKey { set; get; }
        [MaxLength(50)]
        public string CMT { set; get; }
        public DateTime Birthday { set; get; }
        public string Gender { set; get; }
        [MaxLength(100)]
        public string Address { set; get; }
        [Required]
        public string PhoneNumber { set; get; }
        [MaxLength(256)]
        public string Email { set; get; }
        [Required]
        public int CompanyId { set; get; }
    }
}
