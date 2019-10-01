using Microsoft.AspNetCore.Mvc;
using NTSoftware.Service.Interface;

namespace NTSoftware.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeContractController : BaseController
    {
        private IEmployeeContractService _iemployeeContractService;
        public EmployeeContractController(IEmployeeContractService iemployeeContractService)
        {
            _iemployeeContractService = iemployeeContractService;
        }

    }
}