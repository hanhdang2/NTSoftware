using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NTSoftware.Core.Models.Models;
using NTSoftware.Core.Shared;
using NTSoftware.Core.Shared.Constants;
using NTSoftware.Core.Shared.Dtos;
using NTSoftware.Service.Interface;

namespace NTSoftware.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractController : BaseController
    {
        private IContractService _icontractService;
     public ContractController(IContractService icontractService)
        {
            _icontractService = icontractService;
        }
        [HttpGet]
        [Route("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            if (id == 0)
            {
                return new BadRequestObjectResult(new GenericResult(new List<Contract>(), false, ErrorMsg.DATA_REQUEST_IN_VALID, ErrorCode.DATA_REQUEST_IN_VALID));
            }
            else
            {
                try
                {
                    var data = _icontractService.GetById(id);
                    return new OkObjectResult(new GenericResult(data, true, ErrorMsg.SUCCEED, ErrorCode.SUCCEED_CODE));
                }
                catch (Exception ex)
                {
                    return new OkObjectResult(new GenericResult(new List<Contract>(), false, ErrorMsg.ERROR_ON_HANDLE_DATA, ErrorCode.ERROR_HANDLE_DATA));

                }
            }
        }
        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            try
            {
                var data = _icontractService.GetAll();
                return new OkObjectResult(new GenericResult(data, true, ErrorMsg.SUCCEED, ErrorCode.SUCCEED_CODE));
            }
            catch (Exception ex)
            {
                return new OkObjectResult(new GenericResult(new List<Contract>(), false, ErrorMsg.ERROR_ON_HANDLE_DATA, ErrorCode.ERROR_HANDLE_DATA));
            }
        }
    }
}