using System;
using System.Collections.Generic;
using System.Text;

namespace NTSoftware.Core.Models.Models.Interface
{
   public interface IDomainEntity
    {
        string CreatedBy { get; set; }
        string UpdatedBy { get; set; }
        DateTime CreatedDate { get; set; }
        DateTime UpdatedDate { get; set; }
    }
}
