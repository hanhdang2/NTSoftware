using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using NTSoftware.Core.Models.Enum;
using NTSoftware.Core.Models.Models;
using NTSoftware.Core.Models.Models.NTSoftware.Core.Models.Models;
using NTSoftware.Core.Shared;
using NTSoftware.Core.Shared.Constants;
using NTSoftware.Core.Shared.Dtos;
using NTSoftware.Service.Interface;
using NTSoftware.Service.Interface.ViewModels;
using NTSoftware.ViewModel.Auth;

namespace NTSoftware.Controllers
{
    public class AccountController : BaseController
    {
        #region Constructor

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _config;
        private readonly IEmailSender _emailSender;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IConfiguration config, IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
            _emailSender = emailSender;
        }

        #endregion Constructor

        #region GET

        [HttpGet]
        [Route("RequestPassword")]
        [AllowAnonymous]
        public async Task<IActionResult> RequestPasword(string email)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(email);
                if (user == null)
                {
                    return new BadRequestObjectResult(new GenericResult(null, false, ErrorMsg.NOT_EXIST_EMAIL, ErrorCode.NO_EMAIL_CODE));
                }
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);

                await _emailSender.SendEmailAsync("lhngoc2497@gmail.com", "", $"<div><span> Bạn vừa gửi yêu cầu lấy lại mật khẩu.Click vào đường dẫn dưới đây để tiếp tục.</span> <a href = 'http://localhost:4200/auth/reset-password?userId={user.Id}&code={code}' style = ' position: absolute; left: 50%;padding: 30px 60px; font - size: 30px;  top: 50 %; transform: translate(-50 %, -50 %);color: #1670f0; text - decoration: none; overflow: hidden;letter - spacing: 2px;text - transform: uppercase; box - shadow: 0 10px 20px rgba(0, 0, 0, 0.2);'>Reset Password</a> </div> ");

                return new OkObjectResult(new GenericResult(null, true, ErrorMsg.SEND_MAIL_SUCCESS, ErrorCode.SEND_EMAIL_SUCCESS));
            }
            catch (Exception ex)
            {
                return new OkObjectResult(new GenericResult(null, false, ErrorMsg.ERROR_ON_HANDLE_DATA, ErrorCode.ERROR_HANDLE_DATA));
            }

        }

        #endregion GET


        #region Post



        #endregion  Post

        #region Put

        [HttpPut]
        [Route("ChangePasswordWithCode")]
        public async Task<IActionResult> ChangePasswordWithCode([FromBody]ChangePasswordViewModel vm)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(vm.UserId);
                if (user == null)
                {
                    return new BadRequestObjectResult(new GenericResult(null, false, ErrorMsg.NOT_EXIST_EMAIL, ErrorCode.NO_EMAIL_CODE));
                }

                var result = await _userManager.ResetPasswordAsync(user, vm.TokenCode, vm.NewPassword);
                if (result.Succeeded)
                {
                    return new OkObjectResult(new GenericResult(result, true, ErrorMsg.SUCCEED, ErrorCode.SUCCEED_CODE));
                }
                return new OkObjectResult(new GenericResult(null, false, ErrorMsg.INVALID_TOKEN_RESET, ErrorCode.INVALID_TOKEN_RESET));
            }
            catch (Exception ex)
            {
                return new OkObjectResult(new GenericResult(null, false, ErrorMsg.ERROR_ON_HANDLE_DATA, ErrorCode.ERROR_HANDLE_DATA));
            }

        }

        [HttpPut]
        [Route("ChangePassword")]
        [Authorize]
        public async Task<IActionResult> UpdatePassword()
        {
            //var user = await _userManager.FindByIdAsync(Inforvm.Id.ToString());
            //if (user == null)
            //    return new BadRequestObjectResult(new GenericResult(null, false, ErrorMsg.HAS_ERROR, ErrorCode.HAS_ERROR_CODE));
            //if (await _userManager.CheckPasswordAsync(user, Inforvm.OldPassword))
            //{
            //    var result = await _userManager.ChangePasswordAsync(user, Inforvm.OldPassword, Inforvm.NewPassword);
            //    if (result.Succeeded)
            //    {
            //        return new OkObjectResult(new GenericResult(result, true, ErrorMsg.SUCCEED, ErrorCode.SUCCEED_CODE));
            //    }
            //    return new BadRequestObjectResult(new GenericResult(null, false, ErrorMsg.ERROR_ON_HANDLE_DATA, ErrorCode.ERROR_HANDLE_DATA));
            //};
            return new BadRequestObjectResult(new GenericResult(null, false, ErrorMsg.ERROR_ON_HANDLE_DATA, ErrorCode.ERROR_HANDLE_DATA));
        }

        #endregion Put

        #region Login Admin
        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginViewModel vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return new BadRequestObjectResult(new GenericResult(null, false, ErrorMsg.DATA_REQUEST_IN_VALID, ErrorCode.DATA_REQUEST_IN_VALID));
                }
                var user = await _userManager.FindByNameAsync(vm.UserName);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(vm.UserName, vm.Password, false, true);
                    if (result.IsLockedOut)
                    {
                        return new OkObjectResult(new GenericResult(null, false, ErrorMsg.LOCK_ACCOUNT, ErrorCode.LOCK_ACCOUNT));
                    }
                    else if (!result.Succeeded)
                    {
                        return new OkObjectResult(new GenericResult(null, false, ErrorMsg.LOGIN_FAILED, ErrorCode.LOGIN_FAILED));
                    }

                    var claims = new[]
                    {
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim("UserId",user.Id.ToString()),
                    new Claim("UserType",user.UserType.ToString()),
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
                    return new OkObjectResult(new GenericResult(token_access, true, ErrorMsg.SUCCEED, ErrorCode.SUCCEED_CODE));
                }
                return new OkObjectResult(new GenericResult(null, false, ErrorMsg.LOGIN_FAILED, ErrorCode.HAS_ERROR_CODE));
            }
            catch (Exception ex)
            {
                return new OkObjectResult(new GenericResult(null, false, ErrorMsg.LOGIN_FAILED, ErrorCode.HAS_ERROR_CODE));
            }


        }

        #endregion Login Admin


        #region Login Other User


        #endregion Login Other User
    }
}