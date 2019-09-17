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
    public class ProjectController : BaseController
    {
        private IProjectService _iprojectService;
        public ProjectController(IProjectService iprojectService)
        {
            _iprojectService = iprojectService;
        }
        [HttpGet]
        [Route("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            if (id == 0)
            {
                return new BadRequestObjectResult(new GenericResult(new Project(), false, ErrorMsg.DATA_REQUEST_IN_VALID, ErrorCode.DATA_REQUEST_IN_VALID));
            }
            else
            {
                try
                {
                    var data = _iprojectService.GetById(id);
                    return new OkObjectResult(new GenericResult(data, true, ErrorMsg.SUCCEED, ErrorCode.SUCCEED_CODE));
                }
                catch (Exception ex)
                {
                    return new OkObjectResult(new GenericResult(new Project(), false, ErrorMsg.ERROR_ON_HANDLE_DATA, ErrorCode.ERROR_HANDLE_DATA));
                }
            }
        }
        [HttpGet]
        [Route("GetAllPaging")]
        public IActionResult GetAllPaging(int page, int pageSize, string description = "")
        {
            try
            {
                var data = _iprojectService.GetAllPaging(page, pageSize, description);
                return new OkObjectResult(new GenericResult(data, true, ErrorMsg.SUCCEED, ErrorCode.SUCCEED_CODE));
            }
            catch (Exception ex)
            {
                return new OkObjectResult(new GenericResult(new Project(), false, ErrorMsg.ERROR_ON_HANDLE_DATA, ErrorCode.ERROR_HANDLE_DATA));
            }
        }
        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            try
            {
                var data = _iprojectService.GetAll();
                return new OkObjectResult(new GenericResult(data, true, ErrorMsg.SUCCEED, ErrorCode.SUCCEED_CODE));
            }
            catch (Exception ex)
            {
                return new OkObjectResult(new GenericResult(new List<Project>(), false, ErrorMsg.ERROR_ON_HANDLE_DATA, ErrorCode.ERROR_HANDLE_DATA));
            }
        }
        [HttpPost]
        [Route("Add")]
        public IActionResult Add([FromBody] ProjectViewModel Vm)
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
                    var data = _iprojectService.Add(Vm);

                    return new OkObjectResult(new GenericResult(data, true, ErrorMsg.SUCCEED, ErrorCode.SUCCEED_CODE));
                }
                catch (Exception ex)
                {
                    return new OkObjectResult(new GenericResult(new Project(), false, ErrorMsg.ERROR_ON_HANDLE_DATA, ErrorCode.ERROR_HANDLE_DATA));
                }
            }
        }
    }
}