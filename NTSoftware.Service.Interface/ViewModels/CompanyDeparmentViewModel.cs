using System;
using System.Collections.Generic;
using System.Text;

namespace NTSoftware.Service.Interface.ViewModels
{
  public class CompanyDeparmentViewModel
    {
        public int CompanyId { set; get; }
        public DepartmentViewModel department { get; set; }
    }
}
