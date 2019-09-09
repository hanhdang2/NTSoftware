using System;
using System.Collections.Generic;
using System.Text;

namespace NTSoftware.Service.Interface.ViewModels
{
    public class ProjectViewModel
    {
        public int Id { set; get; }
        public string ProjectName { set; get; }
        public string Describe { set; get; }
        public DateTime StartDate { set; get; }
        public DateTime EndDate { set; get; }
        public int CompanyId { set; get; }
    }
    public class ProjectCreateViewModel
    {
        public string ProjectName { set; get; }
        public string Describe { set; get; }
        public DateTime StartDate { set; get; }
        public DateTime EndDate { set; get; }
        public int CompanyId { set; get; }
    }
}
