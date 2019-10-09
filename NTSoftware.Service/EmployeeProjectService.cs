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
        private IEmployeeProjectRepository _iemployeeProjectRepository;
        private readonly AppDbContext _dbContext;
        public EmployeeProjectService(IUnitOfWork unitOfWork, IMapper mapper, AppDbContext dbContext, IEmployeeProjectRepository iemployeeProjectRepo)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _iemployeeProjectRepository = iemployeeProjectRepo;
            _dbContext = dbContext;
        }

        public List<EmployeeProjectViewModel> GetAll()
        {
            var model = _iemployeeProjectRepository.FindAll().ToList();
            return _mapper.Map<List<EmployeeProject>, List<EmployeeProjectViewModel>>(model);
        }

        public PagedResult<EmployeeProjectViewModel> GetAllPaging(int page, int pageSize)
        {
            var query = _iemployeeProjectRepository.FindAll().ToList();
            int totalRow = query.Count();

            try
            {
                var data = _mapper.Map<List<EmployeeProject>, List<EmployeeProjectViewModel>>(query);

                var paginationSet = new PagedResult<EmployeeProjectViewModel>()
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
        public EmployeeProject Add(EmployeeProjectViewModel vm)
        {
             var entity = _mapper.Map<EmployeeProjectViewModel, EmployeeProject>(vm);
            _iemployeeProjectRepository.Add(entity);
                SaveChanges();
             return entity;
        }

        private void SaveChanges()
        {
            _unitOfWork.Commit();
        }
    }
}
