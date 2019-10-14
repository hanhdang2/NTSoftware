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
                return new BadRequestObjectResult(new GenericResult(null, false, ErrorMsg.HAS_ERROR, ErrorCode.HAS_ERROR_CODE));
            }
        }

        #endregion GET

        #region POST      


        #endregion POST

        #region PUT

        [HttpPut]
        [Route("Update")]
        public IActionResult Update([FromBody]AppUserViewModel Vm)
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
                    var result = _appUserService.UpdateAsync(Vm);
                    return new OkObjectResult(result);
                }
                catch (Exception ex)
                {
                    return new OkObjectResult(new GenericResult(new AppUser(), false, ErrorMsg.HAS_ERROR, ErrorCode.HAS_ERROR_CODE));
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