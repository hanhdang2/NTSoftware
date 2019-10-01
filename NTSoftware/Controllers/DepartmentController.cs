using Microsoft.AspNetCore.Mvc;
using NTSoftware.Service.Interface;

namespace NTSoftware.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : BaseController
    {
        private IDepartmentService _idepartmentService;
        public DepartmentController(IDepartmentService idepartmentService)
        {
            _idepartmentService = idepartmentService;
        }

    }
}