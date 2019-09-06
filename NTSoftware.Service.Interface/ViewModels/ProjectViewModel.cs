using System;
using System.Collections.Generic;
using System.Text;

namespace NTSoftware.Service.Interface.ViewModels
{
    public class ProjectViewModel
    {
        public string ProjectName { set; get; }
        public string Describe { set; get; }
        public DateTime StartDate { set; get; }
        public DateTime EndDate { set; get; }
        public int ManagerId { set; get; }
        public int DepartmentId { set; get; }
    }
    public class ProjectCreateViewModel
    {
        public string ProjectName { set; get; }
        public string Describe { set; get; }
        public DateTime StartDate { set; get; }
        public DateTime EndDate { set; get; }
        public int ManagerId { set; get; }
        public int DepartmentId { set; get; }
    }
}
