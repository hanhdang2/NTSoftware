using NTSoftware.Core.Models.DomainEntity;
using NTSoftware.Core.Models.Models.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NTSoftware.Core.Models.Models
{
    public class EmployeeProject : DomainEntity<int>
    {

        public Guid UserID { set; get; }
        public int ProjectId { set; get; }
        public DateTime JoinDate { set; get; }
        public DateTime? OutDate { set; get; }
    }
}
