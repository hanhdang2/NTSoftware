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
        public class AppUser : IdentityUser<Guid>,IDomainEntity
        {
            public Status Status { set; get; }
            public string Password { get; set; }
            public string Token { get; set; }
            public Roles UserType { get; set; }
            public bool IsLockedOut => this.LockoutEnabled && this.LockoutEnd >= DateTimeOffset.UtcNow;
            public string CreatedBy { get; set; }
            public string UpdatedBy { get; set; }
            public DateTime CreatedDate { get; set; }
            public DateTime UpdatedDate { get; set; }
            public int UserEmployeeID { get; set; }
            public int UserAdminId { get; set; }
            public string EmailCompany { set; get; }
         
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
