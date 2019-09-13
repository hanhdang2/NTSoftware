using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NTSoftware.Service.Interface.ViewModels
{
    public class ResetViewModel
    {
        [Required]
        public string Email { set; get; }
        [Required]
        public string code { set; get; }
        [Required]
        public string password { set; get; }
    }
}
