using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NTSoftware.Service.Interface.ViewModels
{
   public class LoginViewModel
    {
        [Required]
        public string UserName { set; get; }
        [Required]
        public string Password { set; get; }
        public bool isSave { set; get; }
    }
}
