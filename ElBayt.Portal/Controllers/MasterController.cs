using ElBayt.Common.Common;
using ElBayt.Common.Core.Logging;
using ElBayt.Common.Enums;
using ElBayt.DTO.ELBayt.DBDTOs;
using ElBayt.DTO.ELBayt.DTOs;
using ElBayt.Services.IElBaytServices;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElBayt_ECommerce.WebAPI.Controllers
{
    [EnableCors(CorsOrigin.LOCAL_ORIGIN)]
    [ApiController]
    [Route("api/v1.0/ElBaytECommerce/Master")]
    public class MasterController : Controller
    {
        private readonly IDepartmentsServices _departmentServices;
        private readonly ILogger _logger;

        public MasterController(IDepartmentsServices departmentServices, ILogger logger)
        {
            _departmentServices = departmentServices ?? throw new ArgumentNullException(nameof(departmentServices));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [Route(nameof(GetClothDepartments))]
        [Produces(General.JSONCONTENTTYPE, Type = typeof(ElBaytResponse<List<GetClothDepartmentDTO>>))]
        public async Task<ActionResult> GetClothDepartments()
        {
            var Response = new ElBaytResponse<List<GetClothDepartmentDTO>>
            {
                Errors = new List<string>()
            };
            try
            {

                var Departments = await _departmentServices.ClothesService.GetClothDepartments();
                #region Result
                Response.Result = EnumResponseResult.Successed;
                Response.Data = Departments;
                #endregion

                return Ok(Response);
            }
            //catch (NotFoundException ex)
            //{
            //    #region Logging info

            //    _logger.ErrorInDetail($"newException GetProductDepartments", correlationGuid,
            //        $"{nameof(MasterController)}_{nameof(GetProductDepartments)}_{nameof(NotFoundException)}",
            //        ex, 1, User.Identity.Name);

            //    #endregion Logging info
            //    #region Result
            //    Response.Result = EnumResponseResult.Failed;
            //    Response.Data = null;
            //    Response.Errors.Add(ex.Message);
            //    #endregion

            //    return NotFound(Response);
            //}
            catch (Exception ex)
            {

                #region Result
                Response.Result = EnumResponseResult.Failed;
                Response.Data = null;

                Response.Errors.Add(ex.Message);
                #endregion

                return BadRequest(Response);
            }
        }

        [HttpGet]
        [Route(nameof(GetClothTypes))]
        [Produces(General.JSONCONTENTTYPE, Type = typeof(ElBaytResponse<List<GetClothTypeDTO>>))]
        public async Task< ActionResult> GetClothTypes()
        {
            var Response = new ElBaytResponse<List<GetClothTypeDTO>>
            {
                Errors = new List<string>()
            };
            try
            {

                var Types = await _departmentServices.ClothesService.GetClothTypes();
                #region Result
                Response.Result = EnumResponseResult.Successed;
                Response.Data = Types;
                #endregion

                return Ok(Response);
            }
            //catch (NotFoundException ex)
            //{
            //    #region Logging info

            //    _logger.ErrorInDetail($"newException GetProductTypes", correlationGuid,
            //        $"{nameof(MasterController)}_{nameof(GetProductTypes)}_{nameof(NotFoundException)}",
            //        ex, 1, User.Identity.Name);

            //    #endregion Logging info
            //    #region Result
            //    Response.Result = EnumResponseResult.Failed;
            //    Response.Data = null;
            //    Response.Errors.Add(ex.Message);
            //    #endregion

            //    return NotFound(Response);
            //}
            catch (Exception ex)
            {
                #region Logging info

                //_logger.ErrorInDetail($"newException GetProductTypes", correlationGuid,
                //    $"{nameof(MasterController)}_{nameof(GetProductTypes)}_{nameof(Exception)}", ex, 1, User.Identity.Name);

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
