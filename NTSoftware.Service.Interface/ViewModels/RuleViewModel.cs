using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NTSoftware.Service.Interface.ViewModels
{
    public class RuleViewModel
    {

        public int Id { set; get; }
        [Required]
        public string Content { set; get; }
        public int CompanyId { set; get; }

    }
}
