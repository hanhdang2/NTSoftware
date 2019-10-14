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

        private readonly IMapper _mapper;
        private IContractCompanyRepository _contractCompanyRepository;
        private readonly AppDbContext _dbContext;
        public ContractCompanyService(IMapper mapper, AppDbContext dbContext, IContractCompanyRepository icontractCompanyRepo)
        {
            _mapper = mapper;
            _contractCompanyRepository = icontractCompanyRepo;
            _dbContext = dbContext;
        }

        #endregion CONTRUCTOR

        #region GET

        public PagedResult<ContractCompanyViewModel> GetAllPaging(int page, int companyId, int pageSize, Status status)
        {

            var query = _contractCompanyRepository.FindAll(x => x.CompanyId == companyId).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            if (status != Status.None)
            {
                query = query.Where(x => x.Status == status).ToList();
            }

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

        public ContractCompany Add(ContractCompanyViewModel vm, string companyCode)
        {

            var entity = _mapper.Map<ContractCompany>(vm);
            entity.ContractNumber = $"HD{companyCode}{_contractCompanyRepository.FindAll().ToList().Count() + 1}";
            _contractCompanyRepository.Add(entity);
            return entity;
        }

        #endregion POST

        #region PUT

        public void Update(ContractCompanyViewModel Vm)
        {
            var data = _mapper.Map<ContractCompany>(Vm);
            _contractCompanyRepository.Update(data);
        }

        #endregion PUT

        #region DELETE

        public void Delete(int id)
        {
            var entity = _contractCompanyRepository.FindById(id);
            _contractCompanyRepository.RemoveFlg(entity);
            _contractCompanyRepository.Update(entity);
        }

        #endregion DELETE

        #region OTHER_METHOD


        #endregion OTHER_METHOD

    }
}
