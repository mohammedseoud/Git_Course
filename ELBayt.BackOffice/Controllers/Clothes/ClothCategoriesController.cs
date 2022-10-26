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
using Microsoft.AspNetCore.Authorization;
using ELBayt.BackOffice.Core;
using ElBayt.DTO.ELBayt.DTOs;

namespace ElBayt_ECommerce.WebAPI.Controllers
{

    public class ClothCategoriesController : ELBaytController
    {
        private readonly IDepartmentsServices _departmentsServices;
        private readonly ILogger _logger;
        private readonly IUserIdentity _userIdentity;

        public ClothCategoriesController(IDepartmentsServices departmentsServices, ILogger logger, IConfiguration config
            , IUserIdentity userIdentity):base(config)
        {
            _departmentsServices = departmentsServices ?? throw new ArgumentNullException(nameof(departmentsServices));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _userIdentity = userIdentity ?? throw new ArgumentNullException(nameof(userIdentity));
        }

        #region Categories

        [Authorize]
        [HttpPost]
        [Route(nameof(AddNewClothCategory))]
        [Produces(General.JSONCONTENTTYPE, Type = typeof(ElBaytResponse<string>))]
        public async Task<IActionResult> AddNewClothCategory(ClothCategoryDTO ClothCategory)
        {

            var Response = new ElBaytResponse<string>
            {
                Errors = new List<string>()
            };
            try
            {
                var res = await _departmentsServices.ClothesService.AddNewClothCategory(ClothCategory);

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
        [Route(nameof(GetClothCategories))]
        [Produces(General.JSONCONTENTTYPE, Type = typeof(ElBaytResponse<List<GetClothCategoryDTO>>))]
        public async Task<IActionResult> GetClothCategories()
        {
            var Response = new ElBaytResponse<List<GetClothCategoryDTO>>
            {
                Errors = new List<string>()
            };
            try
            {
                var ClothesCategories = await _departmentsServices.ClothesService.GetClothCategories();
                #region Result
                Response.Result = EnumResponseResult.Successed;
                Response.Data = ClothesCategories;
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
        [Route(nameof(GetClothCategory))]
        [Produces(General.JSONCONTENTTYPE, Type = typeof(ElBaytResponse<ClothCategoryDTO>))]
        public async Task<IActionResult> GetClothCategory(int Id)
        {
            var Response = new ElBaytResponse<ClothCategoryDTO>
            {
                Errors = new List<string>()
            };
            try
            {

                var Category = await _departmentsServices.ClothesService.GetClothCategory(Id);
                #region Result
                Response.Result = EnumResponseResult.Successed;
                Response.Data = Category;
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
        [Route(nameof(DeleteClothCategory))]
        [Produces(General.JSONCONTENTTYPE, Type = typeof(ElBaytResponse<bool>))]
        public async Task<IActionResult> DeleteClothCategory(int Id)
        {
            var Response = new ElBaytResponse<bool>
            {
                Errors = new List<string>()
            };
            try
            {
                var URL = await _departmentsServices.ClothesService.DeleteClothCategory(Id);

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
        [Route(nameof(UpdateClothCategory))]
        [Produces(General.JSONCONTENTTYPE, Type = typeof(ElBaytResponse<string>))]
        public async Task<IActionResult> UpdateClothCategory(ClothCategoryDTO clothCategory)
        {
            var Response = new ElBaytResponse<string>
            {
                Errors = new List<string>()
            };
            try
            {
                var res = await _departmentsServices.ClothesService.UpdateClothCategory(clothCategory, _config["FilesInfo:Path"]);

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

        [Authorize]
        [HttpPost]
        [Route(nameof(AddClothCategoryBrands))]
        [Produces(General.JSONCONTENTTYPE, Type = typeof(ElBaytResponse<bool>))]
        public async Task<IActionResult> AddClothCategoryBrands(SelectedCategoryBrandsDTO SelectedBrands)
        {

            var Response = new ElBaytResponse<bool>
            {
                Errors = new List<string>()
            };
            try
            {

                var res = await _departmentsServices.ClothesService.AddClothCategoryBrands(SelectedBrands);
                if (res == CommonMessages.SUCCESSFULLY_ADDING)
                {
                    Response.Result = EnumResponseResult.Successed;
                    Response.Data = true;
                    return Ok(Response);
                }
                Response.Errors.Add(res);
                Response.Result = EnumResponseResult.Failed;
                Response.Data = false;
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
        [HttpGet]
        [Route(nameof(GetSelectedClothCategoryBrands))]
        [Produces(General.JSONCONTENTTYPE, Type = typeof(ElBaytResponse<List<ClothCategoryBrandsDTO>>))]
        public async Task<IActionResult> GetSelectedClothCategoryBrands(int clothcategoryId)
        {
            var Response = new ElBaytResponse<List<ClothCategoryBrandsDTO>>
            {
                Errors = new List<string>()
            };

            try
            {
                var brands = await _departmentsServices.ClothesService.GetClothCategoryBrands(clothcategoryId);
                #region Result
                Response.Result = EnumResponseResult.Successed;
                Response.Data = brands;
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
        [HttpPost]
        [Route(nameof(AddClothCategorySizes))]
        [Produces(General.JSONCONTENTTYPE, Type = typeof(ElBaytResponse<bool>))]
        public async Task<IActionResult> AddClothCategorySizes(SelectedCategorySizesDTO SelectedSizes)
        {

            var Response = new ElBaytResponse<bool>
            {
                Errors = new List<string>()
            };
            try
            {

                var res = await _departmentsServices.ClothesService.AddClothCategorySizes(SelectedSizes);
                if (res == CommonMessages.SUCCESSFULLY_ADDING)
                {
                    Response.Result = EnumResponseResult.Successed;
                    Response.Data = true;
                    return Ok(Response);
                }
                Response.Errors.Add(res);
                Response.Result = EnumResponseResult.Failed;
                Response.Data = false;
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
        [HttpGet]
        [Route(nameof(GetSelectedClothCategorySizes))]
        [Produces(General.JSONCONTENTTYPE, Type = typeof(ElBaytResponse<List<ClothCategorySizesDTO>>))]
        public async Task<IActionResult> GetSelectedClothCategorySizes(int clothcategoryId)
        {
            var Response = new ElBaytResponse<List<ClothCategorySizesDTO>>
            {
                Errors = new List<string>()
            };

            try
            {
                var sizes = await _departmentsServices.ClothesService.GetClothCategorySizes(clothcategoryId);
                #region Result
                Response.Result = EnumResponseResult.Successed;
                Response.Data = sizes;
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

        #endregion


    }
}
