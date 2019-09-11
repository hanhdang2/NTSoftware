using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NTSoftware.Service.Interface
{
  public class ChangePasswordViewModel
    {
        [Required]
        public Guid Id { get; set; }
        [MaxLength(10)]
        public string OldPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }

    }
}
