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
    public class ContractService : IContractService
    {
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private IContractRepository _icontractRepo;
        private readonly AppDbContext _dbContext;
        public ContractService(IUnitOfWork unitOfWork, IMapper mapper, AppDbContext dbContext, IContractRepository icontractRepo)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _icontractRepo = icontractRepo;
            _dbContext = dbContext;
        }
        public List<ContractViewModel> GetAll()
        {
            var model = _icontractRepo.FindAll().ToList();
            return _mapper.Map<List<Contract>, List<ContractViewModel>>(model);
        }

        public PagedResult<ContractViewModel> GetAllPaging(int page, int pageSize)
        {

            var query = _icontractRepo.FindAll().ToList();
            int totalRow = query.Count();

            try
            {
                var data = _mapper.Map<List<Contract>, List<ContractViewModel>>(query);

                var paginationSet = new PagedResult<ContractViewModel>()
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

        public ContractViewModel GetById(int id)
        {
            var data = _icontractRepo.FindById(id);
            return _mapper.Map<Contract, ContractViewModel>(data);
        }
        public Contract Add(ContractViewModel vm)
        {
            {
                var entity = _mapper.Map<Contract>(vm);
                _icontractRepo.Add(entity);
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
