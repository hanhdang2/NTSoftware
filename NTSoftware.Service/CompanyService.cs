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
    public class CompanyService : ICompanyService
    {
        #region Contructor
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private ICompanyRepository _icompanyRepo;
        private readonly AppDbContext _dbContext;

        public CompanyService(IUnitOfWork unitOfWork, IMapper mapper, AppDbContext dbContext, ICompanyRepository icompanyRepo)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _icompanyRepo = icompanyRepo;
            _dbContext = dbContext;
        }
        #endregion Contructor
        public List<CompanyViewModel> GetAll()
        {
            var data = _icompanyRepo.FindAll().ToList();
            return _mapper.Map<List <Company>, List<CompanyViewModel>>(data);
        }

        public PagedResult<CompanyViewModel> GetAllPaging(int page, int pageSize)
        {
            var query = _icompanyRepo.FindAll().ToList();
            int totalRow = query.Count();

            try
            {
                var data = _mapper.Map<List<Company>, List<CompanyViewModel>>(query);

                var paginationSet = new PagedResult<CompanyViewModel>()
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

        public CompanyViewModel GetById(int id)
        {
            var model = _icompanyRepo.FindById(id);
            return _mapper.Map<Company, CompanyViewModel>(model);
        }

        public Company Add(CompanyCreateViewModel Vm)
        {
            var entity = _mapper.Map<Company>(Vm);
            _icompanyRepo.Add(entity);
            SaveChanges();
            return entity;
        }

        private void SaveChanges()
        {
            _unitOfWork.Commit();
        }
    }
}