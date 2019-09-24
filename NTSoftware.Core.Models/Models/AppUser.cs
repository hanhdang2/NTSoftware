using Microsoft.AspNetCore.Identity;
using NTSoftware.Core.Models.DomainEntity;
using NTSoftware.Core.Models.Enum;
using NTSoftware.Core.Models.Models.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace NTSoftware.Core.Models.Models
{

    namespace NTSoftware.Core.Models.Models
    {
        public class AppUser : IdentityUser<Guid>, IDomainEntity
        {

            public Status Status { get; set; }
            public Roles UserType { get; set; }
            public int CompanyId { get; set; }
            public string Position { get; set; }
            public int DepartmentId { get; set; }
            public bool IsLockedOut => this.LockoutEnabled && this.LockoutEnd >= DateTimeOffset.UtcNow;
            public Guid CreatedBy { get; set; }
            public Guid UpdatedBy { get; set; }
            public DateTime CreatedDate { get; set; }
            public DateTime UpdatedDate { get; set; }   
            public int DeleteFlag { get; set; }
            // <summary>
            /// Navigation property for the roles this user belongs to.
            /// </summary>
            public virtual ICollection<IdentityUserRole<Guid>> Roles { get; set; }

            /// <summary>
            /// Navigation property for the claims this user possesses.
            /// </summary>
            public virtual ICollection<IdentityUserClaim<Guid>> Claims { get; set; }

        }
    }

}
