using NTSoftware.Core.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NTSoftware.Service.Interface.ViewModels
{
    public class EmployeeViewModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        [Required]
        public int CompanyId { get; set; }
        public Guid? Id { get; set; }
        [Required]
        public Status Status { get; set; }
        public Roles UserType { get; set; }

        public Guid? CreatedBy { get; set; }

        public Guid? UpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string Name { set; get; }
        public string EmployeeKey { get; set; }
        public string IdentityCard { set; get; }
        public DateTime Birthday { set; get; }
        public Gender Gender { set; get; }
        public string Address { set; get; }
    }
}
