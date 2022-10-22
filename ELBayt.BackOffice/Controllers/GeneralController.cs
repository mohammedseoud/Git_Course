using ElBayt.Common.Common;
using ElBayt.Common.Core.Logging;
using ElBayt.Common.Core.SecurityModels;
using ElBayt.Common.Enums;
using ElBayt.DTO.ELBayt.DBDTOs;
using ElBayt.DTO.ELBayt.DTOs;
using ElBayt.Services.IElBaytServices;
using ELBayt.BackOffice.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ELBayt.WebAPI.Controllers
{
    [Authorize]   
    public class GeneralController : ELBaytController
    {
        private readonly IElBaytServices _elBaytServices;
        private readonly ILogger _logger;
        private readonly IUserIdentity _userIdentity;

        public GeneralController(IElBaytServices elBaytServices, ILogger logger, IConfiguration config
            , IUserIdentity userIdentity) : base(config)
        {
            _elBaytServices = elBaytServices ?? throw new ArgumentNullException(nameof(elBaytServices));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _userIdentity = userIdentity ?? throw new ArgumentNullException(nameof(userIdentity));
        }

        #region Color

        [Authorize]
        [HttpPost]
        [Route(nameof(AddNewColor))]
        [Produces(General.JSONCONTENTTYPE, Type = typeof(ElBaytResponse<string>))]
        public async Task<IActionResult> AddNewColor(ColorDTO Color)
        {

            var Response = new ElBaytResponse<string>
            {
                Errors = new List<string>()
            };
            
            try
            {

                var res = await _elBaytServices.GeneralService.AddNewColor(Color);

                #region Result
                if (res == EnumInsertingResult.Successed)
                {
                    Response.Result = EnumResponseResult.Successed;
                    Response.Data = CommonMessages.SUCCESSFULLY_ADDING;
                }
                else
                {
                    Response.Result = EnumResponseResult.Failed;
                    Response.Data = CommonMessages.NAME_EXISTS;
                }
                #endregion

                return Ok(Response);
            }
            catch (Exception ex)
            {
                #region Result
                Response.Result = EnumResponseResult.Failed;
                Response.Data = CommonMessages.FAILED_ADDING;

                Response.Errors.Add(ex.Message);
                #endregion

                return BadRequest(Response);
            }
        }

        [Authorize]
        [HttpGet]
        [Route(nameof(GetColors))]
        [Produces(General.JSONCONTENTTYPE, Type = typeof(ElBaytResponse<List<GetColorDTO>>))]
        public async Task<IActionResult> GetColors()
        {

            var Response = new ElBaytResponse<List<GetColorDTO>>
            {
                Errors = new List<string>()
            };

            try
            {

                var Clothes = await _elBaytServices.GeneralService.GetColors();
                #region Result
                Response.Result = EnumResponseResult.Successed;
                Response.Data = Clothes;
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

        [Authorize]
        [HttpGet]
        [Route(nameof(GetColor))]
        [Produces(General.JSONCONTENTTYPE, Type = typeof(ElBaytResponse<ColorDTO>))]
        public async Task<ActionResult> GetColor(int Id)
        {
            var Response = new ElBaytResponse<ColorDTO>
            {
                Errors = new List<string>()
            };
            
            try
            {

                var Size = await _elBaytServices.GeneralService.GetColor(Id);
                #region Result
                Response.Result = EnumResponseResult.Successed;
                Response.Data = Size;
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


        [HttpDelete]
        [Route(nameof(DeleteColor))]
        [Produces(General.JSONCONTENTTYPE, Type = typeof(ElBaytResponse<bool>))]
        public async Task<ActionResult> DeleteColor(int Id)
        {
            var Response = new ElBaytResponse<bool>
            {
                Errors = new List<string>()
            };
            
            try
            {

                var Result = await _elBaytServices.GeneralService.DeleteColor(Id);

                #region Result
                if (Result == CommonMessages.SUCCESSFULLY_DELETING)
                {
                    Response.Result = EnumResponseResult.Successed;
                    Response.Data = true;
                }
                else
                {
                    Response.Errors.Add(CommonMessages.ITEM_NOT_EXISTS);
                    Response.Result = EnumResponseResult.Failed;
                    Response.Data = false;

                }
                #endregion


                return Ok(Response);
            }
            catch (Exception ex)
            {
                #region Result
                Response.Result = EnumResponseResult.Failed;
                Response.Data = false;

                Response.Errors.Add(ex.Message);
                #endregion

                return BadRequest(Response);
            }
        }

        [Authorize]
        [HttpPut]
        [Route(nameof(UpdateColor))]
        [Produces(General.JSONCONTENTTYPE, Type = typeof(ElBaytResponse<string>))]
        public async Task<ActionResult> UpdateColor(ColorDTO Color)
        {
           var Response = new ElBaytResponse<string>
            {
                Errors = new List<string>()
            };
            
            try
            {

                var res = await _elBaytServices.GeneralService.UpdateColor(Color);

                #region Result
                if (res == EnumUpdatingResult.Successed)
                {

                    Response.Result = EnumResponseResult.Successed;
                    Response.Data = CommonMessages.SUCCESSFULLY_ADDING;
                }
                else
                {
                    Response.Result = EnumResponseResult.Failed;
                    Response.Data = CommonMessages.NAME_EXISTS;
                }
                #endregion


                return Ok(Response);
            }
            catch (Exception ex)
            {
                #region Result
                Response.Result = EnumResponseResult.Failed;
                Response.Data = CommonMessages.FAILED_UPDATING;

                Response.Errors.Add(ex.Message);
                #endregion

                return BadRequest(Response);
            }
        }

        #endregion

    }
}
