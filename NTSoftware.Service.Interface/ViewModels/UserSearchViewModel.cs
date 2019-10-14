using NTSoftware.Core.Models.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace NTSoftware.Service.Interface.ViewModels
{
    public class UserSearchViewModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public Status Status { get; set; }
        public Roles UserType { get; set; }
        public int CompanyId { get; set; }
        public string Position { get; set; }
        public int DepartmentId { get; set; }
        public string ContractNumber { get; set; }
        public string Name { set; get; }
        public string EmployeeKey { get; set; }
        public string IdentityCard { set; get; }
        public DateTime Birthday { set; get; }
        public Gender Gender { set; get; }
        public string PhoneNumber { set; get; }
        public string Address { set; get; }
    }
}
