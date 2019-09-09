using NTSoftware.Core.Models.DomainEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NTSoftware.Core.Models.Models
{
    public class EmployeeProject : DomainEntity<Guid>
    {
        public int ProjectId { set; get; }
        public DateTime OutDate { set; get; }
        public DateTime StartDate { set; get; }

    }
}
