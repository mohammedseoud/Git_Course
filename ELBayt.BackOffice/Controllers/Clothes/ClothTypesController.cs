using ElBayt.Common.Common;
using ElBayt.Common.Enums;
using ElBayt.Common.Core.Logging;
using ElBayt.DTO.ELBayt.DBDTOs;
using ElBayt.Services.IElBaytServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Extensions.Configuration;
using ElBayt.Common.Core.SecurityModels;
using ElBayt.DTO.ELBayt.DTOs;
using Microsoft.AspNetCore.Authorization;
using ELBayt.BackOffice.Core;

namespace ElBayt_ECommerce.WebAPI.Controllers
{

    public class ClothTypesController : ELBaytController
    {
        private readonly IDepartmentsServices _departmentsServices;
        private readonly ILogger _logger;
        private readonly IUserIdentity _userIdentity;

        public ClothTypesController(IDepartmentsServices departmentsServices, ILogger logger, IConfiguration config
            , IUserIdentity userIdentity) : base(config)
        {
            _departmentsServices = departmentsServices ?? throw new ArgumentNullException(nameof(departmentsServices));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _userIdentity = userIdentity ?? throw new ArgumentNullException(nameof(userIdentity));
        }

        #region Types

        [Authorize]
        [HttpPost]
        [Route(nameof(AddNewClothType))]
        [Produces(General.JSONCONTENTTYPE, Type = typeof(ElBaytResponse<string>))]
        public async Task<IActionResult> AddNewClothType([FromForm] AddClothTypeDTO clothType)
        {

            var Response = new ElBaytResponse<string>
            {
                Errors = new List<string>()
            };
            try
            {

                var TypePic = await _departmentsServices.ClothesService.AddNewClothType(Request.Form, _config["FilesInfo:WebFolder"]);

                if (!string.IsNullOrEmpty(TypePic))
                {
                    SaveFile(TypePic, 0);

                    Response.Result = EnumResponseResult.Successed;
                    Response.Data = CommonMessages.SUCCESSFULLY_ADDING;
                }
                else
                {
                    Response.Result = EnumResponseResult.Failed;
                    Response.Data = CommonMessages.NAME_EXISTS;
                }


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
        [Route(nameof(GetClothTypes))]
        [Produces(General.JSONCONTENTTYPE, Type = typeof(ElBaytResponse<List<GetClothTypeDTO>>))]
        public async Task<IActionResult> GetClothTypes()
        {
            var Response = new ElBaytResponse<List<GetClothTypeDTO>>
            {
                Errors = new List<string>()
            };
            try
            {
                var Types = await _departmentsServices.ClothesService.GetClothTypes();
                #region Result
                Response.Result = EnumResponseResult.Successed;
                Response.Data = Types;
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
        [Route(nameof(GetClothType))]
        [Produces(General.JSONCONTENTTYPE, Type = typeof(ElBaytResponse<ClothTypeDTO>))]
        public async Task<IActionResult> GetClothType(int Id)
        {
            var Response = new ElBaytResponse<ClothTypeDTO>
            {
                Errors = new List<string>()
            };

            try
            {
                var Type = await _departmentsServices.ClothesService.GetClothType(Id);
                #region Result
                Response.Result = EnumResponseResult.Successed;
                Response.Data = Type;
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
        [HttpDelete]
        [Route(nameof(DeleteClothType))]
        [Produces(General.JSONCONTENTTYPE, Type = typeof(ElBaytResponse<bool>))]
        public async Task<IActionResult> DeleteClothType(int Id)
        {
            var Response = new ElBaytResponse<bool>
            {
                Errors = new List<string>()
            };
            try
            {
                var URL = await _departmentsServices.ClothesService.DeleteClothType(Id);

                #region Result
                if (!string.IsNullOrEmpty(URL))
                {
                    DeleteFile(URL);
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
        [Route(nameof(UpdateClothType))]
        [Produces(General.JSONCONTENTTYPE, Type = typeof(ElBaytResponse<string>))]
        public async Task<IActionResult> UpdateClothType([FromForm] AddClothTypeDTO clothType)
        {
            var Response = new ElBaytResponse<string>
            {
                Errors = new List<string>()
            };

            try
            {
                var res = await _departmentsServices.ClothesService.UpdateClothType(Request.Form, _config["FilesInfo:WebFolder"], _config["FilesInfo:Directory"]);

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
