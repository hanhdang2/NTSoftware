
using Microsoft.AspNetCore.Identity;
using NTSoftware.Core.Models.Enum;
using NTSoftware.Core.Models.Models;
using NTSoftware.Core.Models.Models.NTSoftware.Core.Models.Models;
using NTSoftware.Core.Shared.Constants;
using NTSoftware.Core.Shared.Helper;
using NTSoftware.Repository.Interface;
using NTSoftware.Service.Interface.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NTSoftware.Repository.Repository
{
    public class DetailUserRepository : NTRepository<DetailUser, Guid>, IDetailUserRepository
    {
        private AppDbContext _appContext;
        private UserManager<AppUser> _userManager;
        public DetailUserRepository(AppDbContext appContext, UserManager<AppUser> userManager) : base(appContext)
        {
            _appContext = appContext;
            _userManager = userManager;
        }

        public List<UserSearchViewModel> GetLstSearch(List<UserSearchViewModel> lstSelected, int companyId, string keyword)
        {
            try
            {
                var users = _userManager.Users.Where(x => x.CompanyId == companyId && x.DeleteFlag != StatusDelete.DELETED && x.UserType ==Roles.Employee);
                var detailUser = _context.Set<DetailUser>().Where(x => x.DeleteFlag != StatusDelete.DELETED);
                var lstUser = (from u in users
                               join d in detailUser on u.Id equals d.Id
                               select new UserSearchViewModel
                               {
                                   Id = u.Id,
                                   Address = d.Address,
                                   Birthday = d.Birthday,
                                   EmployeeKey = d.EmployeeKey,
                                   Gender = d.Gender,
                                   IdentityCard = d.IdentityCard,
                                   Name = d.Name,
                                   PhoneNumber = d.PhoneNumber,
                                   CompanyId = u.CompanyId,
                                   ContractNumber = u.ContractNumber,
                                   DepartmentId = u.DepartmentId,
                                   Email = u.Email,
                                   Position = u.Position,
                                   Status = u.Status,
                                   UserName = u.UserName,
                                   UserType= u.UserType
                               }).ToList();
                var data = lstUser.Except(lstSelected).Where(x => Utilities.ConvertToUnSign(x.Name).Contains(keyword)).ToList();
                return data;

            }
            catch (Exception ex)
            {
                return new List<UserSearchViewModel>();
            }

        }
    }
}
