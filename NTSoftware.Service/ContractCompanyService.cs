using AutoMapper;
using NTSoftware.Core.Models.Enum;
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
    public class ContractCompanyService : IContractCompanyService
    {
        private IDetailUserRepository _detailUserRepository;
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private IContractCompanyRepository _icontractCompanyRepository;
        private readonly AppDbContext _dbContext;
        public ContractCompanyService(IUnitOfWork unitOfWork, IMapper mapper, AppDbContext dbContext, IContractCompanyRepository icontractCompanyRepo, IDetailUserRepository detailUserRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _icontractCompanyRepository = icontractCompanyRepo;
            _dbContext = dbContext;
            _detailUserRepository = detailUserRepository;
        }
        public List<ContractCompanyViewModel> GetAll( DateTime date1, DateTime date2)
        {
            var enddate = _icontractCompanyRepository.FindAll().Where(x =>(x.EndDate <= date2 )&&( x.EndDate >= date1)).ToList();
            //var model = _icontractCompanyRepository.FindAll().ToList();
            return _mapper.Map<List<ContractCompany>, List<ContractCompanyViewModel>>(enddate);
        }

        public PagedResult<ContractCompanyViewModel> GetAllPaging(int page, int pageSize, Status status)
        {

            var query = _icontractCompanyRepository.FindAll().ToList();
            int totalRow = query.Count();

            try
            {
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
            catch
            {
                return null;
            }
        }
        

        public ContractCompanyViewModel GetById(int id)
        {
            var data = _icontractCompanyRepository.FindById(id);
            return _mapper.Map<ContractCompany, ContractCompanyViewModel>(data);
        }
        public ContractCompany Add(ContractCompanyViewModel vm)
        {
            {
                var entity = _mapper.Map<ContractCompany>(vm);
                _icontractCompanyRepository.Add(entity);
                SaveChanges();
                return entity;
            }
        }
        public void Update(ContractCompanyViewModel Vm)
        {
            var data = _mapper.Map<ContractCompany>(Vm);
            _icontractCompanyRepository.Update(data);
            SaveChanges();
        }
        private void SaveChanges()
            {
                _unitOfWork.Commit();
            }
        
    }
}
