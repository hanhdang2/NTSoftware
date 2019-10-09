using NTSoftware.Core.Models.DomainEntity;
using NTSoftware.Core.Models.Models.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NTSoftware.Core.Models.Models
{
    public class EmployeeProject
    {
        public Guid UserID { set; get; }
        public int ProjectId { set; get; }
        public DateTime JoinDate { set; get; }
        public DateTime? OutDate { set; get; }
        public Guid CreatedBy { get; set; }
        public Guid UpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int DeleteFlag { get; set; }
    }
}
