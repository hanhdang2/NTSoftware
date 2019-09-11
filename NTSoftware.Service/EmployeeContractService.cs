﻿using AutoMapper;
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
   public class EmployeeContractService: IEmployeeContractService
    {
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private IEmployeeContractRepository _iemployeeContractRepo;
        private readonly AppDbContext _dbContext;
        public EmployeeContractService(IUnitOfWork unitOfWork, IMapper mapper, AppDbContext dbContext, IEmployeeContractRepository iemployeeContractRepo)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _iemployeeContractRepo = iemployeeContractRepo;
            _dbContext = dbContext;
        }

        public EmployeeContractViewModel GetById(int id)
        {
            var data = _iemployeeContractRepo.FindById(id);
            return _mapper.Map<EmployeeContract, EmployeeContractViewModel>(data);
        }

        public List<EmployeeContractViewModel> GetAll()
        {
            var model = _iemployeeContractRepo.FindAll().ToList();
            return _mapper.Map<List<EmployeeContract>, List<EmployeeContractViewModel>>(model);
        }

        public PagedResult<EmployeeContractViewModel> GetAllPaging(int page, int pageSize)
        {
            var query = _iemployeeContractRepo.FindAll().ToList();
            int totalRow = query.Count();

            try
            {
                var data = _mapper.Map<List<EmployeeContract>, List<EmployeeContractViewModel>>(query);

                var paginationSet = new PagedResult<EmployeeContractViewModel>()
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
        public EmployeeContract Add(EmployeeContractViewModel vm)
        {
            {
                var entity = _mapper.Map<EmployeeContract>(vm);
                _iemployeeContractRepo.Add(entity);
                SaveChanges();
                return entity;
            }
        }

        private void SaveChanges()
        {
            _unitOfWork.Commit();
        }
    }
}
