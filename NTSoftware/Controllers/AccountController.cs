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
using Microsoft.Extensions.Primitives;
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
        private readonly ICompanyDetailService _companyDetailService;
        private readonly IEmailSender _emailSender;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IConfiguration config, IEmailSender emailSender, ICompanyDetailService companyDetailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
            _emailSender = emailSender;
            _companyDetailService = companyDetailService;
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
                    return new OkObjectResult(new GenericResult(null, false, ErrorMsg.NOT_EXIST_EMAIL, ErrorCode.NO_EMAIL_CODE));
                }
                var checkLogin = CheckAccountCanLogin(user);
                if (checkLogin != null)
                {
                    return new OkObjectResult(checkLogin);
                }
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);

                await _emailSender.SendEmailAsync(email, "", $"<div><span> Bạn vừa gửi yêu cầu lấy lại mật khẩu.Click vào đường dẫn dưới đây để tiếp tục.</span> <a href = 'http://localhost:4200/auth/reset-password?userId={user.Id}&code={code}' style = ' position: absolute; left: 50%;padding: 30px 60px; font - size: 30px;  top: 50 %; transform: translate(-50 %, -50 %);color: #1670f0; text - decoration: none; overflow: hidden;letter - spacing: 2px;text - transform: uppercase; box - shadow: 0 10px 20px rgba(0, 0, 0, 0.2);'>Reset Password</a> </div> ");

                return new OkObjectResult(new GenericResult(null, true, ErrorMsg.SEND_MAIL_SUCCESS, ErrorCode.SEND_EMAIL_SUCCESS));
            }
            catch (Exception ex)
            {
                return new OkObjectResult(new GenericResult(null, false, ErrorMsg.ERROR_ON_HANDLE_DATA, ErrorCode.ERROR_HANDLE_DATA));
            }

        }

        #endregion GET

        #region Post

        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginViewModel vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var allErrors = ModelState.Values.SelectMany(v => v.Errors);
                    return new BadRequestObjectResult(new GenericResult(allErrors, false, ErrorMsg.DATA_REQUEST_IN_VALID, ErrorCode.ERROR_HANDLE_DATA));
                }
                var user = await _userManager.FindByNameAsync(vm.UserName);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(vm.UserName, vm.Password, false, true);
                    if (result.IsLockedOut)
                    {
                        return new OkObjectResult(new GenericResult(null, false, ErrorMsg.LOCK_ACCOUNT, ErrorCode.LOCK_ACCOUNT));
                    }
                    else if (!result.Succeeded || user.DeleteFlag == StatusDelete.DELETED)
                    {
                        return new OkObjectResult(new GenericResult(null, false, ErrorMsg.LOGIN_FAILED, ErrorCode.LOGIN_FAILED));
                    }
                    var checkLogin = CheckAccountCanLogin(user);
                    if (checkLogin != null)
                    {
                        return new OkObjectResult(checkLogin);
                    }
                    var token_access = SignToken(user);
                    return new OkObjectResult(new GenericResult(token_access, true, ErrorMsg.SUCCEED, ErrorCode.SUCCEED_CODE));
                }
                return new OkObjectResult(new GenericResult(null, false, ErrorMsg.LOGIN_FAILED, ErrorCode.HAS_ERROR_CODE));
            }
            catch (Exception ex)
            {
                return new OkObjectResult(new GenericResult(null, false, ErrorMsg.ERROR_ON_HANDLE_DATA, ErrorCode.HAS_ERROR_CODE));
            }


        }

        #endregion  Post

        #region Put

        [HttpPut]
        [Route("ChangePasswordWithCode")]
        public async Task<IActionResult> ChangePasswordWithCode([FromBody]ChangePasswordCodeViewModel vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var allErrors = ModelState.Values.SelectMany(v => v.Errors);
                    return new BadRequestObjectResult(new GenericResult(allErrors, false, ErrorMsg.DATA_REQUEST_IN_VALID, ErrorCode.ERROR_HANDLE_DATA));
                }
                var user = await _userManager.FindByIdAsync(vm.userId);
                if (user == null)
                {
                    return new BadRequestObjectResult(new GenericResult(null, false, ErrorMsg.NOT_EXIST_EMAIL, ErrorCode.NO_EMAIL_CODE));
                }
                var checkLogin = CheckAccountCanLogin(user);
                if (checkLogin != null)
                {
                    return new OkObjectResult(checkLogin);
                }
                var result = await _userManager.ResetPasswordAsync(user, vm.tokenCode, vm.newPassword);
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
        [Route("ChangePasswordWithOldPassword")]
        [Authorize]
        public async Task<IActionResult> UpdatePassword([FromBody] ChangePasswordWithOldPasswordViewModel vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var allErrors = ModelState.Values.SelectMany(v => v.Errors);
                    return new BadRequestObjectResult(new GenericResult(allErrors, false, ErrorMsg.DATA_REQUEST_IN_VALID, ErrorCode.ERROR_HANDLE_DATA));
                }
                var user = await _userManager.FindByIdAsync(vm.userName);
                if (user == null)
                {
                    return new OkObjectResult(new GenericResult(null, false, ErrorMsg.NOT_EXIST_EMAIL, ErrorCode.NO_EMAIL_CODE));
                }

                var checkLogin = CheckAccountCanLogin(user);
                if (checkLogin != null)
                {
                    return new OkObjectResult(checkLogin);
                }
                if (await _userManager.CheckPasswordAsync(user, vm.oldPassword))
                {
                    var result = await _userManager.ChangePasswordAsync(user, vm.oldPassword, vm.newPassword);
                    if (result.Succeeded)
                    {
                        return new OkObjectResult(new GenericResult(result, true, ErrorMsg.SUCCEED, ErrorCode.SUCCEED_CODE));
                    }
                    return new OkObjectResult(new GenericResult(null, false, ErrorMsg.CHANGE_PASSWORD_FAILED, ErrorCode.ERROR_CODE));
                };

                return new OkObjectResult(new GenericResult(null, false, ErrorMsg.OLD_PASSWORD_INCORECT, ErrorCode.ERROR_CODE));
            }
            catch (Exception ex)
            {
                return new OkObjectResult(new GenericResult(null, false, ErrorMsg.ERROR_ON_HANDLE_DATA, ErrorCode.ERROR_CODE));
            }
        }

        [HttpPut]
        [Route("ChangePasswordWithoutOldPassword")]
        [Authorize]
        public async Task<IActionResult> UpdatePassword([FromBody] ChangePasswordWithoutOldPassword vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var allErrors = ModelState.Values.SelectMany(v => v.Errors);
                    return new BadRequestObjectResult(new GenericResult(allErrors, false, ErrorMsg.DATA_REQUEST_IN_VALID, ErrorCode.ERROR_HANDLE_DATA));
                }
                var user = await _userManager.FindByEmailAsync(vm.userName);
                var userCurrent = await _userManager.FindByEmailAsync(vm.curentUser);
                if (user == null || userCurrent == null)
                {
                    return new OkObjectResult(new GenericResult(null, false, ErrorMsg.NOT_EXIST_EMAIL, ErrorCode.NO_EMAIL_CODE));
                }
                var checkLogin = CheckAccountCanLogin(userCurrent);
                if (checkLogin != null)
                {
                    return new OkObjectResult(checkLogin);
                }
                if ((int)userCurrent.UserType > (int)user.UserType)
                {
                    return new BadRequestObjectResult(new GenericResult(null, false, ErrorMsg.UN_AUTHOZIRED, ErrorCode.ERROR_CODE));

                }
                var resultRemove = await _userManager.RemovePasswordAsync(user);
                if (resultRemove.Succeeded)
                {
                    var resultSetNew = await _userManager.AddPasswordAsync(user, vm.newPassword);
                    if (resultSetNew.Succeeded)
                    {
                        return new OkObjectResult(new GenericResult(null, true, ErrorMsg.CHANGE_PASSWORD_SUCCESS, ErrorCode.SUCCEED_CODE));
                    }
                }


                return new OkObjectResult(new GenericResult(null, false, ErrorMsg.CHANGE_PASSWORD_FAILED, ErrorCode.ERROR_CODE));
            }
            catch (Exception ex)
            {
                return new OkObjectResult(new GenericResult(null, false, ErrorMsg.ERROR_ON_HANDLE_DATA, ErrorCode.ERROR_CODE));
            }
        }

        #endregion Put

        #region Private Method

        private string SignToken(AppUser user)
        {
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
              expires: DateTime.UtcNow.AddHours(12),
                signingCredentials: creds);
            var token_access = new JwtSecurityTokenHandler().WriteToken(token);
            return token_access;
        }

        private GenericResult CheckAccountCanLogin(AppUser user)
        {
            if (user.UserType != Roles.AdminCompany)
            {
                return _companyDetailService.CheckCompanyExpried(user.CompanyId);
            }
            if (user.UserType == Roles.Employee && (user.Status == Status.New || user.Status == Status.Expired))
            {
                return new GenericResult(null, false, ErrorMsg.ACCOUNT_EXPRIED_NEW, ErrorCode.LOGIN_FAILED);
            }
            return null;
        }
        #endregion Private Method
    }
}