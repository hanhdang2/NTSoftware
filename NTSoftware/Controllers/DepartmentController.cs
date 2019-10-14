using Microsoft.AspNetCore.Mvc;
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
    public class DepartmentController : BaseController
    {
        #region CONTRUCTOR

        private IDepartmentService _departmentService;
        private IUnitOfWork _unitOfWork;
        private ICompanyDetailService _companyDetailService;

        public DepartmentController(IDepartmentService departmentService, IUnitOfWork unitOfWork, ICompanyDetailService companyDetailService)
        {
            _departmentService = departmentService;
            _unitOfWork = unitOfWork;
            _companyDetailService = companyDetailService;
        }

        #endregion CONTRUCTOR

        #region GET

        [HttpGet]
        [Route("GetPaging")]
        public IActionResult GetPaging(int companyId, int page = 1, int pageSize = 20)
        {
            try
            {
                var companyExist = _companyDetailService.CheckCompanyExpried(companyId);
                if (companyExist != null)
                {
                    return new OkObjectResult(companyExist);
                }
                if (page < 0)
                {
                    page = 1;
                }
                if (pageSize < 0)
                {
                    pageSize = 20;
                }
                var result = _departmentService.GetAllPaging(page, pageSize, companyId);
                return new OkObjectResult(new GenericResult(result, true, ErrorMsg.SUCCEED, ErrorCode.SUCCEED_CODE));
            }
            catch (Exception ex)
            {
                return new OkObjectResult(new GenericResult(null, false, ErrorMsg.HAS_ERROR, ErrorCode.HAS_ERROR_CODE));
            }
        }

        [HttpGet]
        [Route("GetById")]
        public IActionResult GetById(int companyId, int id)
        {
            try
            {
                var companyExist = _companyDetailService.CheckCompanyExpried(companyId);
                if (companyExist != null)
                {
                    return new OkObjectResult(companyExist);
                }
                var result = _departmentService.GetById(id, companyId);
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
        [Route("Add")]
        public async Task<IActionResult> Add([FromBody] DetailDepartmentViewModel Vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var allErrors = ModelState.Values.SelectMany(v => v.Errors);
                    return new BadRequestObjectResult(new GenericResult(allErrors, false, ErrorMsg.DATA_REQUEST_IN_VALID, ErrorCode.ERROR_HANDLE_DATA));
                }
                var companyExist = _companyDetailService.CheckCompanyExpried(Vm.CompanyId);
                if (companyExist != null)
                {
                    return new OkObjectResult(companyExist);
                }
                var result = await _departmentService.Add(Vm);
                return new OkObjectResult(new GenericResult(result, true, ErrorMsg.SUCCEED, ErrorCode.SUCCEED_CODE));
            }
            catch (Exception ex)
            {
                return new OkObjectResult(new GenericResult(null, false, ErrorMsg.HAS_ERROR, ErrorCode.HAS_ERROR_CODE));
            }
        }

        #endregion POST

        #region PUT

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody] DetailDepartmentViewModel Vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var allErrors = ModelState.Values.SelectMany(v => v.Errors);
                    return new BadRequestObjectResult(new GenericResult(allErrors, false, ErrorMsg.DATA_REQUEST_IN_VALID, ErrorCode.ERROR_HANDLE_DATA));
                }
                var companyExist = _companyDetailService.CheckCompanyExpried(Vm.CompanyId);
                if (companyExist != null)
                {
                    return new OkObjectResult(companyExist);
                }
                await _departmentService.Update(Vm);
                return new OkObjectResult(new GenericResult(null, true, ErrorMsg.SUCCEED, ErrorCode.SUCCEED_CODE));
            }
            catch (Exception ex)
            {
                return new OkObjectResult(new GenericResult(null, false, ErrorMsg.HAS_ERROR, ErrorCode.HAS_ERROR_CODE));
            }
        }

        #endregion PUT

        #region DELETE

        [HttpDelete]
        [Route("Delete")]
        public  IActionResult Delete(int id, int companyId)
        {
            try
            {
                var companyExist = _companyDetailService.CheckCompanyExpried(companyId);
                if (companyExist != null)
                {
                    return new OkObjectResult(companyExist);
                }
                var lstEmployee = _departmentService.GetEmployeeCount(companyId);
                if (lstEmployee > 0)
                {
                    return new OkObjectResult(new GenericResult(null, false, ErrorMsg.DEPARTMENT_HAS_EMPLOYEE, ErrorCode.DEPARTMENT_HAS_EMPLOYEE));
                }
                _departmentService.Delete(id);
                return new OkObjectResult(new GenericResult(null, true, ErrorMsg.SUCCEED, ErrorCode.SUCCEED_CODE));
            }
            catch (Exception ex)
            {
                return new OkObjectResult(new GenericResult(null, false, ErrorMsg.HAS_ERROR, ErrorCode.HAS_ERROR_CODE));
            }
        }

        #endregion DELETE

        #region OTHER_METHOD

        #endregion OTHER_METHOD


    }
}