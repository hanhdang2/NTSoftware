using Microsoft.AspNetCore.Mvc;
using NTSoftware.Service.Interface;

namespace NTSoftware.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeProjectController : BaseController
    {
        private IEmployeeProjectService _iemployeeProjectService;
        public EmployeeProjectController(IEmployeeProjectService iemployeeProjectService)
        {
            _iemployeeProjectService = iemployeeProjectService;
        }       
    }
}