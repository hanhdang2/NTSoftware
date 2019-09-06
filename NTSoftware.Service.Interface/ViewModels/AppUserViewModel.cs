using NTSoftware.Core.Models.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace NTSoftware.Service.Interface.ViewModels
{
    public class AppUserViewModel
    {
        public int UserId { set; get; }
        public string UserName { set; get; }
        public string Status { set; get; }
        public string Email { set; get; }
        public string Password { get; set; }
        public string UserType { get; set; }
    }
}
