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
        [Required]
        public string CMT { set; get; }
        public DateTime Birthday { set; get; }
        public string Gender { set; get; }
        [Required]
        public string Address { set; get; }
        [Required]
        public string PhoneNumber { set; get; }
        [Required]
        public int CompanyId { set; get; }
    }
}
