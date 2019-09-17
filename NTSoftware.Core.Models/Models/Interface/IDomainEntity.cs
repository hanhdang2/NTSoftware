using System;
using System.Collections.Generic;
using System.Text;

namespace NTSoftware.Core.Models.Models.Interface
{
   public interface IDomainEntity
    {
        Guid CreatedBy { get; set; }
        Guid UpdatedBy { get; set; }
        DateTime CreatedDate { get; set; }
        DateTime UpdatedDate { get; set; }
        int DeleteFlag { get; set; }
    }
}
