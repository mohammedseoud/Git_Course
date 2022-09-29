﻿using ElBayt.Common.Common;
using ElBayt.Common.Core.ISecurity;
using ElBayt.Common.Core.Logging;
using ElBayt.Common.Core.Mapping;
using ElBayt.Common.Core.SecurityModels;
using ElBayt.Common.Enums;
using ElBayt.Common.Security;
using ElBayt.DTO.ELBayt.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ELBayt.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1.0/ElBayt/Identity")]
    public class IdentityController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IJWTTokenGenerator _jwttokenGenerator;
        private readonly ILogger _logger;
        private readonly ITypeMapper _mapper;

        public IdentityController(UserManager<AppUser> userManager, ILogger logger,
            SignInManager<AppUser> signInManager, IJWTTokenGenerator jwttokenGenerator, ITypeMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwttokenGenerator = jwttokenGenerator;
            _logger = logger;
            _mapper = mapper;
        }

        
        [HttpPost]
        [Route(nameof(Login))]
        public async Task<IActionResult> Login(LoginModel LoginModel)
        {
            var Response = new ElBaytResponse<AppUserDataDTO>
            {
                Errors = new List<string>()
            };
            var correlationGuid = Guid.NewGuid();
            try
            {

                #region Logging info
                _logger.InfoInDetail(LoginModel, correlationGuid, nameof(IdentityController), nameof(Login), 1, User.Identity.Name);
                #endregion Logging info

                var userFromDB = await _userManager.FindByEmailAsync(LoginModel.UserName);

                if (userFromDB == null)
                    userFromDB = await _userManager.FindByNameAsync(LoginModel.UserName);

                if (userFromDB != null)
                {
                    var passwordHasher = new PasswordHasher<AppUser>();
                    var passresult = passwordHasher.VerifyHashedPassword(userFromDB, userFromDB.PasswordHash, LoginModel.Password);

                    if (passresult == PasswordVerificationResult.Success)
                    {
                        var user = new AppUserDataDTO
                        {
                            Name = userFromDB.Name,
                            Email = userFromDB.Email,
                            UserName = userFromDB.UserName,
                            token = _jwttokenGenerator.GenerateToken(userFromDB)

                        };
                        Response.Result = EnumResponseResult.Authenicated;
                        Response.Data = user;
                    }
                }

                #region Result

                #endregion

                return Ok(Response);
            }
            catch (NotFoundException ex)
            {
                #region Logging info

                _logger.ErrorInDetail($"newException {LoginModel}", correlationGuid,
                    $"{nameof(IdentityController)}_{nameof(Login)}_{nameof(NotFoundException)}",
                    ex, 1, User.Identity.Name);

                #endregion Logging info


                #region Result
                Response.Result = EnumResponseResult.UnAuthenicated;
                Response.Data = null;
                Response.Errors.Add(ex.Message);
                #endregion

                return NotFound(Response);
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail($"newException {LoginModel}", correlationGuid,
                    $"{nameof(IdentityController)}_{nameof(Login)}_{nameof(Exception)}",
                    ex, 1, User.Identity.Name);

                #endregion Logging info

                #region Result
                Response.Result = EnumResponseResult.UnAuthenicated;
                Response.Data = null;

                Response.Errors.Add(ex.Message);
                #endregion

                return BadRequest(Response);
            }
        }

        //[Authorize]
        [HttpPost]
        [Route(nameof(Register))]
        public async Task<IActionResult> Register(AppUserDTO _user)
        {
            AppUser user = _mapper.Map<AppUserDTO, AppUser>(_user);

            var Response = new ElBaytResponse<string>
            {
                Errors = new List<string>()
            };
            var correlationGuid = Guid.NewGuid();
            try
            {

                #region Logging info
                _logger.InfoInDetail(_user, correlationGuid, nameof(IdentityController), nameof(Register), 1, User.Identity.Name);
                #endregion Logging info

                var Existeduser = await _userManager.FindByEmailAsync(user.Email);

                var result = new IdentityResult();

                if (Existeduser == null)
                {
                    user.Id = Guid.NewGuid().ToString();
                    var passwordHasher = new PasswordHasher<AppUser>();
                    var Newpass = passwordHasher.HashPassword(user, user.PasswordHash);
                    user.PasswordHash = Newpass;
                    result = await _userManager.CreateAsync(user);
                }
                else
                {
                    Response.Result = EnumResponseResult.Failed;
                    Response.Data = "This User Is Existed";
                    return Ok(Response);
                }


                #region Result
                if (result.Succeeded)
                {

                    Response.Result = EnumResponseResult.Successed;
                    Response.Data = CommonMessages.SUCCESSFULLY_ADDING;

                }
                else
                {
                    Response.Result = EnumResponseResult.Failed;
                    Response.Data = CommonMessages.FAILED_ADDING;
                }

                #endregion


                return Ok(Response);
            }
            catch (NotFoundException ex)
            {
                #region Logging info

                _logger.ErrorInDetail($"newException {_user}", correlationGuid,
                    $"{nameof(IdentityController)}_{nameof(Register)}_{nameof(NotFoundException)}",
                    ex, 1, User.Identity.Name);

                #endregion Logging info


                #region Result
                Response.Result = EnumResponseResult.UnAuthenicated;
                Response.Data = "Failed in Adding";
                Response.Errors.Add(ex.Message);
                #endregion

                return NotFound(Response);
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail($"newException {_user}", correlationGuid,
                    $"{nameof(IdentityController)}_{nameof(Register)}_{nameof(Exception)}",
                    ex, 1, User.Identity.Name);

                #endregion Logging info

                #region Result
                Response.Result = EnumResponseResult.UnAuthenicated;
                Response.Data = "Failed in Adding";


                Response.Errors.Add(ex.Message);
                #endregion

                return BadRequest(Response);
            }

        }
    }
}
