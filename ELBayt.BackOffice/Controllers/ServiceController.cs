using ElBayt.Common.Common;
using ElBayt.Common.Core.Logging;
using ElBayt.Common.Core.SecurityModels;
using ElBayt.Common.Enums;
using ElBayt.DTO.ELBayt.DBDTOs;
using ElBayt.Services.IElBaytServices;
using ELBayt.BackOffice.Core;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ELBayt.WebAPI.Controllers
{
    [EnableCors("_LocalOrigin")]
    [ApiController]
    [Route("api/v1.0/Service/Product")]
    public class ServiceController : ELBaytController
    {
        private readonly IElBaytServices _elBaytServices;
        private readonly ILogger _logger;

        //public ServiceController(IElBaytServices elBaytServices, ILogger logger)
        //{
        //    _elBaytServices = elBaytServices ?? throw new ArgumentNullException(nameof(elBaytServices));
        //    _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        //}

        //[HttpPost]
        //[Route(nameof(AddNewService))]
        //public async Task<IActionResult> AddNewService(ServiceDTO Service)
        //{

        //    var Response = new ElBaytResponse<string>();
        //    Response.Errors = new List<string>();
        //    var correlationGuid = Guid.NewGuid();
        //    try
        //    {

        //        #region Logging info
        //        _logger.InfoInDetail(Service, correlationGuid, nameof(ServiceController), nameof(AddNewService), 1, User.Identity.Name);
        //        #endregion Logging info

        //        await _elBaytServices.Service_Service.AddNewService(Service);

        //        #region Result
        //        Response.Result = EnumResponseResult.Successed;
        //        Response.Data = "Success in Adding";
        //        #endregion

        //        return Ok(Response);
        //    }
        //    catch (NotFoundException ex)
        //    {
        //        #region Logging info

        //        _logger.ErrorInDetail($"newException {Service}", correlationGuid, $"{nameof(ServiceController)}_{nameof(AddNewService)}_{nameof(NotFoundException)}", ex, 1, User.Identity.Name);

        //        #endregion Logging info


        //        #region Result
        //        Response.Result = EnumResponseResult.Failed;
        //        Response.Data = "Failed in Adding";
        //        Response.Errors.Add(ex.Message);
        //        #endregion

        //        return NotFound(Response);
        //    }
        //    catch (Exception ex)
        //    {
        //        #region Logging info

        //        _logger.ErrorInDetail($"newException {Service}", correlationGuid,
        //            $"{nameof(ServiceController)}_{nameof(AddNewService)}_{nameof(Exception)}", ex, 1, User.Identity.Name);

        //        #endregion Logging info

        //        #region Result
        //        Response.Result = EnumResponseResult.Failed;
        //        Response.Data = "Failed in Adding";

        //        Response.Errors.Add(ex.Message);
        //        #endregion

        //        return BadRequest(Response);
        //    }
        //}

        //[HttpPost]
        //[Route(nameof(AddNewServiceDepartment))]
        //public async Task<IActionResult> AddNewServiceDepartment(ServiceDepartmentDTO ServiceDepartment)
        //{

        //    var Response = new ElBaytResponse<string>();
        //    Response.Errors = new List<string>();
        //    var correlationGuid = Guid.NewGuid();
        //    try
        //    {

        //        #region Logging info
        //        _logger.InfoInDetail(ServiceDepartment, correlationGuid, nameof(ServiceController), nameof(AddNewServiceDepartment), 1, User.Identity.Name);
        //        #endregion Logging info

        //        await _elBaytServices.Service_Service.AddNewServiceDepartment(ServiceDepartment);

        //        #region Result
        //        Response.Result = EnumResponseResult.Successed;
        //        Response.Data = "Success in Adding";
        //        #endregion

        //        return Ok(Response);
        //    }
        //    catch (NotFoundException ex)
        //    {
        //        #region Logging info

        //        _logger.ErrorInDetail($"newException {ServiceDepartment}", correlationGuid, $"{nameof(ServiceController)}_{nameof(AddNewServiceDepartment)}_{nameof(NotFoundException)}", ex, 1, User.Identity.Name);

        //        #endregion Logging info


        //        #region Result
        //        Response.Result = EnumResponseResult.Failed;
        //        Response.Data = "Failed in Adding";
        //        Response.Errors.Add(ex.Message);
        //        #endregion

        //        return NotFound(Response);
        //    }
        //    catch (Exception ex)
        //    {
        //        #region Logging info

        //        _logger.ErrorInDetail($"newException {ServiceDepartment}", correlationGuid,
        //            $"{nameof(ServiceController)}_{nameof(AddNewServiceDepartment)}_{nameof(Exception)}", ex, 1, User.Identity.Name);

        //        #endregion Logging info

        //        #region Result
        //        Response.Result = EnumResponseResult.Failed;
        //        Response.Data = "Failed in Adding";

        //        Response.Errors.Add(ex.Message);
        //        #endregion

        //        return BadRequest(Response);
        //    }
        //}
    }
}
