using AutoMapper;
using Microsoft.AspNetCore.Identity;
using NTSoftware.Core.Models.Models;
using NTSoftware.Core.Models.Models.NTSoftware.Core.Models.Models;
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
using System.Text;
using System.Threading.Tasks;

namespace NTSoftware.Service
{
    public class DepartmentService : IDepartmentService
    {

        #region CONTRUCTOR

        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private IDetailUserRepository _detailUserRepository;
        private IDepartmentRepository _idepartmentRepository;
        private readonly AppDbContext _dbContext;
        public DepartmentService(IUnitOfWork unitOfWork, IMapper mapper, AppDbContext dbContext, IDepartmentRepository idepartmentRepo, UserManager<AppUser> userManager, IDepartmentRepository idepartmentRepository, IDetailUserRepository detailUserRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _idepartmentRepository = idepartmentRepo;
            _dbContext = dbContext;
            _userManager = userManager;
            _idepartmentRepository = idepartmentRepository;
            _detailUserRepository = detailUserRepository;
        }

        #endregion CONTRUCTOR

        #region GET

        public DetailDepartmentViewModel GetById(int id, int companyId)
        {
            var department = _idepartmentRepository.FindById(id, x => x.DeleteFlag == StatusDelete.NON_DELETED);
            if (department == null)
            {
                return null;
            }
            var lstEmployee = _userManager.Users.Where(x => x.DepartmentId == id && x.CompanyId == companyId).ToList();
            var data = _mapper.Map<Department, DetailDepartmentViewModel>(department);
            data.lstEmployee = _mapper.Map<List<AppUser>, List<AppUserViewModel>>(lstEmployee); ;
            return data;
        }

        public PagedResult<DepartmentViewModel> GetAllPaging(int page, int pageSize, int companyId)
        {
            try
            {
                var department = _idepartmentRepository.FindAll(x => x.CompanyId == companyId && x.DeleteFlag == StatusDelete.NON_DELETED);
                var employee = _userManager.Users.Where(x => x.CompanyId == companyId && x.DeleteFlag == StatusDelete.NON_DELETED);
                var detailUser = _detailUserRepository.FindAll(x => x.DeleteFlag == StatusDelete.NON_DELETED);
                var query = (from d in department
                             join e in employee on d.Id equals e.DepartmentId
                             join de in detailUser on e.Id equals de.Id
                             select new DepartmentViewModel()
                             {
                                 Address = d.Address,
                                 CompanyId = d.CompanyId,
                                 DepartmentName = d.DepartmentName,
                                 Description = d.Description,
                                 Email = d.Email,
                                 ManagerName = de.Name,
                                 PhoneNumber = d.PhoneNumber,
                                 ManagerId = e.Id,
                                 EmployeeCount = employee.Count(x => x.DepartmentId == e.DepartmentId)
                             }).ToList();
                int totalRow = query.Count();

                var paginationSet = new PagedResult<DepartmentViewModel>()
                {
                    Results = query,
                    CurrentPage = page,
                    RowCount = totalRow,
                    PageSize = pageSize
                };
                return paginationSet;
            }
            catch
            {
                return new PagedResult<DepartmentViewModel>()
                {
                    Results = new List<DepartmentViewModel>(),
                    CurrentPage = page,
                    RowCount = 0,
                    PageSize = pageSize
                };
            }
        }

        #endregion GET

        #region POST

        public async Task<Department> Add(DetailDepartmentViewModel vm)
        {

            var entity = _mapper.Map<Department>(vm);
            _idepartmentRepository.Add(entity);
            SaveChanges();
            var lstUser = _mapper.Map<List<AppUserViewModel>, List<AppUser>>(vm.lstEmployee);
            foreach (var item in lstUser)
            {
                if (item.DepartmentId == -1)
                {
                    item.DepartmentId = entity.Id;
                    await _userManager.UpdateAsync(item);
                }
            }
            SaveChanges();
            return entity;
        }

        #endregion POST

        #region PUT

        public async Task Update(DetailDepartmentViewModel vm)
        {
            var entity = _mapper.Map<Department>(vm);
            var lstOldUser = _userManager.Users.Where(x => x.DepartmentId == vm.Id).ToList();
            if (lstOldUser == null && lstOldUser.Count > 0)
            {
                foreach (var item in lstOldUser)
                {
                    item.DepartmentId = -1;
                    await _userManager.UpdateAsync(item);
                }
            }

            var lstUser = _mapper.Map<List<AppUserViewModel>, List<AppUser>>(vm.lstEmployee);
            foreach (var item in lstUser)
            {
                if (item.DepartmentId == -1)
                {
                    item.DepartmentId = entity.Id;
                    await _userManager.UpdateAsync(item);
                }
            }
            SaveChanges();
        }

        private void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        #endregion PUT

        #region DELETE

        public void Delete(int id)
        {
            var entity = _idepartmentRepository.FindById(id);
            entity.DeleteFlag = StatusDelete.DELETED;
            _idepartmentRepository.Update(entity);
            SaveChanges();
        }

        public int GetEmployeeCount( int companyId)
        {
            return _idepartmentRepository.FindAll(x => x.CompanyId == companyId).Count();
        }

        #endregion DELETE
    }

}

