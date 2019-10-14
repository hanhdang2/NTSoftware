using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NTSoftware.Core.Models.Enum;
using NTSoftware.Core.Models.Models;
using NTSoftware.Core.Models.Models.NTSoftware.Core.Models.Models;
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
    public class DetailUserController : ControllerBase
    {
        #region CONTRUCTOR

        private IDetailUserService _detailUserService;
        private ICompanyDetailService _companyDetailService;
        private IUnitOfWork _unitOfWork;
        private UserManager<AppUser> _userManager;

        public DetailUserController(IDetailUserService detailUserService, ICompanyDetailService companyDetailService, IUnitOfWork unitOfWork, UserManager<AppUser> userManager)
        {
            _detailUserService = detailUserService;
            _companyDetailService = companyDetailService;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        #endregion CONTRUCTOR

        #region GET
        [HttpGet]
        [Route("GetEmployeeSelect")]
        public IActionResult GetEmployeeSelect(int comanyId, List<UserSearchViewModel> lstVm, string keyword = "")
        {
            try
            {
                var checkCompanyExpired = _companyDetailService.CheckCompanyExpried(comanyId);
                if (checkCompanyExpired != null)
                {
                    return new BadRequestObjectResult(checkCompanyExpired);
                }
                var data = _detailUserService.GetUserSelect(lstVm, comanyId, keyword);
                return new OkObjectResult(new GenericResult(data, true, ErrorMsg.SUCCEED, ErrorCode.SUCCEED_CODE));
            }
            catch (Exception ex)
            {
                return new OkObjectResult(new GenericResult(null, false, ErrorMsg.ERROR_ON_HANDLE_DATA, ErrorCode.ERROR_HANDLE_DATA));
            }
        }

        [HttpGet]
        [Route("GetByUser")]
        public async Task<IActionResult> GetByUserAsync(string userName)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(userName);
                if (user == null)
                {
                    return new BadRequestObjectResult(new GenericResult(null, false, ErrorMsg.ACCOUNT_NOT_EXITS, ErrorCode.ERROR_CODE));
                }
                var checkAccount = CheckAccountCanLogin(user);
                if (checkAccount != null)
                {
                    return new OkObjectResult(checkAccount);
                }
                var detail = _detailUserService.GetById(user.Id);
                return new OkObjectResult(new GenericResult(detail, true, ErrorMsg.SUCCEED, ErrorCode.SUCCEED_CODE));
            }
            catch (Exception ex)
            {
                return new OkObjectResult(new GenericResult(null, false, ErrorMsg.ERROR_ON_HANDLE_DATA, ErrorCode.ERROR_HANDLE_DATA));
            }
        }

        #endregion GET

        #region POST

        #endregion POST

        #region PUT

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateAsync([FromBody] DetailUserViewModel Vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var allErrors = ModelState.Values.SelectMany(v => v.Errors);
                    return new BadRequestObjectResult(new GenericResult(allErrors, false, ErrorMsg.DATA_REQUEST_IN_VALID, ErrorCode.ERROR_HANDLE_DATA));
                }
                var user = await _userManager.FindByIdAsync(Vm.Id.ToString());
                if (user == null)
                {
                    return new BadRequestObjectResult(new GenericResult(null, false, ErrorMsg.ACCOUNT_NOT_EXITS, ErrorCode.ERROR_CODE));
                }
                var checkAccount = CheckAccountCanLogin(user);
                if (checkAccount != null)
                {
                    return new OkObjectResult(checkAccount);
                }
                _detailUserService.Update(Vm);
                SaveChanges();
                return new OkObjectResult(new GenericResult(null, true, ErrorMsg.SUCCEED, ErrorCode.SUCCEED_CODE));
            }
            catch (Exception ex)
            {
                return new OkObjectResult(new GenericResult(null, false, ErrorMsg.ERROR_ON_HANDLE_DATA, ErrorCode.ERROR_HANDLE_DATA));
            }
        }

        #endregion PUT

        #region DELETE

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