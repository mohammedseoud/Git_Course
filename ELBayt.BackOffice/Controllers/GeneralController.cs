﻿using ElBayt.Common.Common;
using ElBayt.Common.Core.Logging;
using ElBayt.Common.Core.SecurityModels;
using ElBayt.Common.Enums;
using ElBayt.DTO.ELBayt.DBDTOs;
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
        private readonly IConfiguration _config;
        private readonly IUserIdentity _userIdentity;

        public GeneralController(IElBaytServices elBaytServices, ILogger logger, IConfiguration config
            , IUserIdentity userIdentity)
        {
            _elBaytServices = elBaytServices ?? throw new ArgumentNullException(nameof(elBaytServices));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _userIdentity = userIdentity ?? throw new ArgumentNullException(nameof(userIdentity));
        }

        #region Color

        [Authorize]
        [HttpPost]
        [Route(nameof(AddNewColor))]
        [ProducesResponseType(typeof(ElBaytResponse<string>), 200)]
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
            catch (NotFoundException ex)
            {
                #region Result
                Response.Result = EnumResponseResult.Failed;
                Response.Data = CommonMessages.FAILED_ADDING;
                Response.Errors.Add(ex.Message);
                #endregion

                return NotFound(Response);
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
        [ProducesResponseType(typeof(ElBaytResponse<object>), 200)]
        public ActionResult GetColors()
        {
            var _user = User.Identity.Name;


            var Response = new ElBaytResponse<object>
            {
                Errors = new List<string>()
            };
            
            try
            {

                var Clothes = _elBaytServices.GeneralService.GetColors();
                #region Result
                Response.Result = EnumResponseResult.Successed;
                Response.Data = Clothes;
                #endregion

                return Ok(Response);
            }
            catch (NotFoundException ex)
            {
                #region Result
                Response.Result = EnumResponseResult.Failed;
                Response.Data = null;
                Response.Errors.Add(ex.Message);
                #endregion

                return NotFound(Response);
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
        [ProducesResponseType(typeof(ElBaytResponse<ColorDTO>), 200)]
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
            catch (NotFoundException ex)
            {
                #region Result
                Response.Result = EnumResponseResult.Failed;
                Response.Data = null;
                Response.Errors.Add(ex.Message);
                #endregion

                return NotFound(Response);
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
        [ProducesResponseType(typeof(ElBaytResponse<bool>), 200)]
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
                if (Result != CommonMessages.SUCCESSFULLY_DELETING)
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
            catch (NotFoundException ex)
            {
                #region Result
                Response.Result = EnumResponseResult.Failed;
                Response.Data = false;
                Response.Errors.Add(ex.Message);
                #endregion

                return NotFound(Response);
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
        [ProducesResponseType(typeof(ElBaytResponse<string>), 200)]
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
            catch (NotFoundException ex)
            {
                #region Result
                Response.Result = EnumResponseResult.Failed;
                Response.Data = CommonMessages.FAILED_UPDATING;
                Response.Errors.Add(ex.Message);
                #endregion

                return NotFound(Response);
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
