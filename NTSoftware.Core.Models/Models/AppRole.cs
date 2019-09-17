using Microsoft.AspNetCore.Identity;
using NTSoftware.Core.Models.Models.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace NTSoftware.Core.Models.Models
{
    public class AppRole : IdentityRole<Guid>, IDomainEntity
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
        public Guid CreatedBy { get; set; }
        public Guid UpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int DeleteFlag { get; set; }
        public virtual ICollection<IdentityUserRole<Guid>> Users { get; set; }

        /// <summary>
        /// Navigation property for claims in this role.
        /// </summary>
        public virtual ICollection<IdentityRoleClaim<Guid>> Claims { get; set; }

    }
}
