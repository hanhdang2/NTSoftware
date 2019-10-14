using Microsoft.AspNetCore.Mvc;
using NTSoftware.Core.Models.Enum;
using NTSoftware.Core.Models.Models.NTSoftware.Core.Models.Models;
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
    public class ProjectController : BaseController
    {
        #region CONTRUCTOR

        private IProjectService _projectService;
        private IEmployeeProjectService _employeeProjectService;
        private ICompanyDetailService _companyDetailService;
        private IUnitOfWork _unitOfWork;

        public ProjectController(IProjectService projectService, IEmployeeProjectService employeeProjectService, ICompanyDetailService companyDetailService, IUnitOfWork unitOfWork)
        {
            _projectService = projectService;
            _employeeProjectService = employeeProjectService;
            _companyDetailService = companyDetailService;
            _unitOfWork = unitOfWork;
        }

        #endregion CONTRUCTOR

        #region GET
        [HttpGet]
        [Route("GetPaging")]
        public IActionResult GetPaging(int comanyId, int page = 1, int pageSize = 20, string description = "")
        {
            try
            {
                var checkCompanyExpired = _companyDetailService.CheckCompanyExpried(comanyId);
                if (checkCompanyExpired != null)
                {
                    return new BadRequestObjectResult(checkCompanyExpired);
                }
                var data = _projectService.GetAllPaging(page, pageSize, comanyId, description);
                return new OkObjectResult(new GenericResult(data, true, ErrorMsg.SUCCEED, ErrorCode.SUCCEED_CODE));
            }
            catch (Exception ex)
            {
                return new OkObjectResult(new GenericResult(null, false, ErrorMsg.ERROR_ON_HANDLE_DATA, ErrorCode.ERROR_HANDLE_DATA));
            }
        }
        [HttpGet]
        [Route("GetById")]
        public IActionResult GetById(int comanyId, int id)
        {
            try
            {
                var checkCompanyExpired = _companyDetailService.CheckCompanyExpried(comanyId);
                if (checkCompanyExpired != null)
                {
                    return new BadRequestObjectResult(checkCompanyExpired);
                }
                var data = _projectService.GetById(id, comanyId);
                return new OkObjectResult(new GenericResult(data, true, ErrorMsg.SUCCEED, ErrorCode.SUCCEED_CODE));
            }
            catch (Exception ex)
            {
                return new OkObjectResult(new GenericResult(null, false, ErrorMsg.ERROR_ON_HANDLE_DATA, ErrorCode.ERROR_HANDLE_DATA));
            }
        }

        #endregion GET

        #region POST

        [HttpPost]
        [Route("Add")]
        public IActionResult Add([FromBody] ProjectDetailViewModel Vm)
        {
            try
            {
                var checkCompanyExpired = _companyDetailService.CheckCompanyExpried(Vm.CompanyId);
                if (checkCompanyExpired != null)
                {
                    return new BadRequestObjectResult(checkCompanyExpired);
                }
                var data = _projectService.Add(Vm);
                return new OkObjectResult(new GenericResult(data, true, ErrorMsg.SUCCEED, ErrorCode.SUCCEED_CODE));
            }
            catch (Exception ex)
            {
                return new OkObjectResult(new GenericResult(null, false, ErrorMsg.ERROR_ON_HANDLE_DATA, ErrorCode.ERROR_HANDLE_DATA));
            }
        }

        #endregion POST

        #region PUT

        [HttpPut]
        [Route("Update")]
        public IActionResult Update([FromBody] ProjectDetailViewModel Vm)
        {
            try
            {
                var checkCompanyExpired = _companyDetailService.CheckCompanyExpried(Vm.CompanyId);
                if (checkCompanyExpired != null)
                {
                    return new BadRequestObjectResult(checkCompanyExpired);
                }
                _projectService.Update(Vm);
                return new OkObjectResult(new GenericResult(Vm, true, ErrorMsg.SUCCEED, ErrorCode.SUCCEED_CODE));
            }
            catch (Exception ex)
            {
                return new OkObjectResult(new GenericResult(null, false, ErrorMsg.ERROR_ON_HANDLE_DATA, ErrorCode.ERROR_HANDLE_DATA));
            }
        }

        #endregion PUT

        #region DELETE

        [HttpDelete]
        [Route("Delete")]
        public IActionResult Delete(int comanyId, int id)
        {
            try
            {
                var checkCompanyExpired = _companyDetailService.CheckCompanyExpried(comanyId);
                if (checkCompanyExpired != null)
                {
                    return new BadRequestObjectResult(checkCompanyExpired);
                }
                _projectService.Delete(id);
                return new OkObjectResult(new GenericResult(null, true, ErrorMsg.SUCCEED, ErrorCode.SUCCEED_CODE));
            }
            catch (Exception ex)
            {
                return new OkObjectResult(new GenericResult(null, false, ErrorMsg.ERROR_ON_HANDLE_DATA, ErrorCode.ERROR_HANDLE_DATA));
            }
        }

        #endregion DELETE

        #region OTHER_METHOD
        private GenericResult CheckAccountCanLogin(AppUser user)
        {
            if (user.UserType == Roles.AdminNT)
            {
                return null;
            }
            var companyExpried = _companyDetailService.CheckCompanyExpried(user.CompanyId);
            if (user.UserType == Roles.AdminCompany)
            {
                return companyExpried;
            }
            else
            {
                if (companyExpried == null)
                {
                    if (user.Status == Status.New || user.Status == Status.Expired)
                    {
                        return new GenericResult(null, false, ErrorMsg.ACCOUNT_EXPRIED_NEW, ErrorCode.EXPIRES_EMPLOYEE_CODE);
                    }
                }
                return companyExpried;
            }
        }

        private void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        #endregion OTHER_METHOD

    }
}