using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NTSoftware.Core.Models.Enum;
using NTSoftware.Core.Shared;
using NTSoftware.Core.Shared.Constants;
using NTSoftware.Core.Shared.Dtos;
using NTSoftware.Core.Shared.Interface;
using NTSoftware.Service.Interface;
using NTSoftware.Service.Interface.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NTSoftware.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : BaseController
    {
        #region CONTRUCTOR

        private ICompanyDetailService _companyDetailService;
        private IMapper _mapper;
        private IUnitOfWork _unitOfWork;
        private IAppUserService _appUserService;
        private IContractCompanyService _contractCompanyService;
        private IEmployeeContractService _employeeContractService;
        private IDetailUserService _detailUserService;

        public CompanyController(ICompanyDetailService companyDetailService, IUnitOfWork unitOfWork, IAppUserService appUserService, IContractCompanyService contractCompanyService, IDetailUserService detailUserService, IMapper mapper, IEmployeeContractService employeeContractService)
        {
            _companyDetailService = companyDetailService;
            _unitOfWork = unitOfWork;
            _appUserService = appUserService;
            _contractCompanyService = contractCompanyService;
            _detailUserService = detailUserService;
            _mapper = mapper;
            _employeeContractService = employeeContractService;
        }

        #endregion CONTRUCTOR

        #region GET

        [HttpGet]
        [Route("GetById")]
        public IActionResult GetById(int id)
        {
            try
            {
                var result = _companyDetailService.GetById(id);
                return new OkObjectResult(new GenericResult(result, true, ErrorMsg.SUCCEED, ErrorCode.SUCCEED_CODE));
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new GenericResult(null, false, ErrorMsg.HAS_ERROR, ErrorCode.HAS_ERROR_CODE));
            }
        }

        [HttpGet]
        [Route("GetPaging")]
        public IActionResult GetPaging(string nameCompany = "",
            string phoneNumber = "", string address = "",
            string representativeName = "", string positionRepresentative = "", int page = 1, int pageSize = 20)
        {
            try
            {
                if (page < 0)
                {
                    page = 1;
                }
                if (pageSize < 0)
                {
                    pageSize = 20;
                }
                var result = _companyDetailService.GetAllPaging(page, pageSize, nameCompany, phoneNumber, address, representativeName, positionRepresentative);
                return new OkObjectResult(new GenericResult(result, true, ErrorMsg.SUCCEED, ErrorCode.SUCCEED_CODE));
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new GenericResult(null, false, ErrorMsg.HAS_ERROR, ErrorCode.HAS_ERROR_CODE));
            }
        }

        #endregion GET

        #region POST

        [HttpPost]
        [Route("AddCompany")]
        public async Task<IActionResult> AddCompany([FromBody] CompanyFullViewModel Vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var allErrors = ModelState.Values.SelectMany(v => v.Errors);
                    return new BadRequestObjectResult(new GenericResult(allErrors, false, ErrorMsg.DATA_REQUEST_IN_VALID, ErrorCode.ERROR_HANDLE_DATA));
                }
                var user = await _appUserService.GetByUserName(Vm.EmployeeVm.UserName);
                if (user != null)
                {
                    return new OkObjectResult(new GenericResult(null, false, ErrorMsg.ACCOUNT_EXISTED, ErrorCode.ERROR_CODE));
                }
                var company = _companyDetailService.Add(_mapper.Map<CompanyDetailViewModel>(Vm));
                SaveChanges();
                Vm.CompanyCode = company.CompanyCode;
                Vm.ContractVm.Status = Status.New;
                var contractCompany = _contractCompanyService.Add(_mapper.Map<ContractCompanyViewModel>(Vm.ContractVm), company.CompanyCode);
                Vm.EmployeeVm.UserType = Roles.AdminCompany;
                Vm.EmployeeVm.CompanyId = company.Id;
                var userVm = _mapper.Map<EmployeeViewModel, AppUserViewModel>(Vm.EmployeeVm);
                userVm.UserType = Roles.AdminCompany;
                userVm.ContractNumber = contractCompany.ContractNumber;
                var result = await _appUserService.AddAsync(userVm);
                if (result != null)
                {
                    var detailVm = _mapper.Map<EmployeeViewModel, DetailUserViewModel>(Vm.EmployeeVm);
                    detailVm.Id = result.Id;
                    var resultDetail = _detailUserService.Add(detailVm, company.CompanyCode, company.Id);
                    SaveChanges();
                    return new OkObjectResult(new GenericResult(result, true, ErrorMsg.SUCCEED, ErrorCode.SUCCEED_CODE));
                }
                return new OkObjectResult(new GenericResult(null, true, ErrorMsg.HAS_ERROR, ErrorCode.HAS_ERROR_CODE));
            }
            catch (Exception ex)
            {
                return new OkObjectResult(new GenericResult(null, false, ErrorMsg.HAS_ERROR, ErrorCode.HAS_ERROR_CODE));
            }
        }
        [HttpPost]
        [Route("AddEmployee")]
        public async Task<IActionResult> AddEmployee([FromBody] EmployeeFullViewModel Vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var allErrors = ModelState.Values.SelectMany(v => v.Errors);
                    return new BadRequestObjectResult(new GenericResult(allErrors, false, ErrorMsg.DATA_REQUEST_IN_VALID, ErrorCode.ERROR_HANDLE_DATA));
                }
                var user = await _appUserService.GetByUserName(Vm.EmployeeViewModel.UserName);
                if (user != null)
                {
                    return new OkObjectResult(new GenericResult(null, false, ErrorMsg.ACCOUNT_EXISTED, ErrorCode.ERROR_CODE));
                }
                var company = _companyDetailService.GetById(Vm.CompanyId);
                if (company == null)
                {
                    return new OkObjectResult(new GenericResult(null, false, ErrorMsg.COMPANY_NOT_EXITS, ErrorCode.ERROR_CODE));
                }
                var contrucEmployee = _employeeContractService.Add(_mapper.Map<EmployeeContractViewModel>(Vm), company.CompanyCode);
                var userVm = _mapper.Map<EmployeeViewModel, AppUserViewModel>(Vm.EmployeeViewModel);
                userVm.UserType = Roles.Employee;
                userVm.Status = Status.New;
                userVm.ContractNumber = contrucEmployee.ContractNumber;
                userVm.CompanyId = company.Id;
                var result = await _appUserService.AddAsync(userVm);
                if (result != null)
                {
                    var detailVm = _mapper.Map<EmployeeViewModel, DetailUserViewModel>(Vm.EmployeeViewModel);
                    detailVm.Id = result.Id;
                    var resultDetail = _detailUserService.Add(detailVm, company.CompanyCode, company.Id);
                    SaveChanges();
                    return new OkObjectResult(new GenericResult(result, true, ErrorMsg.SUCCEED, ErrorCode.SUCCEED_CODE));
                }
                return new OkObjectResult(new GenericResult(null, true, ErrorMsg.HAS_ERROR, ErrorCode.HAS_ERROR_CODE));
            }
            catch (Exception ex)
            {
                return new OkObjectResult(new GenericResult(null, false, ErrorMsg.HAS_ERROR, ErrorCode.HAS_ERROR_CODE));
            }
        }

        #endregion POST

        #region PUT

        [HttpPost]
        [Route("Update")]
        public IActionResult Update([FromBody] CompanyDetailViewModel Vm)
        {
            try
            {
                var bIsExist = _companyDetailService.CheckCompanyExpried(Vm.Id);
                if (bIsExist != null)
                {
                    return new OkObjectResult(bIsExist);
                }
                _companyDetailService.Update(Vm);
                SaveChanges();
                return new OkObjectResult(new GenericResult(null, true, ErrorMsg.HAS_ERROR, ErrorCode.HAS_ERROR_CODE));
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new GenericResult(null, false, ErrorMsg.HAS_ERROR, ErrorCode.HAS_ERROR_CODE));
            }
        }

        #endregion PUT

        #region DELETE

        [HttpDelete]
        [Route("Delete")]
        public IActionResult Delete([FromBody] CompanyDetailViewModel Vm)
        {
            try
            {
                var bIsExist = _companyDetailService.CheckCompanyExpried(Vm.Id);
                if (bIsExist == null)
                {
                    _companyDetailService.DeleteCompany(Vm.Id);
                    SaveChanges();
                    return new OkObjectResult(new GenericResult(null, true, ErrorMsg.HAS_ERROR, ErrorCode.HAS_ERROR_CODE));

                }
                if (bIsExist.ErrorCode == ErrorCode.NOT_EXIST_COMPANY_CODE)
                {
                    return new OkObjectResult(bIsExist);
                }
                return new OkObjectResult(new GenericResult(null, false, ErrorMsg.COMPANY_CAN_NOT_DELETE, ErrorCode.CAN_NOT_DELETE_COMPANY));

            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new GenericResult(null, false, ErrorMsg.HAS_ERROR, ErrorCode.HAS_ERROR_CODE));
            }
        }

        #endregion DELETE

        #region PRIVATE_METHOD

        private void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        #endregion PRIVATE_METHOD
    }
}