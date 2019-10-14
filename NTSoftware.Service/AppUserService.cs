
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NTSoftware.Core.Models.Models;
using NTSoftware.Core.Models.Models.NTSoftware.Core.Models.Models;
using NTSoftware.Core.Shared;
using NTSoftware.Core.Shared.Constants;
using NTSoftware.Core.Shared.Dtos;
using NTSoftware.Core.Shared.Interface;
using NTSoftware.Repository;
using NTSoftware.Repository.Interface;
using NTSoftware.Service.Interface;
using NTSoftware.Service.Interface.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace NTSoftware.Service
{
    public class AppUserService : IAppUserService
    {
        #region CONTRUCTOR

        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _dbContext;

        public AppUserService(IMapper mapper, AppDbContext dbContext, UserManager<AppUser> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _dbContext = dbContext;
        }
        #endregion CONTRUCTOR

        #region GET

        public async Task<AppUserViewModel> GetById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                return null;
            }
            var userVm = _mapper.Map<AppUser, AppUserViewModel>(user);
            return userVm;
        }
        public async Task<AppUserViewModel> GetByUserName(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var userVm = _mapper.Map<AppUser, AppUserViewModel>(user);
            return userVm; 
        }
        public PagedResult<AppUserViewModel> GetAllPaging(int page, int pageSize)
        {

            var query = _userManager.Users.ToList();
            int totalRow = query.Count();

            var data = _mapper.Map<List<AppUser>, List<AppUserViewModel>>(query).Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var paginationSet = new PagedResult<AppUserViewModel>()
            {
                Results = data,
                CurrentPage = page,
                RowCount = totalRow,
                PageSize = pageSize
            };
            return paginationSet;

        }
        public async Task<List<AppUserViewModel>> GetAllAsync()
        {
            return await _userManager.Users.ProjectTo<AppUserViewModel>().ToListAsync();
        }

        #endregion GET 

        #region POST

        public async Task<AppUserViewModel> AddAsync(AppUserViewModel userVm)
        {
            var user = _mapper.Map<AppUserViewModel, AppUser>(userVm);
            user.Id = new Guid();
            var data = await _userManager.CreateAsync(user, userVm.PasswordHash);
            if (data.Succeeded)
            {
                return _mapper.Map<AppUser, AppUserViewModel>(user);
            }
            return null;
        }

        #endregion POST

        #region PUT

        public async Task UpdateAsync(AppUserViewModel Vm)
        {
            var user = _mapper.Map<AppUser>(Vm);
            await _userManager.UpdateAsync(user);
        }
        #endregion PUT

        #region DELETE

        public async Task DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            user.DeleteFlag = StatusDelete.DELETED;
            await _userManager.UpdateAsync(user);
        }

        #endregion DELETE

        #region OTHER_METHOD

        public GenericResult CheckUserExits(AppUserViewModel vm)
        {
            var user = _userManager.FindByEmailAsync(vm.Email);
            if (user == null)
            {
                return new GenericResult(null, false, ErrorMsg.NOT_EXIST_EMAIL, ErrorCode.ERROR_CODE);
            }
            return null;
        }

        public async Task RemoveDepartment( int departmentId)
        {
            var lstUser = _userManager.Users.Where(x => x.DepartmentId == departmentId && x.DeleteFlag == StatusDelete.NON_DELETED).ToList();
            foreach (var item in lstUser)
            {
                var user = _mapper.Map<AppUser>(item);
                user.DepartmentId = -1;
                await _userManager.UpdateAsync(user);
            }
        }

        public async Task AddDepartment(List<AppUserViewModel> lstVm)
        {
            foreach (var item in lstVm)
            {
                var user = _mapper.Map<AppUser>(item);
                user.DepartmentId = item.DepartmentId;
                await _userManager.UpdateAsync(user);
            }
        }

        #endregion OTHER_METHOD
    }
}
