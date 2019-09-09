using NTSoftware.Core.Models.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace NTSoftware.Service.Interface.ViewModels
{
    public class AppUserViewModel
    {
        public int UserId { set; get; }
        public DateTime EndDate { get; set; }
        public string Status { set; get; }
        public string Password { get; set; }
        public Roles UserType { get; set; }
        public int UserEmployeeID { get; set; }
        public int UserAdminId { get; set; }
        public string Token { get; set; }
    }
}
