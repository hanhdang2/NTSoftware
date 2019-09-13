using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NTSoftware.Service.Interface.ViewModels
{
    public class ProjectViewModel
    {
        public int Id { set; get; }
        [Required]
        public string ProjectName { set; get; }
        [Required]
        public string Describe { set; get; }
        public DateTime StartDate { set; get; }
        public DateTime EndDate { set; get; }
        [Required]
        public int CompanyId { set; get; }
        [Required]
        public int ManagerId { set; get; } 
    }
}
