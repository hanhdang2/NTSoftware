using System;
using System.Collections.Generic;
using System.Text;

namespace NTSoftware.Service.Interface.ViewModels
{
    public class ProjectDetailViewModel
    {
        public ProjectDetailViewModel(int id, string projectName, string description, DateTime startDate, DateTime? endDate, int companyId, Guid managerId, List<DetailUserViewModel> lstEmployee)
        {
            Id = id;
            ProjectName = projectName;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
            CompanyId = companyId;
            ManagerId = managerId;
            this.lstEmployee = lstEmployee;
        }

        public int Id { get; set; }
        public string ProjectName { set; get; }
        public string Description { set; get; }
        public DateTime StartDate { set; get; }
        public DateTime? EndDate { set; get; }
        public int CompanyId { set; get; }
        public Guid ManagerId { set; get; }
        public List<DetailUserViewModel> lstEmployee { get; set; }
    }
}
