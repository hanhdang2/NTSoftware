using AutoMapper;
using NTSoftware.Core.Models.Models;
using NTSoftware.Core.Shared;
using NTSoftware.Core.Shared.Constants;
using NTSoftware.Core.Shared.Dtos;
using NTSoftware.Core.Shared.Helper;
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
        private IContractCompanyRepository _iContractCompanyRepository;
        private readonly AppDbContext _dbContext;

        public CompanyDetailService(IUnitOfWork unitOfWork, IMapper mapper, AppDbContext dbContext, ICompanyRepository icompanyRepo, ICompanyRepository icompanyRepository, IContractCompanyRepository icontractCompanyRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _icompanyRepository = icompanyRepo;
            _dbContext = dbContext;
            _icompanyRepository = icompanyRepository;
            _iContractCompanyRepository = icontractCompanyRepository;
        }
        #endregion Contructor

        #region GET

        public List<CompanyDetailViewModel> GetAll()
        {
            var data = _icompanyRepository.FindAll().ToList();
            return _mapper.Map<List<CompanyDetail>, List<CompanyDetailViewModel>>(data);
        }

        public PagedResult<CompanyDetailViewModel> GetAllPaging(int page, int pageSize, string nameCompany,
            string phoneNumber, string address,
            string representativeName, string positionRepresentative)
        {
            string newPhone = Utilities.ConvertToUnSign(phoneNumber);
            string newNameCompany = Utilities.ConvertToUnSign(nameCompany);
            string newRepresentativeName = Utilities.ConvertToUnSign(representativeName);
            string newPositionRepresentative = Utilities.ConvertToUnSign(positionRepresentative);
            var query = _icompanyRepository.Find(x => Utilities.ConvertToUnSign(x.CompanyName).Contains(newNameCompany) &&
             Utilities.ConvertToUnSign(x.PhoneNumber).Contains(newPhone) &&
              Utilities.ConvertToUnSign(x.RepresentativeName).Contains(newRepresentativeName) &&
               Utilities.ConvertToUnSign(x.PositionRepresentative).Contains(newPositionRepresentative));

            int totalRow = query.Count();


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

        public GenericResult GetById(int id)
        {
            var model = _icompanyRepository.FindById(id);
            var company = _mapper.Map<CompanyDetail, CompanyDetailViewModel>(model);
            return new GenericResult(company, true, ErrorMsg.SUCCEED, ErrorCode.SUCCEED_CODE);
        }

        #endregion GET

        #region POST

        public GenericResult Add(CompanyDetailViewModel Vm)
        {
            var entity = _mapper.Map<CompanyDetail>(Vm);
            _icompanyRepository.Add(entity);
            SaveChanges();
            return new GenericResult(entity, true, ErrorMsg.SUCCEED, ErrorCode.SUCCEED_CODE);
        }

        #endregion POST

        #region PUT

        public GenericResult Update(CompanyDetailViewModel Vm)
        {
            var data = _mapper.Map<CompanyDetail>(Vm);
            _icompanyRepository.Update(data);
            SaveChanges();
            return new GenericResult(data, true, ErrorMsg.SUCCEED, ErrorCode.SUCCEED_CODE);
        }

        private void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        #endregion PUT

        #region DELETE

        public GenericResult DeleteCompany(int id)
        {
            var company = _icompanyRepository.FindById(id);
            company.DeleteFlag = StatusDelete.DELETED;
            return new GenericResult(null, true, ErrorMsg.SUCCEED, ErrorCode.SUCCEED_CODE);

        }

        #endregion DELETE

        #region PRIVATE_METHOD

        public GenericResult CheckCompanyExpried(int id)
        {
            var checkCompany = CheckCompanyExist(id);
            if (checkCompany == false)
            {
                return new GenericResult(null, false, ErrorMsg.COMPANY_NOT_EXITS, ErrorCode.ERROR_CODE);
            }
            var contractCompany = _iContractCompanyRepository.Find(x => x.CompanyId == id).LastOrDefault();
            if (contractCompany == null || contractCompany.DeleteFlag == StatusDelete.DELETED)
            {
                return new GenericResult(null, false, ErrorMsg.COMPANY_EXPRIED, ErrorCode.ERROR_CODE);
            }
            if (contractCompany.EndDate < DateTime.Now)
            {
                return new GenericResult(null, false, ErrorMsg.COMPANY_EXPRIED, ErrorCode.ERROR_CODE);
            }
            else if (contractCompany.StartDate > DateTime.Now)
            {
                return new GenericResult(null, false, ErrorMsg.COMPANY_NOT_READY, ErrorCode.ERROR_CODE);
            }
            return null;
        }

        public bool CheckCompanyExist(int id)
        {
            var company = _icompanyRepository.FindById(id);
            return company == null;
        }

        #endregion PRIVATE_METHOD
    }
}
