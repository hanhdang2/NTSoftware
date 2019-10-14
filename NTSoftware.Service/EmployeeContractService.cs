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
    public class EmployeeContractService : IEmployeeContractService
    {
        #region CONTRUCTOR

        private readonly IMapper _mapper;
        private IEmployeeContractRepository _employeeContractRepository;

        public EmployeeContractService(IMapper mapper, IEmployeeContractRepository employeeContractRepository)
        {
            _mapper = mapper;
            _employeeContractRepository = employeeContractRepository;
        }

        #endregion CONTRUCTOR

        #region GET

        public EmployeeContractViewModel GetById(int id)
        {
            var data = _employeeContractRepository.FindById(id);
            return _mapper.Map<EmployeeContract, EmployeeContractViewModel>(data);
        }

        public PagedResult<EmployeeContractViewModel> GetAllPaging(int page, int pageSize, int companyId, Status status)
        {
            var query = _employeeContractRepository.FindAll(x => x.CompanyId == companyId).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            if (status != Status.None)
            {
                query = query.Where(x => x.Status == status).ToList();
            }
            int totalRow = query.Count();

            try
            {
                var data = _mapper.Map<List<EmployeeContract>, List<EmployeeContractViewModel>>(query);

                var paginationSet = new PagedResult<EmployeeContractViewModel>()
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
                return new PagedResult<EmployeeContractViewModel>()
                {
                    Results = new List<EmployeeContractViewModel>(),
                    CurrentPage = page,
                    RowCount = 0,
                    PageSize = pageSize
                }; ;
            }
        }

        #endregion GET

        #region POST

        public EmployeeContract Add(EmployeeContractViewModel vm, string companyCode)
        {

            var entity = _mapper.Map<EmployeeContract>(vm);
            entity.ContractNumber = $"HDE{companyCode}{_employeeContractRepository.FindAll().ToList().Count + 1}";
            _employeeContractRepository.Add(entity);
            return entity;
        }

        #endregion POST

        #region PUT

        public void Update(EmployeeContractViewModel vm)
        {
            var entity = _mapper.Map<EmployeeContract>(vm);
            _employeeContractRepository.Update(entity);
        }

        #endregion PUT

        #region DELETE

        public void Delete(int id)
        {
            var entity = _employeeContractRepository.FindById(id);
            _employeeContractRepository.RemoveFlg(entity);
            _employeeContractRepository.Update(entity);
        }

        #endregion DELETE

        #region OTHER_METHOD

        #endregion OTHER_METHOD




    }
}
