using AutoMapper;
using Microsoft.AspNetCore.Identity;
using NTSoftware.Core.Models.Models;
using NTSoftware.Core.Models.Models.NTSoftware.Core.Models.Models;
using NTSoftware.Core.Shared.Constants;
using NTSoftware.Core.Shared.Dtos;
using NTSoftware.Core.Shared.Helper;
using NTSoftware.Core.Shared.Interface;
using NTSoftware.Repository;
using NTSoftware.Repository.Interface;
using NTSoftware.Service.Interface;
using NTSoftware.Service.Interface.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace NTSoftware.Service
{
    public class DetailUserService : IDetailUserService
    {
        #region CONTRUCTOR

        private readonly IMapper _mapper;
        private IDetailUserRepository _detailUserRepository;
        private UserManager<AppUser> _userManager;

        public DetailUserService(IMapper mapper, IDetailUserRepository detailUserRepository, UserManager<AppUser> userManager)
        {
            _mapper = mapper;
            _detailUserRepository = detailUserRepository;
            _userManager = userManager;
        }

        #endregion CONTRUCTOR

        #region GET

        public DetailUserViewModel GetById(Guid id)
        {
            var model = _detailUserRepository.FindById(id);
            return _mapper.Map<DetailUser, DetailUserViewModel>(model);
        }

        #endregion GET

        #region POST

        public DetailUser Add(DetailUserViewModel Vm, string companyCode, int companyId)
        {
            var entity = _mapper.Map<DetailUser>(Vm);
            var lstUser = _userManager.Users.Where(x => x.CompanyId == companyId).ToList();
            entity.EmployeeKey = $"NV{companyCode}{lstUser.Count + 1}";
            _detailUserRepository.Add(entity);
            return entity;
        }

        #endregion POST

        #region PUT

        public void Update(DetailUserViewModel Vm)
        {
            var data = _mapper.Map<DetailUser>(Vm);
            _detailUserRepository.Update(data);
        }

        #endregion PUT

        #region DELETE

        public void Delete(Guid id)
        {
            var entity = _detailUserRepository.FindById(id);
            entity.DeleteFlag = StatusDelete.DELETED;
            _detailUserRepository.Update(entity);
        }

        public List<UserSearchViewModel> GetUserSelect(List<UserSearchViewModel> lstVm, int companyId, string keyword)
        {
            var data = _detailUserRepository.GetLstSearch(lstVm, companyId, keyword);
            return data;
        }

        #endregion DELETE

        #region OTHER_METHOD

        #endregion OTHER_METHOD
    }
}
