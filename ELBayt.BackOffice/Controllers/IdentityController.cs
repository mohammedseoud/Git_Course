using ElBayt.Common.Common;
using ElBayt.Common.Core.ISecurity;
using ElBayt.Common.Core.Logging;
using ElBayt.Common.Core.Mapping;
using ElBayt.Common.Core.SecurityModels;
using ElBayt.Common.Enums;
using ElBayt.Common.Security;
using ElBayt.DTO.ELBayt.DTOs;
using ELBayt.BackOffice.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ELBayt.WebAPI.Controllers
{
    public class IdentityController : ELBaytController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IJWTTokenGenerator _jwttokenGenerator;
        private readonly ILogger _logger;
        private readonly ITypeMapper _mapper;

        public IdentityController(UserManager<AppUser> userManager, ILogger logger,
            IConfiguration config, SignInManager<AppUser> signInManager,
            IJWTTokenGenerator jwttokenGenerator, ITypeMapper mapper) : base(config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwttokenGenerator = jwttokenGenerator;
            _logger = logger;
            _mapper = mapper;
        }

        
        [HttpPost]
        [Route(nameof(Login))]
        [Produces(General.JSONCONTENTTYPE, Type = typeof(ElBaytResponse<AppUserDataDTO>))]
        public async Task<IActionResult> Login(LoginModel LoginModel)
        {
            var Response = new ElBaytResponse<AppUserDataDTO>
            {
                Errors = new List<string>()
            };
            
            try
            {

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
            catch (Exception ex)
            {
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
        [Produces(General.JSONCONTENTTYPE, Type = typeof(ElBaytResponse<string>))]
        public async Task<IActionResult> Register(AppUserDTO _user)
        {
            AppUser user = _mapper.Map<AppUserDTO, AppUser>(_user);

            var Response = new ElBaytResponse<string>
            {
                Errors = new List<string>()
            };            
            try
            {

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
            catch (Exception ex)
            {

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
