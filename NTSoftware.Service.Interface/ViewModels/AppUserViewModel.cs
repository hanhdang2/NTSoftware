using NTSoftware.Core.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NTSoftware.Service.Interface.ViewModels
{
    public class AppUserViewModel
    {
        [Required]
        public string Password { get; set; }
        public Roles UserType { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string EmailCompany { set; get; }
        public Status Status { set; get; }
        [Required]
        public int UserEmployeeID { get; set; }
        [Required]
        public int UserAdminId { get; set; }

    }
}
