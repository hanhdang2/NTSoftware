using System;
using System.Collections.Generic;
using System.Text;

namespace NTSoftware.Service.Interface.ViewModels
{
    public class DetailDepartmentViewModel
    {
        public DetailDepartmentViewModel()
        {

        }

        public int Id { get; set; }
        public string DepartmentName { set; get; }
        public string PhoneNumber { set; get; }
        public string Email { set; get; }
        public string Address { set; get; }
        public string Description { set; get; }
        public Guid ManagerId { set; get; }
        public int CompanyId { set; get; }
        public List<AppUserViewModel> lstEmployee { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid UpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int DeleteFlag { get; set; }
    }
}
