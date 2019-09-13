using NTSoftware.Core.Models.DomainEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NTSoftware.Core.Models.Models
{
    [Table("Project")]
    public class Project :DomainEntity<int>
    {
        public string ProjectName { set; get; }
        public string Describe { set; get; }
        public DateTime StartDate { set; get; }
        public DateTime EndDate { set; get; }
        public int CompanyId { set; get; }
        public int ManagerId { set; get; }
        
    }
}
