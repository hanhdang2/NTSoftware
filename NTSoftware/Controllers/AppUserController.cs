using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NTSoftware.Core.Models.Models.NTSoftware.Core.Models.Models;
using NTSoftware.Core.Shared;
using NTSoftware.Core.Shared.Constants;
using NTSoftware.Core.Shared.Dtos;
using NTSoftware.Service.Interface;
using NTSoftware.Service.Interface.ViewModels;

namespace NTSoftware.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUserController : BaseController
    {
        
        private IAppUserService _iuserService;
        public AppUserController(IAppUserService iuserService)
        {
            _iuserService = iuserService;
        }
        
        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add([FromBody] AppUserViewModel Vm)
        {
            if (!ModelState.IsValid)
            {
                var allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return new BadRequestObjectResult(new GenericResult( allErrors,false,ErrorMsg.DATA_REQUEST_IN_VALID,ErrorCode.DATA_REQUEST_IN_VALID));
            }
            else
            {
                try
                {
                    var result = await _iuserService.AddAsync(Vm);
                    if (result)
                    {
                        return new OkObjectResult(new GenericResult(result,true,ErrorMsg.SUCCEED,ErrorCode.SUCCEED_CODE));
                    }
                    else
                    {
                        return new OkObjectResult(new GenericResult(new List <AppUser>(),false,ErrorMsg.ERROR_ON_HANDLE_DATA,ErrorCode.HANDLE_ERROR_CODE));
                    }
                }
                catch (Exception)
                {
                    return new BadRequestObjectResult(new GenericResult(new List<AppUser>(), false, ErrorMsg.HAS_ERROR, ErrorCode.HAS_ERROR_CODE));
                    throw;
                }
            }
        }

        [HttpPut]
        [Route("Update")]
        public IActionResult Update([FromBody]AppUserViewModel Vm)
        {
            if (!ModelState.IsValid)
            {
                var allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return new BadRequestObjectResult(new GenericResult(allErrors, false, ErrorMsg.DATA_REQUEST_IN_VALID, ErrorCode.DATA_REQUEST_IN_VALID));
            }
            else
            {
                try
                {
                     _iuserService.UpdateAsync(Vm);

                    return new OkObjectResult(new GenericResult(new List<AppUser>(), true, ErrorMsg.SUCCEED, ErrorCode.SUCCEED_CODE));
                }
                catch (Exception ex)
                {
                    return new OkObjectResult(new GenericResult(new List<AppUser>(), false, ErrorMsg.HAS_ERROR, ErrorCode.HAS_ERROR_CODE));
                }
            }
        }
  
    }
}