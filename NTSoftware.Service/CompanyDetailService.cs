using AutoMapper;
using Microsoft.AspNetCore.Identity;
using NTSoftware.Core.Models.Enum;
using NTSoftware.Core.Models.Models;
using NTSoftware.Core.Models.Models.NTSoftware.Core.Models.Models;
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
    public class CompanyDetailService : ICompanyDetailService
    {
        #region Contructor
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private ICompanyRepository _icompanyRepository;
        private readonly AppDbContext _dbContext;
        private IDetailUserRepository _detailUserRepository;
        private IContractCompanyRepository _icontractCompanyRepository;
        private readonly UserManager<AppUser> _userManager;
        private IDepartmentRepository _idepartmentRepository;


        public CompanyDetailService(IUnitOfWork unitOfWork, IMapper mapper,IDepartmentRepository departmentRepository, AppDbContext dbContext, UserManager<AppUser> userManager, ICompanyRepository icompanyRepo, IDetailUserRepository detailUserRepository, IContractCompanyRepository icontractCompanyRepository)
        { 
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _icompanyRepository = icompanyRepo;
            _dbContext = dbContext;
            _detailUserRepository = detailUserRepository;
            _icontractCompanyRepository = icontractCompanyRepository;
            _userManager = userManager;
            _idepartmentRepository = departmentRepository;
        }
        #endregion Contructor
        public List<CompanyDetailViewModel> GetAll()
        {
            var data = _icompanyRepository.FindAll().ToList();
            return _mapper.Map<List<CompanyDetail>, List<CompanyDetailViewModel>>(data);
        }
        
       
        public PagedResult<CompanyDetailViewModel> GetAllPaging(int page, int pageSize, string namecompany,
            string phonenumber, string address,
            string representativename, string positionrepresentative)
        {
            var query = _icompanyRepository.Find(x => x.CompanyName == namecompany && x.PhoneNumber == phonenumber && x.Address == address && x.RepresentativeName == representativename && x.PositionRepresentative == positionrepresentative);
            int totalRow = query.Count();

            try
            {
                var data = _mapper.Map<List<CompanyDetail>, List<CompanyDetailViewModel>>(query.ToList());

                var paginationSet = new PagedResult<CompanyDetailViewModel>()
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

        public CompanyDetailViewModel GetById(int id)
        {
            var model = _icompanyRepository.FindById(id);
            return _mapper.Map<CompanyDetail, CompanyDetailViewModel>(model);
        }
        public ContractCompanyModel GetCompanyContract(int id)
        {
            var company = _icompanyRepository.FindById(id);
            if (company != null)
            {
                var model = _mapper.Map<CompanyDetailViewModel>(company);
                var contract = _icontractCompanyRepository.Find(x => x.CompanyId == id);
                var data = _mapper.Map<List<ContractCompanyViewModel>>(contract);
                var result = new ContractCompanyModel();
                result.companydetail = model;
                result.contractcompany = data;
                return result;
            }
            else
            {
                return null;
            }

        }
        public CompanyDetail Add(CompanyDetailViewModel Vm)
        {
            var entity = _mapper.Map<CompanyDetail>(Vm);
            _icompanyRepository.Add(entity);
            return entity;
        }
         public void Update(CompanyDetailViewModel Vm)
        {
            var data = _mapper.Map<CompanyDetail>(Vm);
            _icompanyRepository.Update(data);
            SaveChanges();
        }
      
        
        private void SaveChanges()
        {
            _unitOfWork.Commit();
        }

    }
}
