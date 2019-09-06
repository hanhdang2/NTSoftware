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
   public class DepartmentService : IDepartmentService
    {
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private IDepartmentRepository _idepartmentRepo;
        private readonly AppDbContext _dbContext;
        public DepartmentService(IUnitOfWork unitOfWork, IMapper mapper, AppDbContext dbContext, IDepartmentRepository idepartmentRepo)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _idepartmentRepo = idepartmentRepo;
            _dbContext = dbContext;
        }

        public DepartmentViewModel GetById(int id)
        {
            var data = _idepartmentRepo.FindById(id);
            return _mapper.Map<Department, DepartmentViewModel>(data);
        }

        public List<DepartmentViewModel> GetAll()
        {
            var model = _idepartmentRepo.FindAll().ToList();
            return _mapper.Map<List<Department>, List<DepartmentViewModel>>(model);
        }

        public PagedResult<DepartmentViewModel> GetAllPaging(int page, int pageSize)
        {
            var query = _idepartmentRepo.FindAll().ToList();
            int totalRow = query.Count();

            try
            {
                var data = _mapper.Map<List<Department>, List<DepartmentViewModel>>(query);

                var paginationSet = new PagedResult<DepartmentViewModel>()
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
    }

    }
