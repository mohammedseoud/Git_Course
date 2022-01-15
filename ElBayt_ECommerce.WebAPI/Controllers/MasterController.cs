using ElBayt.Common.Common;
using ElBayt.Common.Core.Logging;
using ElBayt.Common.Enums;
using ElBayt.Services.IElBaytServices;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace ElBayt_ECommerce.WebAPI.Controllers
{
    [EnableCors(CorsOrigin.LOCAL_ORIGIN)]
    [ApiController]
    [Route("api/v1.0/ElBaytECommerce/Master")]
    public class MasterController : Controller
    {
        private readonly IElBaytServices _elBaytServices;
        private readonly ILogger _logger;

        public MasterController(IElBaytServices elBaytServices, ILogger logger)
        {
            _elBaytServices = elBaytServices ?? throw new ArgumentNullException(nameof(elBaytServices));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [Route(nameof(GetProductDepartments))]
        public ActionResult GetProductDepartments()
        {
            var Response = new ElBaytResponse<object>
            {
                Errors = new List<string>()
            };
            var correlationGuid = Guid.NewGuid();
            try
            {

                #region Logging info
                _logger.InfoInDetail("GetProductDepartments", correlationGuid, nameof(MasterController), nameof(GetProductDepartments), 1, User.Identity.Name);
                #endregion Logging info

                var Departments = _elBaytServices.ProductService.GetProductDepartments();
                #region Result
                Response.Result = EnumResponseResult.Successed;
                Response.Data = Departments;
                #endregion

                return Ok(Response);
            }
            catch (NotFoundException ex)
            {
                #region Logging info

                _logger.ErrorInDetail($"newException GetProductDepartments", correlationGuid,
                    $"{nameof(MasterController)}_{nameof(GetProductDepartments)}_{nameof(NotFoundException)}",
                    ex, 1, User.Identity.Name);

                #endregion Logging info
                #region Result
                Response.Result = EnumResponseResult.Failed;
                Response.Data = null;
                Response.Errors.Add(ex.Message);
                #endregion

                return NotFound(Response);
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail($"newException GetProductDepartments", correlationGuid,
                    $"{nameof(MasterController)}_{nameof(GetProductDepartments)}_{nameof(Exception)}", ex, 1, User.Identity.Name);

                #endregion Logging info
                #region Result
                Response.Result = EnumResponseResult.Failed;
                Response.Data = null;

                Response.Errors.Add(ex.Message);
                #endregion

                return BadRequest(Response);
            }
        }
    }
}
