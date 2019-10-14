using AutoMapper;
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
using System.Linq;
using System.Threading.Tasks;

namespace NTSoftware.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUserController : BaseController
    {
        #region CONTRUCTOR

        private IMapper _mapper;
        private  IUnitOfWork _unitOfWork;
        private IAppUserService _appUserService;
        private ICompanyDetailService _companyDetailService;
        private IDetailUserService _detailUserService;
        public AppUserController(IMapper mapper, IAppUserService appUserService, IDetailUserService detailUserService, ICompanyDetailService companyDetailService, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _appUserService = appUserService;
            _detailUserService = detailUserService;
            _companyDetailService = companyDetailService;
            _unitOfWork = unitOfWork;
        }

        #endregion CONTRUCTOR

        #region GET

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var result = await _appUserService.GetById(id);
                return new OkObjectResult(new GenericResult(result, true, ErrorMsg.SUCCEED, ErrorCode.SUCCEED_CODE));
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
        public async Task<IActionResult> Add([FromBody]AppUserViewModel Vm)
        {
            if (!ModelState.IsValid)
            {
                var allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return new BadRequestObjectResult(new GenericResult(allErrors, false, ErrorMsg.DATA_REQUEST_IN_VALID, ErrorCode.ERROR_HANDLE_DATA));
            }
            else
            {
                try
                {
                    var result = await _appUserService.AddAsync(Vm);
                    return new OkObjectResult(new GenericResult(result, false, ErrorMsg.SUCCEED, ErrorCode.SUCCEED_CODE));
                }
                catch (Exception ex)
                {
                    return new OkObjectResult(new GenericResult(null, false, ErrorMsg.ERROR_ON_HANDLE_DATA, ErrorCode.ERROR_HANDLE_DATA));
                }
            }
        }

        #endregion POST

        #region PUT

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody]AppUserViewModel Vm)
        {
            if (!ModelState.IsValid)
            {
                var allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return new BadRequestObjectResult(new GenericResult(allErrors, false, ErrorMsg.DATA_REQUEST_IN_VALID, ErrorCode.ERROR_HANDLE_DATA));
            }
            else
            {
                try
                {
                    await _appUserService.UpdateAsync(Vm);
                    return new OkObjectResult(new GenericResult(Vm, false, ErrorMsg.SUCCEED, ErrorCode.SUCCEED_CODE));
                }
                catch (Exception ex)
                {
                    return new OkObjectResult(new GenericResult(null, false, ErrorMsg.ERROR_ON_HANDLE_DATA, ErrorCode.ERROR_HANDLE_DATA));
                }
            }
        }

        #endregion PUT

        #region DELETE

        private void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        #endregion DELETE




    }
}