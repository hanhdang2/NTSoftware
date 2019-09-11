using NTSoftware.Core.Models.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace NTSoftware.Service.Interface.ViewModels
{
    public class AppUserViewModel
    {
        public string Password { get; set; }
        public Roles UserType { get; set; }
        public string UserName { get; set; }
        public string Email { set; get; }
        public Status Status { set; get; }
        public int UserEmployeeID { get; set; }
        public int UserAdminId { get; set; }

    }
}
