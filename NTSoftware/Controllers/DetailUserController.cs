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
using NTSoftware.Core.Shared.Interface;
using NTSoftware.Service.Interface;
using NTSoftware.Service.Interface.ViewModels;

namespace NTSoftware.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetailUserController : ControllerBase
    {
        #region CONTRUCTOR

        private IDetailUserService _detailUserService;
        private ICompanyDetailService _companyDetailService;
        private IUnitOfWork _unitOfWork;

        public DetailUserController(IDetailUserService detailUserService, ICompanyDetailService companyDetailService, IUnitOfWork unitOfWork)
        {
            _detailUserService = detailUserService;
            _companyDetailService = companyDetailService;
            _unitOfWork = unitOfWork;
        }

        #endregion CONTRUCTOR

        #region GET

        #endregion GET

        #region POST

        #endregion POST

        #region PUT

        #endregion PUT

        #region DELETE

        #endregion DELETE

        #region OTHER_METHOD

        #endregion OTHER_METHOD

    }
}