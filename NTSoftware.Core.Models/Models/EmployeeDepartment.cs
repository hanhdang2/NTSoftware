using NTSoftware.Core.Models.DomainEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NTSoftware.Core.Models.Models
{
    [Table("EmployeeDapartment")]
    public class EmployeeDepartment:DomainEntity<int>
    {
        public int EmployeeId { set; get; }
        public int DapartmentId { set; get; }
        public DateTime StartDate { set; get; }
        public DateTime EndDate { set; get; }
    }
}
