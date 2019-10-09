using AutoMapper;
using NTSoftware.Core.Models.Enum;
using NTSoftware.Core.Models.Models;
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

namespace NTSoftware.Service
{
    public class ContractCompanyService : IContractCompanyService
    {
        #region CONTRUCTOR

        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private IContractCompanyRepository _contractCompanyRepository;
        private readonly AppDbContext _dbContext;
        public ContractCompanyService(IUnitOfWork unitOfWork, IMapper mapper, AppDbContext dbContext, IContractCompanyRepository icontractCompanyRepo)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _contractCompanyRepository = icontractCompanyRepo;
            _dbContext = dbContext;
        }

        #endregion CONTRUCTOR

        #region GET

        public PagedResult<ContractCompanyViewModel> GetAllPaging(int page, int pageSize, Status status)
        {

            var query = _contractCompanyRepository.FindAll().ToList();
            int totalRow = query.Count();

            var data = _mapper.Map<List<ContractCompany>, List<ContractCompanyViewModel>>(query);

            var paginationSet = new PagedResult<ContractCompanyViewModel>()
            {
                Results = data,
                CurrentPage = page,
                RowCount = totalRow,
                PageSize = pageSize
            };
            return paginationSet;

        }

        public ContractCompanyViewModel GetById(int id)
        {
            var data = _contractCompanyRepository.FindById(id);
            return _mapper.Map<ContractCompany, ContractCompanyViewModel>(data);
        }

        #endregion GET

        #region POST

        public ContractCompany Add(ContractCompanyViewModel vm)
        {
            {
                var entity = _mapper.Map<ContractCompany>(vm);
                _contractCompanyRepository.Add(entity);
                SaveChanges();
                return entity;
            }
        }

        #endregion POST

        #region PUT

        public bool Update(ContractCompanyViewModel Vm)
        {
            var data = _mapper.Map<ContractCompany>(Vm);
            _contractCompanyRepository.Update(data);
            SaveChanges();
            return true;
        }
        private void SaveChanges()
        {
            _unitOfWork.Commit();
        }


        #endregion PUT

        #region DELETE

        public bool Delete(int id)
        {
            var entity = _contractCompanyRepository.FindById(id);
            entity.DeleteFlag = StatusDelete.DELETED;
            _contractCompanyRepository.Update(entity);
            SaveChanges();
            return true;
        }

        #endregion DELETE

        #region OTHER_METHOD


        #endregion OTHER_METHOD

    }
}
