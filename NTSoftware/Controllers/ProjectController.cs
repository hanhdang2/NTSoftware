using Microsoft.AspNetCore.Mvc;
using NTSoftware.Service.Interface;

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
    }
}