using AutoMapper;
using NTSoftware.Core.Models.Models;
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

        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private IDetailUserRepository _detailUserRepository;

        public DetailUserService(IUnitOfWork unitOfWork, IMapper mapper, IDetailUserRepository detailUserRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _detailUserRepository = detailUserRepository;
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

        public DetailUser Add(DetailUserViewModel Vm)
        {
            var entity = _mapper.Map<DetailUser>(Vm);
            _detailUserRepository.Add(entity);
            SaveChanges();
            return entity;
        }

        #endregion POST

        #region PUT

        public void Update(DetailUserViewModel Vm)
        {
            var data = _mapper.Map<DetailUser>(Vm);
            _detailUserRepository.Update(data);
            SaveChanges();
        }

        private void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        #endregion PUT

        #region DELETE

        public void Delete(Guid id)
        {
            var entity = _detailUserRepository.FindById(id);
            entity.DeleteFlag = StatusDelete.DELETED;
            _detailUserRepository.Update(entity);
            SaveChanges();
        }

        #endregion DELETE

        #region OTHER_METHOD

        #endregion OTHER_METHOD
    }
}
