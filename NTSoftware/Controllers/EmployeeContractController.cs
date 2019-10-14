using Microsoft.AspNetCore.Mvc;
using NTSoftware.Core.Shared;
using NTSoftware.Core.Shared.Constants;
using NTSoftware.Core.Shared.Dtos;
using NTSoftware.Core.Shared.Interface;
using NTSoftware.Service.Interface;
using NTSoftware.Service.Interface.ViewModels;
using System;

namespace NTSoftware.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeContractController : BaseController
    {

        #region CONTRUCTOR

        private IEmployeeContractService _employeeContractService;
        private ICompanyDetailService _companyDetailService;
        private IUnitOfWork _unitOfWork;

        public EmployeeContractController(IEmployeeContractService employeeContractService, ICompanyDetailService companyDetailService, IUnitOfWork unitOfWork)
        {
            _employeeContractService = employeeContractService;
            _companyDetailService = companyDetailService;
            _unitOfWork = unitOfWork;
        }

        #endregion CONTRUCTOR

        #region GET

        [HttpGet]
        [Route("GetById")]
        public IActionResult GetById(int id, int companyId)
        {
            try
            {
                var companyExpired = _companyDetailService.CheckCompanyExpried(companyId);
                if (companyExpired != null)
                {
                    return new OkObjectResult(companyExpired);
                }
                var data = _employeeContractService.GetById(id);
                return new OkObjectResult(new GenericResult(data, true, ErrorMsg.SUCCEED, ErrorCode.SUCCEED_CODE));
            }
            catch (Exception ex)
            {
                return new OkObjectResult(new GenericResult(null, false, ErrorMsg.HAS_ERROR, ErrorCode.ERROR_CODE));
            }
        }

        #endregion GET

        #region POST

        [HttpPost]
        [Route("Add")]
        public IActionResult Add([FromBody] EmployeeContractViewModel Vm)
        {
            try
            {
                var companyExpired = _companyDetailService.CheckCompanyExpried(Vm.CompanyId);
                if (companyExpired != null)
                {
                    return new OkObjectResult(companyExpired);
                }
                var data = _employeeContractService.Add(Vm, _companyDetailService.GetById(Vm.CompanyId).CompanyCode);
                SaveChanges();
                return new OkObjectResult(new GenericResult(data, true, ErrorMsg.SUCCEED, ErrorCode.SUCCEED_CODE));
            }
            catch (Exception ex)
            {
                return new OkObjectResult(new GenericResult(null, false, ErrorMsg.HAS_ERROR, ErrorCode.ERROR_CODE));
            }
        }

        #endregion POST

        #region PUT

        [HttpPut]
        [Route("Update")]
        public IActionResult Update([FromBody] EmployeeContractViewModel Vm)
        {
            try
            {
                var companyExpired = _companyDetailService.CheckCompanyExpried(Vm.CompanyId);
                if (companyExpired != null)
                {
                    return new OkObjectResult(companyExpired);
                }
                _employeeContractService.Update(Vm);
                SaveChanges()
                return new OkObjectResult(new GenericResult(null, true, ErrorMsg.SUCCEED, ErrorCode.SUCCEED_CODE));
            }
            catch (Exception ex)
            {
                return new OkObjectResult(new GenericResult(null, false, ErrorMsg.HAS_ERROR, ErrorCode.ERROR_CODE));
            }
        }

        #endregion PUT

        #region DELETE

        [HttpDelete]
        [Route("Delete")]
        public IActionResult Delete(int id, int companyId)
        {
            try
            {
                var companyExpired = _companyDetailService.CheckCompanyExpried(companyId);
                if (companyExpired != null)
                {
                    return new OkObjectResult(companyExpired);
                }
                _employeeContractService.Delete(id);
                SaveChanges();
                return new OkObjectResult(new GenericResult(null, true, ErrorMsg.SUCCEED, ErrorCode.SUCCEED_CODE));
            }
            catch (Exception ex)
            {
                return new OkObjectResult(new GenericResult(null, false, ErrorMsg.HAS_ERROR, ErrorCode.ERROR_CODE));
            }
        }

        #endregion DELETE

        #region OTHER_METHOD

        private void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        #endregion OTHER_METHOD


    }
}