using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NTSoftware.Core.Models.Enum;
using NTSoftware.Core.Models.Models;
using NTSoftware.Core.Models.Models.NTSoftware.Core.Models.Models;
using NTSoftware.Core.Shared;
using NTSoftware.Core.Shared.Dtos;
using NTSoftware.Service.Interface.ViewModels;

namespace NTSoftware.Controllers
{
    public class AccountController : BaseController
    {
        #region Constructor
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _config;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
        }
        #endregion Constructor


        #region Login Admin
        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return new BadRequestObjectResult(model);
                }
                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, true);
                    if (result.IsLockedOut)
                    {
                        return new ObjectResult(new GenericResult(false, ErrorMsg.LOGIN_FAILED));
                    }
                    else if (!result.Succeeded)
                    {
                        return new BadRequestObjectResult(result.ToString());
                    }
                   
                    var claims = new[]
                    {
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim("UserId",user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                        _config["Tokens:Issuer"],
                        claims,
                        expires: DateTime.UtcNow.AddHours(2),
                        signingCredentials: creds);
                    var token_access = new JwtSecurityTokenHandler().WriteToken(token);
                    if (model.isSave)
                    {
                        user.Token = token.ToString();         
                        var data = await _userManager.UpdateAsync(user);
                        // cập nhật trường token bên với giá trị là token bên trên
                    }
                    return new ObjectResult(new GenericResult(true, token_access));
                }
                    return new ObjectResult(new GenericResult(false, ErrorMsg.LOGIN_FAILED));
            }
            catch (Exception ex)
            {
                return new ObjectResult(new GenericResult(false, ex.Message));
            }


        }

        #endregion Login Admin


        #region Login Other User


        #endregion Login Other User
    }
}