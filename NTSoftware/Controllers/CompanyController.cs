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
using NTSoftware.Service.Interface.ViewModels;

namespace NTSoftware.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : BaseController
    {
        private ICompanyService _icompanyService;
    public CompanyController(ICompanyService icompanyService)
        {
            _icompanyService = icompanyService;
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            if (id == 0)
            {
                return new BadRequestObjectResult(new GenericResult( new Company(), false, ErrorMsg.DATA_REQUEST_IN_VALID, ErrorCode.DATA_REQUEST_IN_VALID));
            }
            else
            {
                try
                {
                    var data = _icompanyService.GetById(id);
                    return new OkObjectResult(new GenericResult(data, true, ErrorMsg.SUCCEED, ErrorCode.SUCCEED_CODE));
                }
                catch (Exception ex)
                {
                    return new OkObjectResult(new GenericResult(new Company(), false, ErrorMsg.ERROR_ON_HANDLE_DATA, ErrorCode.ERROR_HANDLE_DATA));
                }
            }
        }
        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            try
            {
                var data = _icompanyService.GetAll();
                return new OkObjectResult(new GenericResult(data, true, ErrorMsg.SUCCEED, ErrorCode.SUCCEED_CODE));
            }
            catch (Exception ex)
            {
                return new OkObjectResult(new GenericResult(new List<Company>(), false, ErrorMsg.ERROR_ON_HANDLE_DATA, ErrorCode.ERROR_HANDLE_DATA));
            }
        }
        [HttpPost]
        [Route("Add")]
        public IActionResult Add([FromBody] CompanyViewModel Vm)
        {
            if (!ModelState.IsValid)
            {
                var allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return new BadRequestObjectResult(new GenericResult(allErrors,false,ErrorMsg.DATA_REQUEST_IN_VALID,ErrorCode.DATA_REQUEST_IN_VALID));
            }
            else
            {
                try
                {
                   var data= _icompanyService.Add(Vm);

                    return new OkObjectResult(new GenericResult(data ,true, ErrorMsg.SUCCEED,ErrorCode.SUCCEED_CODE));
                }
                catch (Exception ex)
                {
                    return new OkObjectResult(new GenericResult(new Company(), false, ErrorMsg.ERROR_ON_HANDLE_DATA, ErrorCode.ERROR_HANDLE_DATA));
                }
            }
        }
        [HttpGet]
        [Route("GetAllPaging")]
        public IActionResult GetAllPaging(int page, int pageSize, string namecompany, string phonenumber, string address, string representativename, string positionrepresentative)
        {
            try
            {
                var data = _icompanyService.GetAllPaging(page, pageSize,namecompany,phonenumber,address,representativename,positionrepresentative);
                return new OkObjectResult(new GenericResult(data, true,ErrorMsg.SUCCEED,ErrorCode.SUCCEED_CODE));
            }
            catch (Exception ex)
            {
                return new OkObjectResult(new GenericResult(new Company(), false, ErrorMsg.ERROR_ON_HANDLE_DATA, ErrorCode.ERROR_HANDLE_DATA));
            }
        }
        [HttpPut]
        [Route("Update")]
        public IActionResult Update([FromBody]CompanyViewModel Vm)
        {
            if (!ModelState.IsValid)
            {
                var allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return new BadRequestObjectResult(new GenericResult(false, allErrors));
            }
            else
            {
                try
                {
                       _icompanyService.Update(Vm);

                    return new OkObjectResult(new GenericResult( new Company() ,true, ErrorMsg.SUCCEED,ErrorCode.SUCCEED_CODE));
                }
                catch (Exception ex)
                {
                    return new OkObjectResult(new GenericResult(new Company(),false, ErrorMsg.ERROR_ON_HANDLE_DATA,ErrorCode.ERROR_HANDLE_DATA));
                }
            }
        }

    }
}