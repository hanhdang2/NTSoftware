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
   public class EmployeeService :IEmployeeService
    {
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private IEmployeeRepository _iemployeeRepository;
        private readonly AppDbContext _dbContext;
        public EmployeeService(IUnitOfWork unitOfWork, IMapper mapper, AppDbContext dbContext, IEmployeeRepository iemployeeRepo)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _iemployeeRepository = iemployeeRepo;
            _dbContext = dbContext;
        }

        public EmployeeViewModel GetById(int id)
        {
            var data = _iemployeeRepository.FindById(id);
            return _mapper.Map<Employee, EmployeeViewModel>(data);
        }

        public List<EmployeeViewModel> GetAll()
        {
            var model = _iemployeeRepository.FindAll().ToList();
            return _mapper.Map<List<Employee>, List<EmployeeViewModel>>(model);
        }

        public PagedResult<EmployeeViewModel> GetAllPaging(int page, int pageSize)
        {

            var query = _iemployeeRepository.FindAll().ToList();
            int totalRow = query.Count();

            try
            {
                var data = _mapper.Map<List<Employee>, List<EmployeeViewModel>>(query);

                var paginationSet = new PagedResult<EmployeeViewModel>()
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
        public Employee Add(EmployeeViewModel vm)
        {
            var entity = _mapper.Map<EmployeeViewModel, Employee>(vm);
            _iemployeeRepository.Add(entity);
            SaveChanges();
            return entity;
        }

        private void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(EmployeeViewModel vm)
        {
            var data = _mapper.Map<Employee>(vm);
            _iemployeeRepository.Update(data);
            SaveChanges();
        }
    }
}
