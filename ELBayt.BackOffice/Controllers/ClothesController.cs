﻿using ElBayt.Common.Common;
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
    
    public class ClothesController : ELBaytController
    {
        private readonly IDepartmentsServices _departmentsServices;
        private readonly ILogger _logger;
        private readonly IConfiguration _config;
        private readonly IUserIdentity _userIdentity;

        public ClothesController(IDepartmentsServices departmentsServices, ILogger logger, IConfiguration config
            , IUserIdentity userIdentity)
        {
            _departmentsServices = departmentsServices ?? throw new ArgumentNullException(nameof(departmentsServices));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _config= config ?? throw new ArgumentNullException(nameof(config));
            _userIdentity = userIdentity ?? throw new ArgumentNullException(nameof(userIdentity));
        }



        #region Types

        [Authorize]
        [HttpPost]
        [Route(nameof(AddNewClothType))]
        [Produces("application/json" ,Type = typeof(ElBaytResponse<string>))]
        public async Task<IActionResult> AddNewClothType([FromForm]AddClothTypeDTO clothType)
        {

            var Response = new ElBaytResponse<string>
            {
                Errors = new List<string>()
            };
            try
            {

                var clothtype = await _departmentsServices.ClothesService.AddNewClothType(Request.Form, _config["FilesInfo:WebFolder"]);

                if (clothtype != null)
                {
                    var path = Path.Combine(_config["FilesInfo:Directory"], clothtype.TypePic);
                    var files = path.Split("\\");
                    var PicDirectory = path.Remove(path.IndexOf(files[^1]));

                    if (!Directory.Exists(PicDirectory))
                        Directory.CreateDirectory(PicDirectory);

                    using var stream = new FileStream(path, FileMode.Create);
                    Request.Form.Files[0].CopyTo(stream);

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
        [Route(nameof(GetClothTypes))]
        [ProducesResponseType(typeof(ElBaytResponse<List<GetClothTypeDTO>>), 200)]
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
        [Route(nameof(GetClothType))]
        [ProducesResponseType(typeof(ElBaytResponse<ClothTypeDTO>), 200)]
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
        [HttpDelete]
        [Route(nameof(DeleteClothType))]
        [ProducesResponseType(typeof(ElBaytResponse<bool>), 200)]
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


                #region Result
                if (!string.IsNullOrEmpty(URL))
                {
                    var fullpath = Path.Combine(_config["FilesInfo:Path"], URL);
                    if (Directory.Exists(fullpath))
                        Directory.Delete(fullpath);
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
        [Route(nameof(UpdateClothType))]
        [ProducesResponseType(typeof(ElBaytResponse<string>), 200)]
        public async Task<IActionResult> UpdateClothType()
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

        #region Categories

        [Authorize]
        [HttpPost]
        [Route(nameof(AddNewClothCategory))]
        [ProducesResponseType(typeof(ElBaytResponse<string>), 200)]
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
        [Route(nameof(GetClothCategories))]
        [ProducesResponseType(typeof(ElBaytResponse<List<GetClothCategoryDTO>>), 200)]
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
        [Route(nameof(GetClothCategory))]
        [ProducesResponseType(typeof(ElBaytResponse<ClothCategoryDTO>), 200)]
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
        [HttpDelete]
        [Route(nameof(DeleteClothCategory))]
        [ProducesResponseType(typeof(ElBaytResponse<bool>), 200)]
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
                    var fullpath = Path.Combine(_config["FilesInfo:Path"], URL);
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
        [Route(nameof(UpdateClothCategory))]
        [ProducesResponseType(typeof(ElBaytResponse<string>), 200)]
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

        #region Departments

        [Authorize]
        [HttpPost]
        [Route(nameof(AddNewClothDepartment))]
        [ProducesResponseType(typeof(ElBaytResponse<string>), 200)]
        public async Task<IActionResult> AddNewClothDepartment()
        {

            var Response = new ElBaytResponse<string>
            {
                Errors = new List<string>()
            };
            try
            {
                var clothDepartment = await _departmentsServices.ClothesService.AddNewClothDepartment(Request.Form, _config["FilesInfo:WebFolder"]);

                if (clothDepartment != null)
                {
                    var path = Path.Combine(_config["FilesInfo:Directory"], clothDepartment.DepartmentPic);
                    var files = path.Split("\\");
                    var PicDirectory = path.Remove(path.IndexOf(files[^1]));

                    if (!Directory.Exists(PicDirectory))
                        Directory.CreateDirectory(PicDirectory);

                    using var stream = new FileStream(path, FileMode.Create);
                    Request.Form.Files[0].CopyTo(stream);

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
        [Route(nameof(GetClothDepartments))]
        [ProducesResponseType(typeof(ElBaytResponse<List<GetClothDepartmentDTO>>), 200)]
        public async Task<IActionResult> GetClothDepartments()
        {
            var Response = new ElBaytResponse<List<GetClothDepartmentDTO>>
            {
                Errors = new List<string>()
            };
            try
            {
                var Departments = await _departmentsServices.ClothesService.GetClothDepartments();
                #region Result
                Response.Result = EnumResponseResult.Successed;
                Response.Data = Departments;
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
        [Route(nameof(GetClothDepartment))]
        [ProducesResponseType(typeof(ElBaytResponse<ClothDepartmentDTO>), 200)]
        public async Task<IActionResult> GetClothDepartment(int Id)
        {
            var Response = new ElBaytResponse<ClothDepartmentDTO>
            {
                Errors = new List<string>()
            };
            try
            {
                var Department = await _departmentsServices.ClothesService.GetClothDepartment(Id);
                #region Result
                Response.Result = EnumResponseResult.Successed;
                Response.Data = Department;
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
        [HttpDelete]
        [Route(nameof(DeleteClothDepartment))]
        [ProducesResponseType(typeof(ElBaytResponse<bool>), 200)]
        public async Task<IActionResult> DeleteClothDepartment(int Id)
        {
            var Response = new ElBaytResponse<bool>
            {
                Errors = new List<string>()
            };
            try
            {
                var URL = await _departmentsServices.ClothesService.DeleteClothDepartment(Id);


                #region Result
                if (!string.IsNullOrEmpty(URL))
                {
                    var files = URL.Split("\\");
                    var fullpath = Path.Combine(_config["FilesInfo:Directory"], URL.Remove(URL.IndexOf(files[^1])) );
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
        [Route(nameof(UpdateClothDepartment))]
        [ProducesResponseType(typeof(ElBaytResponse<string>), 200)]
        public async Task<IActionResult> UpdateClothDepartment()
        {
            var Response = new ElBaytResponse<string>
            {
                Errors = new List<string>()
            };
           
            try
            {
                var res = await _departmentsServices.ClothesService.UpdateClothDepartment(Request.Form, _config["FilesInfo:WebFolder"], _config["FilesInfo:Directory"]);

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

        #region Brands

        [Authorize]
        [HttpPost]
        [Route(nameof(AddNewClothBrand))]
        [ProducesResponseType(typeof(ElBaytResponse<string>), 200)]
        public async Task<IActionResult> AddNewClothBrand()
        {

            var Response = new ElBaytResponse<string>
            {
                Errors = new List<string>()
            };
           
            try
            {
                var clothBrand = await _departmentsServices.ClothesService.AddNewClothBrand(Request.Form, _config["FilesInfo:WebFolder"]);

                if (clothBrand != null)
                {
                    var path = Path.Combine(_config["FilesInfo:Directory"], clothBrand.BrandPic);
                    var files = path.Split("\\");
                    var PicDirectory = path.Remove(path.IndexOf(files[^1]));

                    if (!Directory.Exists(PicDirectory))
                        Directory.CreateDirectory(PicDirectory);

                    using var stream = new FileStream(path, FileMode.Create);
                    Request.Form.Files[0].CopyTo(stream);

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
        [Route(nameof(GetClothBrands))]
        [ProducesResponseType(typeof(ElBaytResponse<List<GetClothBrandDTO>>), 200)]
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
        [Route(nameof(GetClothBrand))]
        [ProducesResponseType(typeof(ElBaytResponse<ClothBrandDTO>), 200)]
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
        [HttpDelete]
        [Route(nameof(DeleteClothBrand))]
        [ProducesResponseType(typeof(ElBaytResponse<bool>), 200)]
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
        [Route(nameof(UpdateClothBrand))]
        [ProducesResponseType(typeof(ElBaytResponse<string>), 200)]
        public async Task<IActionResult> UpdateClothBrand()
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

        #region Sizes

        [Authorize]
        [HttpPost]
        [Route(nameof(AddNewClothSize))]
        [ProducesResponseType(typeof(ElBaytResponse<string>), 200)]
        public async Task<IActionResult> AddNewClothSize(ClothSizeDTO ClothSize)
        {

            var Response = new ElBaytResponse<string>();
            Response.Errors = new List<string>();

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
        [Route(nameof(GetSizes))]
        [ProducesResponseType(typeof(ElBaytResponse<List<GetClothSizeDTO>>), 200)]
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
        [Route(nameof(GetClothSizes))]
        [ProducesResponseType(typeof(ElBaytResponse<List<GetClothSizeDTO>>), 200)]
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
        [Route(nameof(GetClothSize))]
        [ProducesResponseType(typeof(ElBaytResponse<ClothSizeDTO>), 200)]
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
        [HttpDelete]
        [Route(nameof(DeleteClothSize))]
        [ProducesResponseType(typeof(ElBaytResponse<bool>), 200)]
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
        [Route(nameof(UpdateClothSize))]
        [ProducesResponseType(typeof(ElBaytResponse<string>), 200)]
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

        #region Clothes

        [Authorize]
        [HttpPost]
        [Route(nameof(AddNewCloth))]
        [ProducesResponseType(typeof(ElBaytResponse<string>), 200)]
        public async Task<IActionResult> AddNewCloth()
        {

            var Response = new ElBaytResponse<string>
            {
                Errors = new List<string>()
            };

            try
            {

                if (Request.Form.Files[0].Length > 0)
                {
                    var product = await _departmentsServices.ClothesService.AddNewCloth(Request.Form, _config["FilesInfo:WebFolder"]);

                    if (product != null)
                    {
                        var path = Path.Combine(_config["FilesInfo:Directory"], product.ProductImageURL1);
                        var files = path.Split("\\");
                        var PicDirectory = path.Remove(path.IndexOf(files[^1]));

                        if (!Directory.Exists(PicDirectory))
                            Directory.CreateDirectory(PicDirectory);

                        using var stream = new FileStream(path, FileMode.Create);
                        Request.Form.Files[0].CopyTo(stream);

                        if (product.ProductImageURL2 != null)
                        {
                            path = Path.Combine(_config["FilesInfo:Directory"], product.ProductImageURL2);
                            files = path.Split("\\");
                            PicDirectory = path.Remove(path.IndexOf(files[^1]));

                            using var stream2 = new FileStream(path, FileMode.Create);
                            Request.Form.Files[1].CopyTo(stream2);
                        }

                        #region Result
                        Response.Result = EnumResponseResult.Successed;
                        Response.Data = CommonMessages.SUCCESSFULLY_ADDING;
                        #endregion

                        return Ok(Response);
                    }
                    Response.Result = EnumResponseResult.Failed;
                    Response.Data = CommonMessages.NAME_EXISTS;
                    return Ok(Response);
                }

                #region Result

                Response.Errors.Add("File Size Is Zero");
                Response.Result = EnumResponseResult.Failed;
                Response.Data = null;

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
        [Route(nameof(GetClothes))]
        [ProducesResponseType(typeof(ElBaytResponse<List<GetClothDTO>>), 200)]
        public async Task<IActionResult> GetClothes()
        {
            var Response = new ElBaytResponse<List<GetClothDTO>>
            {
                Errors = new List<string>()
            };
           
            try
            {

     
                var Clothes = await _departmentsServices.ClothesService.GetClothes();
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
        [Route(nameof(GetCloth))]
        [ProducesResponseType(typeof(ElBaytResponse<NumberClothDTO>), 200)]
        public async Task<IActionResult> GetCloth(int Id)
        {
            var Response = new ElBaytResponse<NumberClothDTO>
            {
                Errors = new List<string>()
            };
           
            try
            {  
                var Product = await _departmentsServices.ClothesService.GetCloth(Id);
                #region Result
                Response.Result = EnumResponseResult.Successed;
                Response.Data = Product;
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
        [HttpDelete]
        [Route(nameof(DeleteCloth))]
        [ProducesResponseType(typeof(ElBaytResponse<bool>), 200)]
        public async Task<IActionResult> DeleteCloth(int Id)
        {
            var Response = new ElBaytResponse<bool>
            {
                Errors = new List<string>()
            };
           
            try
            { 
                var URL = await _departmentsServices.ClothesService.DeleteCloth(Id);

                #region Result
                if (!string.IsNullOrEmpty(URL))
                {
                    var fullpath = Path.Combine(_config["FilesInfo:Path"], URL);
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
        [Route(nameof(UpdateCloth))]
        [ProducesResponseType(typeof(ElBaytResponse<string>), 200)]
        public async Task<IActionResult> UpdateCloth()
        {
            var Response = new ElBaytResponse<string>
            {
                Errors = new List<string>()
            };
           
            try
            {
                var imageUrls = await _departmentsServices.ClothesService.UpdateCloth(Request.Form, _config["FilesInfo:WebFolder"], _config["FilesInfo:Directory"]);

                #region Result
                if (imageUrls != null)
                {
                    if (!string.IsNullOrEmpty(Request.Form["Img1"]))
                    {
                        var oldpath = Path.Combine(_config["FilesInfo:Directory"], imageUrls[0]);
                        var newpath = Path.Combine(_config["FilesInfo:Directory"], imageUrls[1]);

                        if (System.IO.File.Exists(oldpath))
                            System.IO.File.Delete(newpath);

                        using var stream = new FileStream(newpath, FileMode.Create);
                        Request.Form.Files[0].CopyTo(stream);
                    }

                    if (!string.IsNullOrEmpty(Request.Form["Img2"]))
                    {
                        var fileindex = Request.Form.Files.Count == 1 ? 0 : 1;

                        string oldpath =!string.IsNullOrEmpty(imageUrls[2]) ? Path.Combine(_config["FilesInfo:Directory"], imageUrls[2]):null;

                        if (!string.IsNullOrEmpty(imageUrls[3]))
                        {
                            var newpath = Path.Combine(_config["FilesInfo:Directory"], imageUrls[3]);

                            if (System.IO.File.Exists(oldpath))
                                System.IO.File.Delete(oldpath);

                            using var stream = new FileStream(newpath, FileMode.Create);
                            Request.Form.Files[fileindex].CopyTo(stream);
                        }
                    }

                    Response.Result = EnumResponseResult.Successed;
                    Response.Data = CommonMessages.SUCCESSFULLY_UPDATING;
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

        [Authorize]
        [HttpPost]
        [Route(nameof(UploadClothImage))]
        [ProducesResponseType(typeof(ElBaytResponse<ClothImageDTO>), 200)]
        public async Task<IActionResult> UploadClothImage()
        {

            var Response = new ElBaytResponse<ClothImageDTO>
            {
                Errors = new List<string>()
            };
           
            try
            {     
                var file = Request.Form.Files[0];

                if (file.Length > 0)
                {

                    var DisImage = await _departmentsServices.ClothesService.SaveClothImage(Request.Form["ClothId"].ToString(), file, _config["FilesInfo:WebFolder"]);
                    var path = Path.Combine(_config["FilesInfo:Directory"], DisImage.URL);
                    var files = path.Split("\\");
                    var PicDirectory = path.Remove(path.IndexOf(files[^1]));

                    if (!Directory.Exists(PicDirectory))
                        Directory.CreateDirectory(PicDirectory);

                    using var stream = new FileStream(path, FileMode.Create);
                    file.CopyTo(stream);

                    Response.Result = EnumResponseResult.Successed;
                    Response.Data = DisImage;
                    return Ok(Response);
                }

                #region Result

                Response.Errors.Add("File Size Is Zero");
                Response.Result = EnumResponseResult.Failed;
                Response.Data = null;

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
        [Route(nameof(GetClothImages))]
        [ProducesResponseType(typeof(ElBaytResponse<List<ClothImageDTO>>), 200)]
        public async Task<IActionResult> GetClothImages(int clothId)
        {
            var Response = new ElBaytResponse<List<ClothImageDTO>>
            {
                Errors = new List<string>()
            };
           
            try
            {
                var Images = await _departmentsServices.ClothesService.GetClothImages(clothId);
                #region Result
                Response.Result = EnumResponseResult.Successed;
                Response.Data = Images;
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
        [HttpDelete]
        [Route(nameof(DeleteClothImage))]
        [ProducesResponseType(typeof(ElBaytResponse<bool>), 200)]
        public async Task<IActionResult> DeleteClothImage(int ImageId)
        {

            var Response = new ElBaytResponse<bool>
            {
                Errors = new List<string>()
            };
           
            try
            {
                var Image = await _departmentsServices.ClothesService.GetClothImage(ImageId);
                if (Image != null)
                {
                    var Res = await _departmentsServices.ClothesService.DeleteClothImage(ImageId);
                    if (Res == "true")
                    {
                        var fullpath = Path.Combine(_config["FilesInfo:Directory"], Image.URL);
                        if (System.IO.File.Exists(fullpath))
                            System.IO.File.Delete(fullpath);

                        Response.Result = EnumResponseResult.Successed;
                        Response.Data = true;
                        return Ok(Response);

                    }

                    Response.Errors.Add(Res);
                    Response.Result = EnumResponseResult.Failed;
                    Response.Data = false;
                    return Ok(Response);
                }
                Response.Errors.Add("The Image Does not Exists !!");
                Response.Result = EnumResponseResult.Failed;
                Response.Data = false;
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
        [HttpDelete]
        [Route(nameof(DeleteClothImageByURL))]
        [ProducesResponseType(typeof(ElBaytResponse<bool>), 200)]
        public async Task<IActionResult> DeleteClothImageByURL(string URL)
        {

            var Response = new ElBaytResponse<bool>
            {
                Errors = new List<string>()
            };        
            try
            {
                var res = await _departmentsServices.ClothesService.DeleteClothImageByURL(URL);
                if (res == "true")
                {
                    var fullpath = Path.Combine(_config["FilesInfo:Directory"], URL);
                    if (System.IO.File.Exists(fullpath))
                        System.IO.File.Delete(fullpath);

                    Response.Result = EnumResponseResult.Successed;
                    Response.Data = true;
                    return Ok(Response);
                }
                Response.Errors.Add("The Image Does not Exists !!");
                Response.Result = EnumResponseResult.Failed;
                Response.Data = false;
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
        [HttpPost]
        [Route(nameof(AddClothBrands))]
        [ProducesResponseType(typeof(ElBaytResponse<bool>), 200)]
        public async Task<IActionResult> AddClothBrands(SelectedBrandsDTO SelectedBrands)
        {

            var Response = new ElBaytResponse<bool>
            {
                Errors = new List<string>()
            };           
            try
            {

                var res = await _departmentsServices.ClothesService.AddClothBrands(SelectedBrands);
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
        [HttpGet]
        [Route(nameof(GetSelectedClothBrands))]
        [ProducesResponseType(typeof(ElBaytResponse<List<ClothBrandsDTO>>), 200)]
        public async Task<IActionResult> GetSelectedClothBrands(int clothId)
        {
            var Response = new ElBaytResponse<List<ClothBrandsDTO>>
            {
                Errors = new List<string>()
            };
           
            try
            {
                var brands = await _departmentsServices.ClothesService.GetClothBrands(clothId);
                #region Result
                Response.Result = EnumResponseResult.Successed;
                Response.Data = brands;
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
        [Route(nameof(GetClothDBLInfo))]
        [ProducesResponseType(typeof(ElBaytResponse<ClothDBLDataDTO>), 200)]
        public async Task<IActionResult> GetClothDBLInfo(int clothId)
        {
            var Response = new ElBaytResponse<ClothDBLDataDTO>
            {
                Errors = new List<string>()
            };
           
            try
            {
                var Info = await _departmentsServices.ClothesService.GetClothDBLInfo(clothId);
                #region Result
                Response.Result = EnumResponseResult.Successed;
                Response.Data = Info;
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
        [HttpPost]
        [Route(nameof(AddClothInfo))]
        [ProducesResponseType(typeof(ElBaytResponse<string>), 200)]
        public async Task<IActionResult> AddClothInfo(ClothInfoDTO ClothInfo)
        {

            var Response = new ElBaytResponse<string>
            {
                Errors = new List<string>()
            };          
            try
            {

                var res = await _departmentsServices.ClothesService.AddClothInfo(ClothInfo);
                if (res == CommonMessages.SUCCESSFULLY_ADDING|| res == CommonMessages.SUCCESSFULLY_UPDATING)
                {
                    Response.Result = EnumResponseResult.Successed;
                    Response.Data = CommonMessages.SUCCESSFULLY_ADDING;
                    return Ok(Response);
                }
               
                Response.Result = EnumResponseResult.Failed;
                Response.Data = CommonMessages.FAILED_ADDING;
                return Ok(Response);
            }
            catch (NotFoundException ex)
            {
                #region Result
                Response.Result = EnumResponseResult.Failed;
                Response.Data = ex.Message;
                Response.Errors.Add(ex.ErrorMessage);
                #endregion

                return NotFound(Response);
            }
            catch (Exception ex)
            {
                #region Result
                Response.Result = EnumResponseResult.Failed;
                Response.Data = ex.Message;

                Response.Errors.Add(ex.Message);
                #endregion

                return BadRequest(Response);
            }
        }

        [Authorize]
        [HttpGet]
        [Route(nameof(GetClothInfo))]
        [ProducesResponseType(typeof(ElBaytResponse<List<ClothInfoDataDTO>>), 200)]
        public async Task<IActionResult> GetClothInfo(int clothId)
        {
            var Response = new ElBaytResponse<List<ClothInfoDataDTO>>
            {
                Errors = new List<string>()
            };
           
            try
            {
               var Info = await _departmentsServices.ClothesService.GetClothInfo(clothId);
                #region Result
                Response.Result = EnumResponseResult.Successed;
                Response.Data = Info;
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
        [HttpDelete]
        [Route(nameof(DeleteClothInfo))]
        [ProducesResponseType(typeof(ElBaytResponse<bool>), 200)]
        public async Task<IActionResult> DeleteClothInfo(int Id)
        {
            var Response = new ElBaytResponse<bool>
            {
                Errors = new List<string>()
            };   
            try
            {
                var Res = await _departmentsServices.ClothesService.DeleteClothInfo(Id);

                #region Result
                if (Res == CommonMessages.SUCCESSFULLY_DELETING)
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
        [HttpGet]
        [Route(nameof(GetInfo))]
        [ProducesResponseType(typeof(ElBaytResponse<ClothInfoDTO>), 200)]
        public async Task<IActionResult> GetInfo(int Id)
        {
            var Response = new ElBaytResponse<ClothInfoDTO>
            {
                Errors = new List<string>()
            };
           
            try
            {    
                var Info = await _departmentsServices.ClothesService.GetInfo(Id);
                #region Result
                Response.Result = EnumResponseResult.Successed;
                Response.Data = Info;
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
        #endregion
    }
}