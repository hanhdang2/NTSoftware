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
        public string Description { set; get; }
        public DateTime StartDate { set; get; }
        public DateTime? EndDate { set; get; }
        public int CompanyId { set; get; }
        public Guid ManagerId { set; get; }
    }
}
