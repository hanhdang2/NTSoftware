using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NTSoftware.Core.Shared;
using NTSoftware.Core.Shared.Dtos;

namespace NTSoftware.Controllers
{
    public class AccountController : BaseController
    {
        #region Constructor

        #endregion Constructor


        #region Login Admin
        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login()
        {
            try
            {
                //if (!ModelState.IsValid)
                //{
                //    return new BadRequestObjectResult(model);
                //}

                return new ObjectResult(new GenericResult(false, "Login failed!"));
            }
            catch (Exception ex)
            {
                return new ObjectResult(new GenericResult(false, "Login failed!"));
            }


        }

        #endregion Login Admin


        #region Login Other User

        #endregion Login Other User
    }
}