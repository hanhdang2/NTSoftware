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
   public class EmployeeDepartmentService : IEmployeeDepartmentService
    {
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private IEmployeeDepartmentRepository _iemployeeDepartmentRepo;
        private readonly AppDbContext _dbContext;
        public EmployeeDepartmentService(IUnitOfWork unitOfWork, IMapper mapper, AppDbContext dbContext, IEmployeeDepartmentRepository iemployeeDepartmentRepo)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _iemployeeDepartmentRepo = iemployeeDepartmentRepo;
            _dbContext = dbContext;
        }

        public EmployeeDepartmentViewModel GetById(int id)
        {
            var data = _iemployeeDepartmentRepo.FindById(id);
            return _mapper.Map<EmployeeDepartment, EmployeeDepartmentViewModel>(data);
        }

        public List<EmployeeDepartmentViewModel> GetAll()
        {
            var model = _iemployeeDepartmentRepo.FindAll().ToList();
            return _mapper.Map<List<EmployeeDepartment>, List<EmployeeDepartmentViewModel>>(model);
        }

        public PagedResult<EmployeeDepartmentViewModel> GetAllPaging(int page, int pageSize)
        {
            throw new NotImplementedException();
        }
        public EmployeeDepartment Add(EmployeeDepartmentViewModel vm)
        {
             var entity = _mapper.Map<EmployeeDepartmentViewModel,EmployeeDepartment>(vm);
                _iemployeeDepartmentRepo.Add(entity);
                SaveChanges();
             return entity;
        }

        private void SaveChanges()
        {
            _unitOfWork.Commit();
        }
    }
}
