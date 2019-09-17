
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NTSoftware.Core.Models.Models.NTSoftware.Core.Models.Models;
using NTSoftware.Core.Shared.Dtos;
using NTSoftware.Core.Shared.Interface;
using NTSoftware.Repository;
using NTSoftware.Service.Interface;
using NTSoftware.Service.Interface.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NTSoftware.Service
{
    public class AppUserService :IAppUserService
    {
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _dbContext;

        public AppUserService(IUnitOfWork unitOfWork, IMapper mapper, AppDbContext dbContext, UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _dbContext = dbContext;
        }

        public async Task<AppUserViewModel> GetById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var roles = await _userManager.GetRolesAsync(user);
            var userVm = _mapper.Map<AppUser, AppUserViewModel>(user);

            return userVm;
        }

        public PagedResult<AppUserViewModel> GetAllPaging(int page, int pageSize)
        {

            var query = _userManager.Users.ToList();
               

            int totalRow = query.Count();

            try
            {
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
            catch
            {
                return null;
            }
        }
        public void UpdateAsync(AppUserViewModel Vm)
        {
            var user = _mapper.Map<AppUser>(Vm);
            _userManager.UpdateAsync(user);
            SaveChanges();
        }

        private void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public async Task<List<AppUserViewModel>> GetAllAsync()
        {
            return await _userManager.Users.ProjectTo<AppUserViewModel>().ToListAsync();
        }

        public async Task<bool> AddAsync(AppUserViewModel userVm)
        {
            var user = _mapper.Map<AppUserViewModel, AppUser>(userVm);
            user.Id = new Guid();
            var data = await _userManager.CreateAsync(user, userVm.PasswordHash);
            var result = (data.Succeeded) ? true : false;
            return result;
        }
    }
}
