using NTSoftware.Core.Models.DomainEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace NTSoftware.Core.Models.Models
{
   public class Rule: DomainEntity<int>
    {
        public int CompanyId { set; get; }
        public string Content { set; get; }
        
    }
}
