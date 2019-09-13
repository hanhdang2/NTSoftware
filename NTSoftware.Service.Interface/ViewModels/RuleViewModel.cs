using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NTSoftware.Service.Interface.ViewModels
{
    public class RuleViewModel
    {
        [Required]
        public int Id { set; get; }
        [Required]
        public int CompanyId { set; get; }
        [Required]
        public string Content { set; get; }
    }
}
