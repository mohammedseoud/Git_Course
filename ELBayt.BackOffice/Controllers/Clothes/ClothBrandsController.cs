using ElBayt.Common.Common;
using ElBayt.Common.Enums;
using ElBayt.Common.Core.Logging;
using ElBayt.DTO.ELBayt.DBDTOs;
using ElBayt.Services.IElBaytServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using System.IO;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using ElBayt.Common.Core.SecurityModels;
using ElBayt.DTO.ELBayt.DTOs;
using Microsoft.AspNetCore.Authorization;
using ELBayt.BackOffice.Core;

namespace ElBayt_ECommerce.WebAPI.Controllers
{
    
    public class ClothBrandsController : ELBaytController
    {
        private readonly IDepartmentsServices _departmentsServices;
        private readonly ILogger _logger;
        private readonly IUserIdentity _userIdentity;

        public ClothBrandsController(IDepartmentsServices departmentsServices, ILogger logger, IConfiguration config
            , IUserIdentity userIdentity):base(config)
        {
            _departmentsServices = departmentsServices ?? throw new ArgumentNullException(nameof(departmentsServices));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _userIdentity = userIdentity ?? throw new ArgumentNullException(nameof(userIdentity));
        }

        #region Brands

        [Authorize]
        [HttpPost]
        [Route(nameof(AddNewClothBrand))]
        [Produces(General.JSONCONTENTTYPE, Type = typeof(ElBaytResponse<string>))]
        public async Task<IActionResult> AddNewClothBrand([FromForm] AddClothBrandDTO clothBrand)
        {

            var Response = new ElBaytResponse<string>
            {
                Errors = new List<string>()
            };

            try
            {
                var BrandPic = await _departmentsServices.ClothesService.AddNewClothBrand(Request.Form, _config["FilesInfo:WebFolder"]);

                if (!string.IsNullOrEmpty(BrandPic))
                {
                    SaveFile(BrandPic, 0);

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
        [Route(nameof(GetClothBrands))]
        [Produces(General.JSONCONTENTTYPE, Type = typeof(ElBaytResponse<List<GetClothBrandDTO>>))]
        public async Task<IActionResult> GetClothBrands()
        {
            var Response = new ElBaytResponse<List<GetClothBrandDTO>>
            {
                Errors = new List<string>()
            };

            try
            {
                var Brands = await _departmentsServices.ClothesService.GetClothBrands();
                #region Result
                Response.Result = EnumResponseResult.Successed;
                Response.Data = Brands;
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
        [Route(nameof(GetClothBrand))]
        [Produces(General.JSONCONTENTTYPE, Type = typeof(ElBaytResponse<ClothBrandDTO>))]
        public async Task<IActionResult> GetClothBrand(int Id)
        {
            var Response = new ElBaytResponse<ClothBrandDTO>
            {
                Errors = new List<string>()
            };

            try
            {
                var Brand = await _departmentsServices.ClothesService.GetClothBrand(Id);
                #region Result
                Response.Result = EnumResponseResult.Successed;
                Response.Data = Brand;
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
        [Route(nameof(DeleteClothBrand))]
        [Produces(General.JSONCONTENTTYPE, Type = typeof(ElBaytResponse<bool>))]
        public async Task<IActionResult> DeleteClothBrand(int Id)
        {
            var Response = new ElBaytResponse<bool>
            {
                Errors = new List<string>()
            };

            try
            {
                var URL = await _departmentsServices.ClothesService.DeleteClothBrand(Id);

                #region Result
                if (!string.IsNullOrEmpty(URL))
                {
                    var files = URL.Split("\\");
                    var fullpath = Path.Combine(_config["FilesInfo:Directory"], URL.Remove(URL.IndexOf(files[^1])));
                    if (Directory.Exists(fullpath))
                        Directory.Delete(fullpath, true);
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
        [Route(nameof(UpdateClothBrand))]
        [Produces(General.JSONCONTENTTYPE, Type = typeof(ElBaytResponse<string>))]
        public async Task<IActionResult> UpdateClothBrand([FromForm] AddClothBrandDTO clothBrand)
        {
            var Response = new ElBaytResponse<string>
            {
                Errors = new List<string>()
            };

            try
            {
                var res = await _departmentsServices.ClothesService.UpdateClothBrand(Request.Form, _config["FilesInfo:WebFolder"], _config["FilesInfo:Directory"]);

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
