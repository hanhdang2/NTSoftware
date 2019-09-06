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
            throw new NotImplementedException();
        }
    }
}
