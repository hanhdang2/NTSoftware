using Microsoft.AspNetCore.Identity;
using NTSoftware.Core.Models.Models.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace NTSoftware.Core.Models.Models
{
    public class AppRole : IdentityRole, IDomainEntity
    {
        public AppRole()
        {

        }
        public AppRole(string roleName) : base(roleName)
        {

        }
        public AppRole(string roleName, string description) : base(roleName)
        {
            Description = description;
        }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public virtual ICollection<IdentityUserRole<string>> Users { get; set; }

        /// <summary>
        /// Navigation property for claims in this role.
        /// </summary>
        public virtual ICollection<IdentityRoleClaim<string>> Claims { get; set; }
    }
}
