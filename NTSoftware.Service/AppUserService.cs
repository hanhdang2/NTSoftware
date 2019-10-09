
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

        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private IDetailUserRepository _detailUserRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _dbContext;

        public AppUserService(IUnitOfWork unitOfWork, IMapper mapper, AppDbContext dbContext, UserManager<AppUser> userManager, IDetailUserRepository detailUserRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _dbContext = dbContext;
            _detailUserRepository = detailUserRepository;
        }

        #endregion CONTRUCTOR

        #region GET

        public async Task<GenericResult> GetById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                return new GenericResult(null, false, ErrorMsg.NOT_EXIST_EMAIL, ErrorCode.ERROR_CODE);
            }
            var userVm = _mapper.Map<AppUser, AppUserViewModel>(user);
            return new GenericResult(userVm, true, ErrorMsg.NOT_EXIST_EMAIL, ErrorCode.ERROR_CODE);
        }

        public PagedResult<AppUserViewModel> GetAllPaging(int page, int pageSize)
        {

            var query = _userManager.Users.ToList();
            int totalRow = query.Count();

            var data = _mapper.Map<List<AppUser>, List<AppUserViewModel>>(query);

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
            var result = (data.Succeeded) ? true : false;
            return result == true ? userVm : null;
        }

        #endregion POST

        #region PUT

        public async Task UpdateAsync(AppUserViewModel Vm)
        {
            var user = _mapper.Map<AppUser>(Vm);
            await _userManager.UpdateAsync(user);
            SaveChanges();
        }

        private void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        #endregion PUT

        #region DELETE

        public async Task DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            user.DeleteFlag = StatusDelete.DELETED;
            await _userManager.UpdateAsync(user);
            SaveChanges();
        }

        #endregion DELETE

        #region PRIVATE_METHOD

        public GenericResult CheckUserExits(AppUserViewModel vm)
        {
            var user = _userManager.FindByEmailAsync(vm.Email);
            if (user == null)
            {
                return new GenericResult(null, false, ErrorMsg.NOT_EXIST_EMAIL, ErrorCode.ERROR_CODE);
            }
            return null;
        }

        #endregion PRIVATE_METHOD
    }
}
