using NTSoftware.Core.Models.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace NTSoftware.Service.Interface.ViewModels
{
    public class UserCompanyDetailViewModel
    {
        public string CompanyName { set; get; }
        public string PhoneNumber { set; get; }
        public string EmailRepresentative { set; get; }
        public string RepresentativeName { set; get; }
        public string PositionRepresentative { set; get; }
        public string Address { set; get; }

        public string UserName { get; set; }
        public string Password { set; get; }
        public Status Status { get; set; }
        public Roles UserType { get; set; }
        public string Position { get; set; }
        public int DepartmentId { get; set; }
        public List<UserDetailView> UserDetail { set; get; }
    }
    public class UserDetailView
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string EmployeeKey { get; set; }
        public string IdentityCard { set; get; }
        public DateTime Birthday { set; get; }
        public Gender Gender { set; get; }
        public string PhoneNumber { set; get; }
        public string Address { set; get; }
    }
}
