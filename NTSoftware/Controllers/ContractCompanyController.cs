using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NTSoftware.Core.Models.Enum;
using NTSoftware.Core.Models.Models;
using NTSoftware.Core.Shared;
using NTSoftware.Core.Shared.Constants;
using NTSoftware.Core.Shared.Dtos;
using NTSoftware.Core.Shared.Interface;
using NTSoftware.Service.Interface;
using NTSoftware.Service.Interface.ViewModels;

namespace NTSoftware.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractController : BaseController
    {
        #region CONTRUCTOR

        private IContractCompanyService _contractCompany;
        private ICompanyDetailService _companyDetailService;
        private IUnitOfWork _unitOfWork;

        public ContractController(IContractCompanyService contractCompany, IUnitOfWork unitOfWork, ICompanyDetailService companyDetailService)
        {
            _contractCompany = contractCompany;
            _unitOfWork = unitOfWork;
            _companyDetailService = companyDetailService;
        }

        #endregion CONTRUCTOR

        #region GET

        [HttpGet]
        [Route("GetPaging")]
        public IActionResult GetPaging(int companyId, Status status = Status.None, int page = 1, int pageSize = 20)
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
                var result = _contractCompany.GetAllPaging(page, companyId, pageSize, status);
                return new OkObjectResult(new GenericResult(result, true, ErrorMsg.SUCCEED, ErrorCode.SUCCEED_CODE));
            }
            catch (Exception ex)
            {
                return new OkObjectResult(new GenericResult(null, false, ErrorMsg.HAS_ERROR, ErrorCode.HAS_ERROR_CODE));
            }
        }

        #endregion GET

        #region POST

        [HttpPost]
        [Route("AddContractCompany")]
        public IActionResult AddContractCompany([FromBody] ContractCompanyViewModel Vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var allErrors = ModelState.Values.SelectMany(v => v.Errors);
                    return new BadRequestObjectResult(new GenericResult(allErrors, false, ErrorMsg.DATA_REQUEST_IN_VALID, ErrorCode.ERROR_HANDLE_DATA));
                }

                var bCheckExist = _companyDetailService.CheckCompanyExpried(Vm.CompanyId);
                if (bCheckExist != null)
                {
                    return new OkObjectResult(bCheckExist);
                }
                Vm.Status = Status.New;
                var result = _contractCompany.Add(Vm, _companyDetailService.GetById(Vm.CompanyId).CompanyCode);
                return new OkObjectResult(new GenericResult(result, true, ErrorMsg.SUCCEED, ErrorCode.SUCCEED_CODE));
            }
            catch (Exception ex)
            {
                return new OkObjectResult(new GenericResult(null, false, ErrorMsg.HAS_ERROR, ErrorCode.HAS_ERROR_CODE));
            }
        }

        #endregion POST

        #region PUT

        [HttpPost]
        [Route("UpdateContractCompany")]
        public IActionResult UpdateContractCompany([FromBody] ContractCompanyViewModel Vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var allErrors = ModelState.Values.SelectMany(v => v.Errors);
                    return new BadRequestObjectResult(new GenericResult(allErrors, false, ErrorMsg.DATA_REQUEST_IN_VALID, ErrorCode.ERROR_HANDLE_DATA));
                }
                var bCheckExist = _companyDetailService.CheckCompanyExpried(Vm.CompanyId);
                if (bCheckExist != null)
                {
                    return new OkObjectResult(bCheckExist);
                }
                Vm.Status = Status.New;
                _contractCompany.Update(Vm);
                return new OkObjectResult(new GenericResult(null, true, ErrorMsg.SUCCEED, ErrorCode.SUCCEED_CODE));
            }
            catch (Exception ex)
            {
                return new OkObjectResult(new GenericResult(null, false, ErrorMsg.HAS_ERROR, ErrorCode.HAS_ERROR_CODE));
            }
        }

        #endregion PUT

        #region DELETE

        #endregion DELETE

        #region OTHER_METHOD

        private void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        #endregion OTHER_METHOD
    }
}