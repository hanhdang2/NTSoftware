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
    public class CompanyService : ICompanyService
    {
        #region Contructor
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private ICompanyRepository _icompanyRepository;
        private readonly AppDbContext _dbContext;

        public CompanyService(IUnitOfWork unitOfWork, IMapper mapper, AppDbContext dbContext, ICompanyRepository icompanyRepo)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _icompanyRepository = icompanyRepo;
            _dbContext = dbContext;
        }
        #endregion Contructor
        public List<CompanyViewModel> GetAll()
        {
            var data = _icompanyRepository.FindAll().ToList();
            return _mapper.Map<List<Company>, List<CompanyViewModel>>(data);
        }

        public PagedResult<CompanyViewModel> GetAllPaging(int page, int pageSize, string namecompany, 
            string phonenumber, string address,
            string representativename, string positionrepresentative)
        {
            var query = _icompanyRepository.Find(x => x.NameCompany == namecompany && x.PhoneNumber == phonenumber && x.Address == address && x.RepresentativeName == representativename && x.PositionRepresentative == positionrepresentative);
            int totalRow = query.Count();

            try
            {
                var data = _mapper.Map<List<Company>, List<CompanyViewModel>>(query.ToList());

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
            var model = _icompanyRepository.FindById(id);
            return _mapper.Map<Company, CompanyViewModel>(model);
        }

        public Company Add(CompanyViewModel Vm)
        {
            var entity = _mapper.Map<Company>(Vm);
            _icompanyRepository.Add(entity);
            SaveChanges();
            return entity;
        }
        public void Update(CompanyViewModel Vm)
        {
            var data = _mapper.Map<Company>(Vm);
            _icompanyRepository.Update(data);
            SaveChanges();
        }

        private void SaveChanges()
        {
            _unitOfWork.Commit();
        }

    }
}
