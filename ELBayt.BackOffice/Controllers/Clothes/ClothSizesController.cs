using ElBayt.Common.Common;
using ElBayt.Common.Enums;
using ElBayt.Common.Core.Logging;
using ElBayt.DTO.ELBayt.DBDTOs;
using ElBayt.Services.IElBaytServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ElBayt.Common.Core.SecurityModels;
using ElBayt.DTO.ELBayt.DTOs;
using Microsoft.AspNetCore.Authorization;
using ELBayt.BackOffice.Core;

namespace ElBayt_ECommerce.WebAPI.Controllers
{

    public class ClothSizesController : ELBaytController
    {
        private readonly IDepartmentsServices _departmentsServices;
        private readonly ILogger _logger;
        private readonly IUserIdentity _userIdentity;

        public ClothSizesController(IDepartmentsServices departmentsServices, ILogger logger, IConfiguration config
            , IUserIdentity userIdentity) : base(config)
        {
            _departmentsServices = departmentsServices ?? throw new ArgumentNullException(nameof(departmentsServices));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _userIdentity = userIdentity ?? throw new ArgumentNullException(nameof(userIdentity));
        }

        #region Sizes

        [Authorize]
        [HttpPost]
        [Route(nameof(AddNewClothSize))]
        [Produces(General.JSONCONTENTTYPE, Type = typeof(ElBaytResponse<string>))]
        public async Task<IActionResult> AddNewClothSize(ClothSizeDTO ClothSize)
        {

            var Response = new ElBaytResponse<string>
            {
                Errors = new List<string>()
            };

            try
            {
                var res = await _departmentsServices.ClothesService.AddNewClothSize(ClothSize);

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
        [Route(nameof(GetSizes))]
        [Produces(General.JSONCONTENTTYPE, Type = typeof(ElBaytResponse<List<GetClothSizeDTO>>))]
        public async Task<IActionResult> GetSizes()
        {
            var Response = new ElBaytResponse<List<GetClothSizeDTO>>
            {
                Errors = new List<string>()
            };

            try
            {
                var Clothes = await _departmentsServices.ClothesService.GetSizes();
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
        [Route(nameof(GetClothSizes))]
        [Produces(General.JSONCONTENTTYPE, Type = typeof(ElBaytResponse<List<GetClothSizeDTO>>))]
        public async Task<IActionResult> GetClothSizes(int ClothId)
        {
            var Response = new ElBaytResponse<List<GetClothSizeDTO>>
            {
                Errors = new List<string>()
            };

            try
            {
                var Clothes = await _departmentsServices.ClothesService.GetClothSizes(ClothId);
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
        [Route(nameof(GetClothSize))]
        [Produces(General.JSONCONTENTTYPE, Type = typeof(ElBaytResponse<ClothSizeDTO>))]
        public async Task<IActionResult> GetClothSize(int Id)
        {
            var Response = new ElBaytResponse<ClothSizeDTO>
            {
                Errors = new List<string>()
            };

            try
            {
                var Size = await _departmentsServices.ClothesService.GetClothSize(Id);
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

        [Authorize]
        [HttpDelete]
        [Route(nameof(DeleteClothSize))]
        [Produces(General.JSONCONTENTTYPE, Type = typeof(ElBaytResponse<bool>))]
        public async Task<IActionResult> DeleteClothSize(int Id)
        {
            var Response = new ElBaytResponse<bool>
            {
                Errors = new List<string>()
            };

            try
            {
                var Result = await _departmentsServices.ClothesService.DeleteClothSize(Id);

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
        [Route(nameof(UpdateClothSize))]
        [Produces(General.JSONCONTENTTYPE, Type = typeof(ElBaytResponse<string>))]
        public async Task<IActionResult> UpdateClothSize(ClothSizeDTO clothSize)
        {
            var Response = new ElBaytResponse<string>
            {
                Errors = new List<string>()
            };

            try
            {
                var res = await _departmentsServices.ClothesService.UpdateClothSize(clothSize);

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
