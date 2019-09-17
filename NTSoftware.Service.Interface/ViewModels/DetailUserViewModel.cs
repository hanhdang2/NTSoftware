using NTSoftware.Core.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NTSoftware.Service.Interface.ViewModels
{
  public  class DetailUserViewModel
    {
        public string Id { set; get; }
        [Required]
        public string Name { set; get; }
        public string EmployeeKey { get; set; }
        public string IdentityCard { set; get; }
        public DateTime Birthday { set; get; }
        public Gender Gender { set; get; }
        public string PhoneNumber { set; get; }
        public string Address { set; get; }
    }
}
