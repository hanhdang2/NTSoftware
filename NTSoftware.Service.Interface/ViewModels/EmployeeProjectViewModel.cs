using System;
using System.Collections.Generic;
using System.Text;

namespace NTSoftware.Service.Interface.ViewModels
{
    public class EmployeeProjectViewModel
    {
        public int Id { set; get; }
        public int ProjectId { set; get; }
        public DateTime OutDate { set; get; }
        public DateTime StartDate { set; get; }
    }
    public class EmployeeProjectCreateDepartmentViewModel
    {
        public int ProjectId { set; get; }
        public DateTime OutDate { set; get; }
        public DateTime StartDate { set; get; }
    }
}
