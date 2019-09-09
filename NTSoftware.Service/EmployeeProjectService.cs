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
   public class EmployeeProjectService : IEmployeeProjectService
    {
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private IEmployeeProjectRepository _iemployeeProjectRepo;
        private readonly AppDbContext _dbContext;
        public EmployeeProjectService(IUnitOfWork unitOfWork, IMapper mapper, AppDbContext dbContext, IEmployeeProjectRepository iemployeeProjectRepo)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _iemployeeProjectRepo = iemployeeProjectRepo;
            _dbContext = dbContext;
        }

        public EmployeeProjectViewModel GetById(Guid id)
        {
            var data = _iemployeeProjectRepo.FindById(id);
            return _mapper.Map<EmployeeProject, EmployeeProjectViewModel>(data);
        }

        public List<EmployeeProjectViewModel> GetAll()
        {
            var model = _iemployeeProjectRepo.FindAll().ToList();
            return _mapper.Map<List<EmployeeProject>, List<EmployeeProjectViewModel>>(model);
        }

        public PagedResult<EmployeeProjectViewModel> GetAllPaging(int page, int pageSize)
        {
            throw new NotImplementedException();
        }
        public EmployeeProject Add(EmployeeProjectViewModel vm)
        {
             var entity = _mapper.Map<EmployeeProjectViewModel, EmployeeProject>(vm);
            _iemployeeProjectRepo.Add(entity);
                SaveChanges();
             return entity;
        }

        private void SaveChanges()
        {
            _unitOfWork.Commit();
        }
    }
}
