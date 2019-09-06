using System;
using System.Collections.Generic;
using System.Text;

namespace NTSoftware.Service.Interface.ViewModels
{
   public class EmployeeContractViewModel
    {
        public int Id { set; get; }
        public int EmployeeId { set; get; }
        public int ProjectId { set; get; }
        public DateTime StrartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
    public class EmployeeCreateContractViewModel
    {
        public int Id { set; get; }
        public int EmployeeId { set; get; }
        public int ProjectId { set; get; }
        public DateTime StrartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}
