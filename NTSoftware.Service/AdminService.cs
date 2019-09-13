using AutoMapper;
using NTSoftware.Core.Models.Models;
using NTSoftware.Core.Shared.Dtos;
using NTSoftware.Core.Shared.Interface;
using NTSoftware.Repository;
using NTSoftware.Repository.Interface;
using NTSoftware.Service.Interface;
using NTSoftware.Service.Interface.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NTSoftware.Service
{
   public class AdminService :IAdminService
    {
        #region Contructor
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private IAdminRepository _iadminRepository;
        private readonly AppDbContext _dbContext;

        public AdminService(IUnitOfWork unitOfWork, IMapper mapper, AppDbContext dbContext, IAdminRepository iadminRepo)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _iadminRepository = iadminRepo;
            _dbContext = dbContext;
        }
        #endregion Contructor
        public List<AdminViewModel> GetAll()
        {
            var data = _iadminRepository.FindAll().ToList();
            return _mapper.Map<List<Admin>, List<AdminViewModel>>(data);
        }

        public PagedResult<AdminViewModel> GetAllPaging(int page, int pageSize, string name, int companyId, string cmt, string phonenumber)
        {
            var query = _iadminRepository.Find(x=> x.Name == name && x.CompanyId == companyId && x.CMT == cmt && x.PhoneNumber == phonenumber);
            int totalRow = query.Count();

            try
            {
                var data = _mapper.Map<List<Admin>, List<AdminViewModel>>(query.ToList());

                var paginationSet = new PagedResult<AdminViewModel>()
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

        public AdminViewModel GetById(int id)
        {
            var model = _iadminRepository.FindById(id);
            return _mapper.Map<Admin, AdminViewModel>(model);
        }

        public Admin Add(AdminViewModel Vm)
        {
            var entity = _mapper.Map<Admin>(Vm);
            _iadminRepository.Add(entity);
            SaveChanges();
            return entity;
        }
        public void Update(AdminViewModel Vm)
        {
            var data = _mapper.Map<Admin>(Vm);
            _iadminRepository.Update(data);
            SaveChanges();
        }

        private void SaveChanges()
        {
            _unitOfWork.Commit();
        }
    }
}
