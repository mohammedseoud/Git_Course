using ElBayt.Common.Common;
using ElBayt.Common.Core.Logging;
using ElBayt.Common.Enums;
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
    [Route("api/v1.0/ElBaytECommerce/Shop")]
    public class ShopController : Controller
    {
        private readonly IElBaytServices _elBaytServices;
        private readonly ILogger _logger;

        public ShopController(IElBaytServices elBaytServices, ILogger logger)
        {
            _elBaytServices = elBaytServices ?? throw new ArgumentNullException(nameof(elBaytServices));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        [HttpGet]
        [Route(nameof(GetShopData))]
        [Produces(General.JSONCONTENTTYPE, Type = typeof(ElBaytResponse<ShopDataDTO>))]
        public async Task<ActionResult> GetShopData(string DepartmentName)
        {
            
            var Response = new ElBaytResponse<ShopDataDTO>
            {
                Errors = new List<string>()
            };
           
            try
            {
                var ShopData = await _elBaytServices.ShopService.GetShopData(DepartmentName);
                #region Result
                Response.Result = EnumResponseResult.Successed;
                Response.Data = ShopData;
                #endregion

                return Ok(Response);
            }
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
        [Route(nameof(GetClothData))]
        [Produces(General.JSONCONTENTTYPE, Type = typeof(ElBaytResponse<ShopProductInfoDTO>))]
        public async Task<ActionResult> GetClothData(string ClothName)
        {

            var Response = new ElBaytResponse<ShopProductInfoDTO>
            {
                Errors = new List<string>()
            };
            try
            { 
                var ProductData = await _elBaytServices.ShopService.GetProductData(ClothName);
                #region Result
                Response.Result = EnumResponseResult.Successed;
                Response.Data = ProductData;
                #endregion

                return Ok(Response);
            }
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
    }
}
