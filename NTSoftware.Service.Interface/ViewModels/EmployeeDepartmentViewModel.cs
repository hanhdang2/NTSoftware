using System;
using System.Collections.Generic;
using System.Text;

namespace NTSoftware.Service.Interface.ViewModels
{
    public class EmployeeDepartmentViewModel
    {
        public int Id { set; get; }
        public int EmployeeId { set; get; }
        public int DapartmentId { set; get; }
        public DateTime StartDate { set; get; }
        public DateTime EndDate { set; get; }
    }
    public class EmployeeCreateDepartmentViewModel
    {
        public int EmployeeId { set; get; }
        public int DapartmentId { set; get; }
        public DateTime StartDate { set; get; }
        public DateTime EndDate { set; get; }
    }
}
