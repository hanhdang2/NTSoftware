using System;
using System.Collections.Generic;
using System.Text;

namespace NTSoftware.Service.Interface.ViewModels
{
   public class LoginViewModel
    {
        public string UserName { set; get; }
        public string Password { set; get; }
        public bool isSave { set; get; }
    }
}
