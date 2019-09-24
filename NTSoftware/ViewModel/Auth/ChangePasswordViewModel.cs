using NTSoftware.Core.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NTSoftware.ViewModel.Auth
{
    public class ChangePasswordViewModel
    {
        public string UserId { get; set; }
        public string TokenCode { get; set; }
        [Required]
        public string NewPassword { get; set; }
    }
}
