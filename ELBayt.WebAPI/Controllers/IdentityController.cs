using ElBayt.Common.Common;
using ElBayt.Common.Core.ISecurity;
using ElBayt.Common.Core.Logging;
using ElBayt.Common.Core.SecurityModels;
using ElBayt.Common.Enums;
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
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IJWTTokenGenerator _jwttokenGenerator;
        private readonly ILogger _logger;

        public IdentityController(UserManager<IdentityUser> userManager, ILogger logger,
            SignInManager<IdentityUser> signInManager, IJWTTokenGenerator jwttokenGenerator) 
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwttokenGenerator = jwttokenGenerator;
            _logger = logger;
        }


        [HttpPost(nameof(Login))]
        public async Task<IActionResult> Login(LoginModel LoginModel)
        {

            var Response = new ElBaytResponse<string>();
            Response.Errors = new List<string>();
            var correlationGuid = Guid.NewGuid();
            try
            {

                #region Logging info
                _logger.InfoInDetail(LoginModel, correlationGuid, nameof(IdentityController), nameof(Login), 1, User.Identity.Name);
                #endregion Logging info

                var userFromDB = await _userManager.FindByIdAsync(LoginModel.UserId);

                if (userFromDB != null)
                {
                    var IsValid = await _signInManager.CheckPasswordSignInAsync(userFromDB, LoginModel.Password, false);

                    if (IsValid.Succeeded)
                    {

                    }
                }

                #region Result
                Response.Result = EnumResponseResult.SuccessAuthenicated;
                Response.Data = "Success in Adding";
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
                Response.Data = "Failed in Adding";
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
                Response.Data = "Failed in Adding";

                Response.Errors.Add(ex.Message);
                #endregion

                return BadRequest(Response);
            }


            

            return View();
        }
    }
}
