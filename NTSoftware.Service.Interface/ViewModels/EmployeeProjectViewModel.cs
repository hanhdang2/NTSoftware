using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NTSoftware.Service.Interface.ViewModels
{
    public class EmployeeProjectViewModel
    {
        public int Id { set; get; }
        [Required]
        public int ProjectId { set; get; }
        public DateTime OutDate { set; get; }
        public DateTime StartDate { set; get; }
    }
}
