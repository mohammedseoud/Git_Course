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

namespace ElBayt_ECommerce.WebAPI.Controllers
{
    [EnableCors(CorsOrigin.LOCAL_ORIGIN)]
    [ApiController]
    [Route("api/v1.0/ElBayt/Clothes")]
    public class ClothesController : Controller
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
        public async Task<IActionResult> AddNewClothType()
        {

            var Response = new ElBaytResponse<string>
            {
                Errors = new List<string>()
            };
            var correlationGuid = Guid.NewGuid();
            try
            {

                #region Logging info
                _logger.InfoInDetail(Request.Form, correlationGuid, nameof(ClothesController), nameof(AddNewClothType), 1, User.Identity.Name);
                #endregion Logging info

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
                #region Logging info

                _logger.ErrorInDetail($"newException {Request.Form}", correlationGuid, $"{nameof(ClothesController)}_{nameof(AddNewClothType)}_{nameof(NotFoundException)}", ex, 1, User.Identity.Name);

                #endregion Logging info


                #region Result
                Response.Result = EnumResponseResult.Failed;
                Response.Data = CommonMessages.FAILED_ADDING;
                Response.Errors.Add(ex.Message);
                #endregion

                return NotFound(Response);
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail($"newException {Request.Form}", correlationGuid,
                    $"{nameof(ClothesController)}_{nameof(AddNewClothType)}_{nameof(Exception)}", ex, 1, User.Identity.Name);

                #endregion Logging info

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
        public ActionResult GetClothTypes()
        {
            var Response = new ElBaytResponse<object>
            {
                Errors = new List<string>()
            };
            var correlationGuid = Guid.NewGuid();
            try
            {

                #region Logging info
                _logger.InfoInDetail("GetAll", correlationGuid, nameof(ClothesController), nameof(GetClothTypes), 1, User.Identity.Name);
                #endregion Logging info

                var Types = _departmentsServices.ClothesService.GetClothTypes();
                #region Result
                Response.Result = EnumResponseResult.Successed;
                Response.Data = Types;
                #endregion

                return Ok(Response);
            }
            catch (NotFoundException ex)
            {
                #region Logging info

                _logger.ErrorInDetail($"newException GetClothTypes", correlationGuid,
                    $"{nameof(ClothesController)}_{nameof(GetClothTypes)}_{nameof(NotFoundException)}",
                    ex, 1, User.Identity.Name);

                #endregion Logging info
                #region Result
                Response.Result = EnumResponseResult.Failed;
                Response.Data = null;
                Response.Errors.Add(ex.Message);
                #endregion

                return NotFound(Response);
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail($"newException GetClothTypes", correlationGuid,
                    $"{nameof(ClothesController)}_{nameof(GetClothTypes)}_{nameof(Exception)}", ex, 1, User.Identity.Name);

                #endregion Logging info
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
        public async Task<ActionResult> GetClothType(Guid Id)
        {
            var Response = new ElBaytResponse<ClothTypeDTO>
            {
                Errors = new List<string>()
            };
            var correlationGuid = Guid.NewGuid();
            try
            {

                #region Logging info
                _logger.InfoInDetail(Id, correlationGuid, nameof(ClothesController), nameof(GetClothType), 1, User.Identity.Name);
                #endregion Logging info

                var Type = await _departmentsServices.ClothesService.GetClothType(Id);
                #region Result
                Response.Result = EnumResponseResult.Successed;
                Response.Data = Type;
                #endregion

                return Ok(Response);
            }
            catch (NotFoundException ex)
            {
                #region Logging info

                _logger.ErrorInDetail($"newException {Id}", correlationGuid,
                    $"{nameof(ClothesController)}_{nameof(GetClothType)}_{nameof(NotFoundException)}",
                    ex, 1, User.Identity.Name);

                #endregion Logging info
                #region Result
                Response.Result = EnumResponseResult.Failed;
                Response.Data = null;
                Response.Errors.Add(ex.Message);
                #endregion

                return NotFound(Response);
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail($"newException {Id}", correlationGuid,
                    $"{nameof(ClothesController)}_{nameof(GetClothType)}_{nameof(Exception)}", ex, 1, User.Identity.Name);

                #endregion Logging info
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
        public async Task<ActionResult> DeleteClothType(Guid Id)
        {
            var Response = new ElBaytResponse<bool>
            {
                Errors = new List<string>()
            };
            var correlationGuid = Guid.NewGuid();
            try
            {

                #region Logging info
                _logger.InfoInDetail(Id, correlationGuid, nameof(ClothesController), nameof(DeleteClothType), 1, User.Identity.Name);
                #endregion Logging info

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
                #region Logging info

                _logger.ErrorInDetail($"newException {Id}", correlationGuid,
                    $"{nameof(ClothesController)}_{nameof(DeleteClothType)}_{nameof(NotFoundException)}",
                    ex, 1, User.Identity.Name);

                #endregion Logging info
                #region Result
                Response.Result = EnumResponseResult.Failed;
                Response.Data = false;
                Response.Errors.Add(ex.Message);
                #endregion

                return NotFound(Response);
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail($"newException {Id}", correlationGuid,
                    $"{nameof(ClothesController)}_{nameof(DeleteClothType)}_{nameof(Exception)}", ex, 1, User.Identity.Name);

                #endregion Logging info
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
        public async Task<ActionResult> UpdateClothType()
        {
            var Response = new ElBaytResponse<string>
            {
                Errors = new List<string>()
            };
            var correlationGuid = Guid.NewGuid();
            try
            {

                #region Logging info
                _logger.InfoInDetail(Request.Form, correlationGuid, nameof(ClothesController), nameof(UpdateClothType), 1, User.Identity.Name);
                #endregion Logging info

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
                #region Logging info

                _logger.ErrorInDetail($"newException {Request.Form}", correlationGuid,
                    $"{nameof(ClothesController)}_{nameof(UpdateClothType)}_{nameof(NotFoundException)}",
                    ex, 1, User.Identity.Name);

                #endregion Logging info
                #region Result
                Response.Result = EnumResponseResult.Failed;
                Response.Data = CommonMessages.FAILED_UPDATING;
                Response.Errors.Add(ex.Message);
                #endregion

                return NotFound(Response);
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail($"newException {Request.Form}", correlationGuid,
                    $"{nameof(ClothesController)}_{nameof(UpdateClothType)}_{nameof(Exception)}", ex, 1, User.Identity.Name);

                #endregion Logging info
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
        public async Task<IActionResult> AddNewClothCategory(ClothCategoryDTO ClothCategory)
        {

            var Response = new ElBaytResponse<string>();
            Response.Errors = new List<string>();
            var correlationGuid = Guid.NewGuid();
            try
            {

                #region Logging info
                _logger.InfoInDetail(ClothCategory, correlationGuid, nameof(ClothesController), nameof(AddNewClothCategory), 1, User.Identity.Name);
                #endregion Logging info

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
                #region Logging info

                _logger.ErrorInDetail($"newException {ClothCategory}", correlationGuid, $"{nameof(ClothesController)}_{nameof(AddNewClothCategory)}_{nameof(NotFoundException)}", ex, 1, User.Identity.Name);

                #endregion Logging info


                #region Result
                Response.Result = EnumResponseResult.Failed;
                Response.Data = CommonMessages.FAILED_ADDING;
                Response.Errors.Add(ex.Message);
                #endregion

                return NotFound(Response);
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail($"newException {ClothCategory}", correlationGuid,
                    $"{nameof(ClothesController)}_{nameof(AddNewClothCategory)}_{nameof(Exception)}", ex, 1, User.Identity.Name);

                #endregion Logging info

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
        public ActionResult GetClothCategories()
        {
            var Response = new ElBaytResponse<object>
            {
                Errors = new List<string>()
            };
            var correlationGuid = Guid.NewGuid();
            try
            {

                #region Logging info
                _logger.InfoInDetail("GetClothCategories", correlationGuid, nameof(ClothesController), nameof(GetClothCategories), 1, User.Identity.Name);
                #endregion Logging info

                var Clothes = _departmentsServices.ClothesService.GetClothCategories();
                #region Result
                Response.Result = EnumResponseResult.Successed;
                Response.Data = Clothes;
                #endregion

                return Ok(Response);
            }
            catch (NotFoundException ex)
            {
                #region Logging info

                _logger.ErrorInDetail($"newException GetClothCategories", correlationGuid,
                    $"{nameof(ClothesController)}_{nameof(GetClothCategories)}_{nameof(NotFoundException)}",
                    ex, 1, User.Identity.Name);

                #endregion Logging info
                #region Result
                Response.Result = EnumResponseResult.Failed;
                Response.Data = null;
                Response.Errors.Add(ex.Message);
                #endregion

                return NotFound(Response);
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail($"newException GetClothCategories", correlationGuid,
                    $"{nameof(ClothesController)}_{nameof(GetClothCategories)}_{nameof(Exception)}", ex, 1, User.Identity.Name);

                #endregion Logging info
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
        public async Task<ActionResult> GetClothCategory(Guid Id)
        {
            var Response = new ElBaytResponse<ClothCategoryDTO>
            {
                Errors = new List<string>()
            };
            var correlationGuid = Guid.NewGuid();
            try
            {

                #region Logging info
                _logger.InfoInDetail(Id, correlationGuid, nameof(ClothesController), nameof(GetClothCategory), 1, User.Identity.Name);
                #endregion Logging info

                var Category = await _departmentsServices.ClothesService.GetClothCategory(Id);
                #region Result
                Response.Result = EnumResponseResult.Successed;
                Response.Data = Category;
                #endregion

                return Ok(Response);
            }
            catch (NotFoundException ex)
            {
                #region Logging info

                _logger.ErrorInDetail($"newException {Id}", correlationGuid,
                    $"{nameof(ClothesController)}_{nameof(GetClothCategory)}_{nameof(NotFoundException)}",
                    ex, 1, User.Identity.Name);

                #endregion Logging info
                #region Result
                Response.Result = EnumResponseResult.Failed;
                Response.Data = null;
                Response.Errors.Add(ex.Message);
                #endregion

                return NotFound(Response);
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail($"newException {Id}", correlationGuid,
                    $"{nameof(ClothesController)}_{nameof(GetClothCategory)}_{nameof(Exception)}", ex, 1, User.Identity.Name);

                #endregion Logging info
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
        public async Task<ActionResult> DeleteClothCategory(Guid Id)
        {
            var Response = new ElBaytResponse<bool>
            {
                Errors = new List<string>()
            };
            var correlationGuid = Guid.NewGuid();
            try
            {

                #region Logging info
                _logger.InfoInDetail(Id, correlationGuid, nameof(ClothesController), nameof(DeleteClothCategory), 1, User.Identity.Name);
                #endregion Logging info

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
                #region Logging info

                _logger.ErrorInDetail($"newException {Id}", correlationGuid,
                    $"{nameof(ClothesController)}_{nameof(DeleteClothCategory)}_{nameof(NotFoundException)}",
                    ex, 1, User.Identity.Name);

                #endregion Logging info
                #region Result
                Response.Result = EnumResponseResult.Failed;
                Response.Data = false;
                Response.Errors.Add(ex.Message);
                #endregion

                return NotFound(Response);
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail($"newException {Id}", correlationGuid,
                    $"{nameof(ClothesController)}_{nameof(DeleteClothCategory)}_{nameof(Exception)}", ex, 1, User.Identity.Name);

                #endregion Logging info
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
        public async Task<ActionResult> UpdateClothCategory(ClothCategoryDTO clothCategory)
        {
            var Response = new ElBaytResponse<string>
            {
                Errors = new List<string>()
            };
            var correlationGuid = Guid.NewGuid();
            try
            {

                #region Logging info
                _logger.InfoInDetail(clothCategory, correlationGuid, nameof(ClothesController), nameof(UpdateClothCategory), 1, User.Identity.Name);
                #endregion Logging info

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
                #region Logging info

                _logger.ErrorInDetail($"newException {clothCategory}", correlationGuid,
                    $"{nameof(ClothesController)}_{nameof(UpdateClothCategory)}_{nameof(NotFoundException)}",
                    ex, 1, User.Identity.Name);

                #endregion Logging info
                #region Result
                Response.Result = EnumResponseResult.Failed;
                Response.Data = CommonMessages.FAILED_UPDATING;
                Response.Errors.Add(ex.Message);
                #endregion

                return NotFound(Response);
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail($"newException {clothCategory}", correlationGuid,
                    $"{nameof(ClothesController)}_{nameof(UpdateClothCategory)}_{nameof(Exception)}", ex, 1, User.Identity.Name);

                #endregion Logging info
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
        public async Task<IActionResult> AddNewClothDepartment()
        {

            var Response = new ElBaytResponse<string>
            {
                Errors = new List<string>()
            };
            var correlationGuid = Guid.NewGuid();
            try
            {

                #region Logging info
                _logger.InfoInDetail(Request.Form, correlationGuid, nameof(ClothesController), nameof(AddNewClothDepartment), 1, User.Identity.Name);
                #endregion Logging info

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
                #region Logging info

                _logger.ErrorInDetail($"newException {Request.Form}", correlationGuid, $"{nameof(ClothesController)}_{nameof(AddNewClothDepartment)}_{nameof(NotFoundException)}", ex, 1, User.Identity.Name);

                #endregion Logging info


                #region Result
                Response.Result = EnumResponseResult.Failed;
                Response.Data = CommonMessages.FAILED_ADDING;
                Response.Errors.Add(ex.Message);
                #endregion

                return NotFound(Response);
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail($"newException {Request.Form}", correlationGuid,
                    $"{nameof(ClothesController)}_{nameof(AddNewClothDepartment)}_{nameof(Exception)}", ex, 1, User.Identity.Name);

                #endregion Logging info

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
        public ActionResult GetClothDepartments()
        {
            var Response = new ElBaytResponse<object>
            {
                Errors = new List<string>()
            };
            var correlationGuid = Guid.NewGuid();
            try
            {

                #region Logging info
                _logger.InfoInDetail("GetAll", correlationGuid, nameof(ClothesController), nameof(GetClothDepartments), 1, User.Identity.Name);
                #endregion Logging info

                var Departments = _departmentsServices.ClothesService.GetClothDepartments();
                #region Result
                Response.Result = EnumResponseResult.Successed;
                Response.Data = Departments;
                #endregion

                return Ok(Response);
            }
            catch (NotFoundException ex)
            {
                #region Logging info

                _logger.ErrorInDetail($"newException GetClothDepartments", correlationGuid,
                    $"{nameof(ClothesController)}_{nameof(GetClothDepartments)}_{nameof(NotFoundException)}",
                    ex, 1, User.Identity.Name);

                #endregion Logging info
                #region Result
                Response.Result = EnumResponseResult.Failed;
                Response.Data = null;
                Response.Errors.Add(ex.Message);
                #endregion

                return NotFound(Response);
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail($"newException GetClothDepartments", correlationGuid,
                    $"{nameof(ClothesController)}_{nameof(GetClothDepartments)}_{nameof(Exception)}", ex, 1, User.Identity.Name);

                #endregion Logging info
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
        public async Task<ActionResult> GetClothDepartment(Guid Id)
        {
            var Response = new ElBaytResponse<ClothDepartmentDTO>
            {
                Errors = new List<string>()
            };
            var correlationGuid = Guid.NewGuid();
            try
            {

                #region Logging info
                _logger.InfoInDetail(Id, correlationGuid, nameof(ClothesController), nameof(GetClothDepartment), 1, User.Identity.Name);
                #endregion Logging info

                var Department = await _departmentsServices.ClothesService.GetClothDepartment(Id);
                #region Result
                Response.Result = EnumResponseResult.Successed;
                Response.Data = Department;
                #endregion

                return Ok(Response);
            }
            catch (NotFoundException ex)
            {
                #region Logging info

                _logger.ErrorInDetail($"newException {Id}", correlationGuid,
                    $"{nameof(ClothesController)}_{nameof(GetClothDepartment)}_{nameof(NotFoundException)}",
                    ex, 1, User.Identity.Name);

                #endregion Logging info
                #region Result
                Response.Result = EnumResponseResult.Failed;
                Response.Data = null;
                Response.Errors.Add(ex.Message);
                #endregion

                return NotFound(Response);
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail($"newException {Id}", correlationGuid,
                    $"{nameof(ClothesController)}_{nameof(GetClothDepartment)}_{nameof(Exception)}", ex, 1, User.Identity.Name);

                #endregion Logging info
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
        public async Task<ActionResult> DeleteClothDepartment(Guid Id)
        {
            var Response = new ElBaytResponse<bool>
            {
                Errors = new List<string>()
            };
            var correlationGuid = Guid.NewGuid();
            try
            {

                #region Logging info
                _logger.InfoInDetail(Id, correlationGuid, nameof(ClothesController), nameof(DeleteClothDepartment), 1, User.Identity.Name);
                #endregion Logging info

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
                #region Logging info

                _logger.ErrorInDetail($"newException {Id}", correlationGuid,
                    $"{nameof(ClothesController)}_{nameof(DeleteClothDepartment)}_{nameof(NotFoundException)}",
                    ex, 1, User.Identity.Name);

                #endregion Logging info
                #region Result
                Response.Result = EnumResponseResult.Failed;
                Response.Data = false;
                Response.Errors.Add(ex.Message);
                #endregion

                return NotFound(Response);
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail($"newException {Id}", correlationGuid,
                    $"{nameof(ClothesController)}_{nameof(DeleteClothDepartment)}_{nameof(Exception)}", ex, 1, User.Identity.Name);

                #endregion Logging info
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
        public async Task<ActionResult> UpdateClothDepartment()
        {
            var Response = new ElBaytResponse<string>
            {
                Errors = new List<string>()
            };
            var correlationGuid = Guid.NewGuid();
            try
            {

                #region Logging info
                _logger.InfoInDetail(Request.Form, correlationGuid, nameof(ClothesController), nameof(UpdateClothDepartment), 1, User.Identity.Name);
                #endregion Logging info

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
                #region Logging info

                _logger.ErrorInDetail($"newException {Request.Form}", correlationGuid,
                    $"{nameof(ClothesController)}_{nameof(UpdateClothDepartment)}_{nameof(NotFoundException)}",
                    ex, 1, User.Identity.Name);

                #endregion Logging info
                #region Result
                Response.Result = EnumResponseResult.Failed;
                Response.Data = CommonMessages.FAILED_UPDATING;
                Response.Errors.Add(ex.Message);
                #endregion

                return NotFound(Response);
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail($"newException {Request.Form}", correlationGuid,
                    $"{nameof(ClothesController)}_{nameof(UpdateClothDepartment)}_{nameof(Exception)}", ex, 1, User.Identity.Name);

                #endregion Logging info
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

      //  [Authorize]
        [HttpPost]
        [Route(nameof(AddNewClothBrand))]
        public async Task<IActionResult> AddNewClothBrand()
        {

            var Response = new ElBaytResponse<string>
            {
                Errors = new List<string>()
            };
            var correlationGuid = Guid.NewGuid();
            try
            {

                #region Logging info
                _logger.InfoInDetail(Request.Form, correlationGuid, nameof(ClothesController), nameof(AddNewClothBrand), 1, User.Identity.Name);
                #endregion Logging info

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
                #region Logging info

                _logger.ErrorInDetail($"newException {Request.Form}", correlationGuid, $"{nameof(ClothesController)}_{nameof(AddNewClothBrand)}_{nameof(NotFoundException)}", ex, 1, User.Identity.Name);

                #endregion Logging info


                #region Result
                Response.Result = EnumResponseResult.Failed;
                Response.Data = CommonMessages.FAILED_ADDING;
                Response.Errors.Add(ex.Message);
                #endregion

                return NotFound(Response);
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail($"newException {Request.Form}", correlationGuid,
                    $"{nameof(ClothesController)}_{nameof(AddNewClothBrand)}_{nameof(Exception)}", ex, 1, User.Identity.Name);

                #endregion Logging info

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
        public ActionResult GetClothBrands()
        {
            var Response = new ElBaytResponse<object>
            {
                Errors = new List<string>()
            };
            var correlationGuid = Guid.NewGuid();
            try
            {

                #region Logging info
                _logger.InfoInDetail("GetAll", correlationGuid, nameof(ClothesController), nameof(GetClothBrands), 1, User.Identity.Name);
                #endregion Logging info

                var Brands = _departmentsServices.ClothesService.GetClothBrands();
                #region Result
                Response.Result = EnumResponseResult.Successed;
                Response.Data = Brands;
                #endregion

                return Ok(Response);
            }
            catch (NotFoundException ex)
            {
                #region Logging info

                _logger.ErrorInDetail($"newException GetClothBrands", correlationGuid,
                    $"{nameof(ClothesController)}_{nameof(GetClothBrands)}_{nameof(NotFoundException)}",
                    ex, 1, User.Identity.Name);

                #endregion Logging info
                #region Result
                Response.Result = EnumResponseResult.Failed;
                Response.Data = null;
                Response.Errors.Add(ex.Message);
                #endregion

                return NotFound(Response);
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail($"newException GetClothBrands", correlationGuid,
                    $"{nameof(ClothesController)}_{nameof(GetClothBrands)}_{nameof(Exception)}", ex, 1, User.Identity.Name);

                #endregion Logging info
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
        public async Task<ActionResult> GetClothBrand(Guid Id)
        {
            var Response = new ElBaytResponse<ClothBrandDTO>
            {
                Errors = new List<string>()
            };
            var correlationGuid = Guid.NewGuid();
            try
            {

                #region Logging info
                _logger.InfoInDetail(Id, correlationGuid, nameof(ClothesController), nameof(GetClothBrand), 1, User.Identity.Name);
                #endregion Logging info

                var Brand = await _departmentsServices.ClothesService.GetClothBrand(Id);
                #region Result
                Response.Result = EnumResponseResult.Successed;
                Response.Data = Brand;
                #endregion

                return Ok(Response);
            }
            catch (NotFoundException ex)
            {
                #region Logging info

                _logger.ErrorInDetail($"newException {Id}", correlationGuid,
                    $"{nameof(ClothesController)}_{nameof(GetClothBrand)}_{nameof(NotFoundException)}",
                    ex, 1, User.Identity.Name);

                #endregion Logging info
                #region Result
                Response.Result = EnumResponseResult.Failed;
                Response.Data = null;
                Response.Errors.Add(ex.Message);
                #endregion

                return NotFound(Response);
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail($"newException {Id}", correlationGuid,
                    $"{nameof(ClothesController)}_{nameof(GetClothBrand)}_{nameof(Exception)}", ex, 1, User.Identity.Name);

                #endregion Logging info
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
        public async Task<ActionResult> DeleteClothBrand(Guid Id)
        {
            var Response = new ElBaytResponse<bool>
            {
                Errors = new List<string>()
            };
            var correlationGuid = Guid.NewGuid();
            try
            {

                #region Logging info
                _logger.InfoInDetail(Id, correlationGuid, nameof(ClothesController), nameof(DeleteClothBrand), 1, User.Identity.Name);
                #endregion Logging info

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
                #region Logging info

                _logger.ErrorInDetail($"newException {Id}", correlationGuid,
                    $"{nameof(ClothesController)}_{nameof(DeleteClothBrand)}_{nameof(NotFoundException)}",
                    ex, 1, User.Identity.Name);

                #endregion Logging info
                #region Result
                Response.Result = EnumResponseResult.Failed;
                Response.Data = false;
                Response.Errors.Add(ex.Message);
                #endregion

                return NotFound(Response);
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail($"newException {Id}", correlationGuid,
                    $"{nameof(ClothesController)}_{nameof(DeleteClothBrand)}_{nameof(Exception)}", ex, 1, User.Identity.Name);

                #endregion Logging info
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
        public async Task<ActionResult> UpdateClothBrand()
        {
            var Response = new ElBaytResponse<string>
            {
                Errors = new List<string>()
            };
            var correlationGuid = Guid.NewGuid();
            try
            {

                #region Logging info
                _logger.InfoInDetail(Request.Form, correlationGuid, nameof(ClothesController), nameof(UpdateClothBrand), 1, User.Identity.Name);
                #endregion Logging info

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
                #region Logging info

                _logger.ErrorInDetail($"newException {Request.Form}", correlationGuid,
                    $"{nameof(ClothesController)}_{nameof(UpdateClothBrand)}_{nameof(NotFoundException)}",
                    ex, 1, User.Identity.Name);

                #endregion Logging info
                #region Result
                Response.Result = EnumResponseResult.Failed;
                Response.Data = CommonMessages.FAILED_UPDATING;
                Response.Errors.Add(ex.Message);
                #endregion

                return NotFound(Response);
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail($"newException {Request.Form}", correlationGuid,
                    $"{nameof(ClothesController)}_{nameof(UpdateClothBrand)}_{nameof(Exception)}", ex, 1, User.Identity.Name);

                #endregion Logging info
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
        public async Task<IActionResult> AddNewClothSize(ClothSizeDTO ClothSize)
        {

            var Response = new ElBaytResponse<string>();
            Response.Errors = new List<string>();
            var correlationGuid = Guid.NewGuid();
            try
            {

                #region Logging info
                _logger.InfoInDetail(ClothSize, correlationGuid, nameof(ClothesController), nameof(AddNewClothSize), 1, User.Identity.Name);
                #endregion Logging info

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
                #region Logging info

                _logger.ErrorInDetail($"newException {ClothSize}", correlationGuid, $"{nameof(ClothesController)}_{nameof(AddNewClothSize)}_{nameof(NotFoundException)}", ex, 1, User.Identity.Name);

                #endregion Logging info


                #region Result
                Response.Result = EnumResponseResult.Failed;
                Response.Data = CommonMessages.FAILED_ADDING;
                Response.Errors.Add(ex.Message);
                #endregion

                return NotFound(Response);
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail($"newException {ClothSize}", correlationGuid,
                    $"{nameof(ClothesController)}_{nameof(AddNewClothSize)}_{nameof(Exception)}", ex, 1, User.Identity.Name);

                #endregion Logging info

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
        public ActionResult GetSizes()
        {
            var Response = new ElBaytResponse<object>
            {
                Errors = new List<string>()
            };
            var correlationGuid = Guid.NewGuid();
            try
            {

                #region Logging info
                _logger.InfoInDetail("GetSizes", correlationGuid, nameof(ClothesController), nameof(GetSizes), 1, User.Identity.Name);
                #endregion Logging info

                var Clothes = _departmentsServices.ClothesService.GetSizes();
                #region Result
                Response.Result = EnumResponseResult.Successed;
                Response.Data = Clothes;
                #endregion

                return Ok(Response);
            }
            catch (NotFoundException ex)
            {
                #region Logging info

                _logger.ErrorInDetail($"newException GetSizes", correlationGuid,
                    $"{nameof(ClothesController)}_{nameof(GetSizes)}_{nameof(NotFoundException)}",
                    ex, 1, User.Identity.Name);

                #endregion Logging info
                #region Result
                Response.Result = EnumResponseResult.Failed;
                Response.Data = null;
                Response.Errors.Add(ex.Message);
                #endregion

                return NotFound(Response);
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail($"newException GetSizes", correlationGuid,
                    $"{nameof(ClothesController)}_{nameof(GetSizes)}_{nameof(Exception)}", ex, 1, User.Identity.Name);

                #endregion Logging info
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
        public async Task<ActionResult> GetClothSizes(Guid ClothId)
        {
            var Response = new ElBaytResponse<object>
            {
                Errors = new List<string>()
            };
            var correlationGuid = Guid.NewGuid();
            try
            {

                #region Logging info
                _logger.InfoInDetail(ClothId, correlationGuid, nameof(ClothesController), nameof(GetClothSizes), 1, User.Identity.Name);
                #endregion Logging info

                var Clothes = await _departmentsServices.ClothesService.GetClothSizes(ClothId);
                #region Result
                Response.Result = EnumResponseResult.Successed;
                Response.Data = Clothes;
                #endregion

                return Ok(Response);
            }
            catch (NotFoundException ex)
            {
                #region Logging info

                _logger.ErrorInDetail($"newException {ClothId}", correlationGuid,
                    $"{nameof(ClothesController)}_{nameof(GetClothSizes)}_{nameof(NotFoundException)}",
                    ex, 1, User.Identity.Name);

                #endregion Logging info
                #region Result
                Response.Result = EnumResponseResult.Failed;
                Response.Data = null;
                Response.Errors.Add(ex.Message);
                #endregion

                return NotFound(Response);
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail($"newException {ClothId}", correlationGuid,
                    $"{nameof(ClothesController)}_{nameof(GetClothSizes)}_{nameof(Exception)}", ex, 1, User.Identity.Name);

                #endregion Logging info
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
        public async Task<ActionResult> GetClothSize(Guid Id)
        {
            var Response = new ElBaytResponse<ClothSizeDTO>
            {
                Errors = new List<string>()
            };
            var correlationGuid = Guid.NewGuid();
            try
            {

                #region Logging info
                _logger.InfoInDetail(Id, correlationGuid, nameof(ClothesController), nameof(GetClothSize), 1, User.Identity.Name);
                #endregion Logging info

                var Size = await _departmentsServices.ClothesService.GetClothSize(Id);
                #region Result
                Response.Result = EnumResponseResult.Successed;
                Response.Data = Size;
                #endregion

                return Ok(Response);
            }
            catch (NotFoundException ex)
            {
                #region Logging info

                _logger.ErrorInDetail($"newException {Id}", correlationGuid,
                    $"{nameof(ClothesController)}_{nameof(GetClothSize)}_{nameof(NotFoundException)}",
                    ex, 1, User.Identity.Name);

                #endregion Logging info
                #region Result
                Response.Result = EnumResponseResult.Failed;
                Response.Data = null;
                Response.Errors.Add(ex.Message);
                #endregion

                return NotFound(Response);
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail($"newException {Id}", correlationGuid,
                    $"{nameof(ClothesController)}_{nameof(GetClothSize)}_{nameof(Exception)}", ex, 1, User.Identity.Name);

                #endregion Logging info
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
        public async Task<ActionResult> DeleteClothSize(Guid Id)
        {
            var Response = new ElBaytResponse<bool>
            {
                Errors = new List<string>()
            };
            var correlationGuid = Guid.NewGuid();
            try
            {

                #region Logging info
                _logger.InfoInDetail(Id, correlationGuid, nameof(ClothesController), nameof(DeleteClothSize), 1, User.Identity.Name);
                #endregion Logging info

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
                #region Logging info

                _logger.ErrorInDetail($"newException {Id}", correlationGuid,
                    $"{nameof(ClothesController)}_{nameof(DeleteClothSize)}_{nameof(NotFoundException)}",
                    ex, 1, User.Identity.Name);

                #endregion Logging info
                #region Result
                Response.Result = EnumResponseResult.Failed;
                Response.Data = false;
                Response.Errors.Add(ex.Message);
                #endregion

                return NotFound(Response);
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail($"newException {Id}", correlationGuid,
                    $"{nameof(ClothesController)}_{nameof(DeleteClothSize)}_{nameof(Exception)}", ex, 1, User.Identity.Name);

                #endregion Logging info
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
        public async Task<ActionResult> UpdateClothSize(ClothSizeDTO clothSize)
        {
            var Response = new ElBaytResponse<string>
            {
                Errors = new List<string>()
            };
            var correlationGuid = Guid.NewGuid();
            try
            {

                #region Logging info
                _logger.InfoInDetail(clothSize, correlationGuid, nameof(ClothesController), nameof(UpdateClothSize), 1, User.Identity.Name);
                #endregion Logging info

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
                #region Logging info

                _logger.ErrorInDetail($"newException {clothSize}", correlationGuid,
                    $"{nameof(ClothesController)}_{nameof(UpdateClothSize)}_{nameof(NotFoundException)}",
                    ex, 1, User.Identity.Name);

                #endregion Logging info
                #region Result
                Response.Result = EnumResponseResult.Failed;
                Response.Data = CommonMessages.FAILED_UPDATING;
                Response.Errors.Add(ex.Message);
                #endregion

                return NotFound(Response);
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail($"newException {clothSize}", correlationGuid,
                    $"{nameof(ClothesController)}_{nameof(UpdateClothSize)}_{nameof(Exception)}", ex, 1, User.Identity.Name);

                #endregion Logging info
                #region Result
                Response.Result = EnumResponseResult.Failed;
                Response.Data = CommonMessages.FAILED_UPDATING;

                Response.Errors.Add(ex.Message);
                #endregion

                return BadRequest(Response);
            }
        }

        #endregion

        #region Products

        [Authorize]
        [HttpPost]
        [Route(nameof(AddNewCloth))]
        public async Task<IActionResult> AddNewCloth()
        {

            var Response = new ElBaytResponse<string>();
            Response.Errors = new List<string>();
            var correlationGuid = Guid.NewGuid();
            try
            {

                #region Logging info
                _logger.InfoInDetail(Request.Form, correlationGuid, nameof(ClothesController), nameof(AddNewCloth), 1, User.Identity.Name);
                #endregion Logging info
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
                #region Logging info

                _logger.ErrorInDetail($"newException {Request.Form}", correlationGuid, $"{nameof(ClothesController)}_{nameof(AddNewCloth)}_{nameof(NotFoundException)}", ex, 1, User.Identity.Name);

                #endregion Logging info


                #region Result
                Response.Result = EnumResponseResult.Failed;
                Response.Data = CommonMessages.FAILED_ADDING;
                Response.Errors.Add(ex.Message);
                #endregion

                return NotFound(Response);
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail($"newException {Request.Form}", correlationGuid,
                    $"{nameof(ClothesController)}_{nameof(AddNewCloth)}_{nameof(Exception)}", ex, 1, User.Identity.Name);

                #endregion Logging info

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
        public ActionResult GetClothes()
        {
            var Response = new ElBaytResponse<object>
            {
                Errors = new List<string>()
            };
            var correlationGuid = Guid.NewGuid();
            try
            {

                #region Logging info
                _logger.InfoInDetail("GetClothes", correlationGuid, nameof(ClothesController), nameof(GetClothes), 1, User.Identity.Name);
                #endregion Logging info

                var Departments = _departmentsServices.ClothesService.GetClothes();
                #region Result
                Response.Result = EnumResponseResult.Successed;
                Response.Data = Departments;
                #endregion

                return Ok(Response);
            }
            catch (NotFoundException ex)
            {
                #region Logging info

                _logger.ErrorInDetail($"newException GetClothes", correlationGuid,
                    $"{nameof(ClothesController)}_{nameof(GetClothes)}_{nameof(NotFoundException)}",
                    ex, 1, User.Identity.Name);

                #endregion Logging info
                #region Result
                Response.Result = EnumResponseResult.Failed;
                Response.Data = null;
                Response.Errors.Add(ex.Message);
                #endregion

                return NotFound(Response);
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail($"newException GetClothes", correlationGuid,
                    $"{nameof(ClothesController)}_{nameof(GetClothes)}_{nameof(Exception)}", ex, 1, User.Identity.Name);

                #endregion Logging info
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
        public async Task<ActionResult> GetCloth(Guid Id)
        {
            var Response = new ElBaytResponse<NumberClothDTO>
            {
                Errors = new List<string>()
            };
            var correlationGuid = Guid.NewGuid();
            try
            {

                #region Logging info
                _logger.InfoInDetail(Id, correlationGuid, nameof(ClothesController), nameof(GetCloth), 1, User.Identity.Name);
                #endregion Logging info

                var Product = await _departmentsServices.ClothesService.GetCloth(Id);
                #region Result
                Response.Result = EnumResponseResult.Successed;
                Response.Data = Product;
                #endregion

                return Ok(Response);
            }
            catch (NotFoundException ex)
            {
                #region Logging info

                _logger.ErrorInDetail($"newException {Id}", correlationGuid,
                    $"{nameof(ClothesController)}_{nameof(GetCloth)}_{nameof(NotFoundException)}",
                    ex, 1, User.Identity.Name);

                #endregion Logging info
                #region Result
                Response.Result = EnumResponseResult.Failed;
                Response.Data = null;
                Response.Errors.Add(ex.Message);
                #endregion

                return NotFound(Response);
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail($"newException {Id}", correlationGuid,
                    $"{nameof(ClothesController)}_{nameof(GetCloth)}_{nameof(Exception)}", ex, 1, User.Identity.Name);

                #endregion Logging info
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
        public async Task<ActionResult> DeleteCloth(Guid Id)
        {
            var Response = new ElBaytResponse<bool>
            {
                Errors = new List<string>()
            };
            var correlationGuid = Guid.NewGuid();
            try
            {

                #region Logging info
                _logger.InfoInDetail(Id, correlationGuid, nameof(ClothesController), nameof(DeleteCloth), 1, User.Identity.Name);
                #endregion Logging info

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
                #region Logging info

                _logger.ErrorInDetail($"newException {Id}", correlationGuid,
                    $"{nameof(ClothesController)}_{nameof(DeleteCloth)}_{nameof(NotFoundException)}",
                    ex, 1, User.Identity.Name);

                #endregion Logging info
                #region Result
                Response.Result = EnumResponseResult.Failed;
                Response.Data = false;
                Response.Errors.Add(ex.Message);
                #endregion

                return NotFound(Response);
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail($"newException {Id}", correlationGuid,
                    $"{nameof(ClothesController)}_{nameof(DeleteCloth)}_{nameof(Exception)}", ex, 1, User.Identity.Name);

                #endregion Logging info
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
        public async Task<ActionResult> UpdateCloth()
        {
            var Response = new ElBaytResponse<string>
            {
                Errors = new List<string>()
            };
            var correlationGuid = Guid.NewGuid();
            try
            {

                #region Logging info
                _logger.InfoInDetail(Request.Form, correlationGuid, nameof(ClothesController), nameof(UpdateCloth), 1, User.Identity.Name);
                #endregion Logging info

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
                #region Logging info

                _logger.ErrorInDetail($"newException {Request.Form}", correlationGuid,
                    $"{nameof(ClothesController)}_{nameof(UpdateCloth)}_{nameof(NotFoundException)}",
                    ex, 1, User.Identity.Name);

                #endregion Logging info
                #region Result
                Response.Result = EnumResponseResult.Failed;
                Response.Data = CommonMessages.FAILED_UPDATING;
                Response.Errors.Add(ex.Message);
                #endregion

                return NotFound(Response);
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail($"newException {Request.Form}", correlationGuid,
                    $"{nameof(ClothesController)}_{nameof(UpdateCloth)}_{nameof(Exception)}", ex, 1, User.Identity.Name);

                #endregion Logging info
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
        public async Task<ActionResult> UploadClothImage()
        {

            var Response = new ElBaytResponse<ClothImageDTO>
            {
                Errors = new List<string>()
            };


            var correlationGuid = Guid.NewGuid();
            try
            {

                #region Logging info
                _logger.InfoInDetail(Request.Form.Files[0], correlationGuid, nameof(ClothesController), nameof(UploadClothImage), 1, User.Identity.Name);
                #endregion Logging info

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
                #region Logging info

                _logger.ErrorInDetail($"newException {Request.Form.Files[0]}", correlationGuid,
                    $"{nameof(ClothesController)} _ {nameof(UpdateCloth)}_{nameof(NotFoundException)}",
                    ex, 1, User.Identity.Name);

                #endregion Logging info
                #region Result
                Response.Result = EnumResponseResult.Failed;
                Response.Data = null;
                Response.Errors.Add(ex.Message);
                #endregion

                return NotFound(Response);
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail($"newException {Request.Form.Files[0]}", correlationGuid,
                    $"{nameof(ClothesController)}_{nameof(UploadClothImage)}_{nameof(Exception)}", ex, 1, User.Identity.Name);

                #endregion Logging info
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
        public async Task<ActionResult> GetClothImages(Guid clothId)
        {
            var Response = new ElBaytResponse<List<ClothImageDTO>>
            {
                Errors = new List<string>()
            };
            var correlationGuid = Guid.NewGuid();
            try
            {

                #region Logging info
                _logger.InfoInDetail(clothId, correlationGuid, nameof(ClothesController), nameof(GetClothImages), 1, User.Identity.Name);
                #endregion Logging info

                var Images = await _departmentsServices.ClothesService.GetClothImages(clothId);
                #region Result
                Response.Result = EnumResponseResult.Successed;
                Response.Data = Images;
                #endregion

                return Ok(Response);
            }
            catch (NotFoundException ex)
            {
                #region Logging info

                _logger.ErrorInDetail($"newException GetClothImages", correlationGuid,
                    $"{nameof(ClothesController)}_{nameof(GetClothImages)}_{nameof(NotFoundException)}",
                    ex, 1, User.Identity.Name);

                #endregion Logging info
                #region Result
                Response.Result = EnumResponseResult.Failed;
                Response.Data = null;
                Response.Errors.Add(ex.Message);
                #endregion

                return NotFound(Response);
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail($"newException {clothId}", correlationGuid,
                    $"{nameof(ClothesController)}_{nameof(GetClothImages)}_{nameof(Exception)}", ex, 1, User.Identity.Name);

                #endregion Logging info
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
        public async Task<ActionResult> DeleteClothImage(Guid ImageId)
        {

            var Response = new ElBaytResponse<bool>
            {
                Errors = new List<string>()
            };


            var correlationGuid = Guid.NewGuid();
            try
            {

                #region Logging info
                _logger.InfoInDetail(ImageId, correlationGuid, nameof(ClothesController), nameof(DeleteClothImage), 1, User.Identity.Name);
                #endregion Logging info

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
                #region Logging info

                _logger.ErrorInDetail($"newException {ImageId}", correlationGuid,
                    $"{nameof(ClothesController)}_{nameof(DeleteClothImage)}_{nameof(NotFoundException)}",
                    ex, 1, User.Identity.Name);

                #endregion Logging info
                #region Result
                Response.Result = EnumResponseResult.Failed;
                Response.Data = false;
                Response.Errors.Add(ex.Message);
                #endregion

                return NotFound(Response);
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail($"newException {ImageId}", correlationGuid,
                    $"{nameof(ClothesController)}_{nameof(DeleteClothImage)}_{nameof(Exception)}", ex, 1, User.Identity.Name);

                #endregion Logging info
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
        public async Task<ActionResult> DeleteClothImageByURL(string URL)
        {

            var Response = new ElBaytResponse<bool>
            {
                Errors = new List<string>()
            };


            var correlationGuid = Guid.NewGuid();
            try
            {

                #region Logging info
                _logger.InfoInDetail(URL, correlationGuid, nameof(ClothesController), nameof(DeleteClothImageByURL), 1, User.Identity.Name);
                #endregion Logging info

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
                #region Logging info

                _logger.ErrorInDetail($"newException {URL}", correlationGuid,
                    $"{nameof(ClothesController)}_{nameof(DeleteClothImageByURL)}_{nameof(NotFoundException)}",
                    ex, 1, User.Identity.Name);

                #endregion Logging info
                #region Result
                Response.Result = EnumResponseResult.Failed;
                Response.Data = false;
                Response.Errors.Add(ex.Message);
                #endregion

                return NotFound(Response);
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail($"newException {URL}", correlationGuid,
                    $"{nameof(ClothesController)}_{nameof(DeleteClothImageByURL)}_{nameof(Exception)}", ex, 1, User.Identity.Name);

                #endregion Logging info
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
        public async Task<ActionResult> AddClothBrands(SelectedBrandsDTO SelectedBrands)
        {

            var Response = new ElBaytResponse<bool>
            {
                Errors = new List<string>()
            };


            var correlationGuid = Guid.NewGuid();
            try
            {

                #region Logging info
                _logger.InfoInDetail(SelectedBrands, correlationGuid, nameof(ClothesController), nameof(AddClothBrands), 1, User.Identity.Name);
                #endregion Logging info

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
                #region Logging info

                _logger.ErrorInDetail($"newException {SelectedBrands}", correlationGuid,
                    $"{nameof(ClothesController)}_{nameof(AddClothBrands)}_{nameof(NotFoundException)}",
                    ex, 1, User.Identity.Name);

                #endregion Logging info
                #region Result
                Response.Result = EnumResponseResult.Failed;
                Response.Data = false;
                Response.Errors.Add(ex.Message);
                #endregion

                return NotFound(Response);
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail($"newException {SelectedBrands}", correlationGuid,
                    $"{nameof(ClothesController)}_{nameof(AddClothBrands)}_{nameof(Exception)}", ex, 1, User.Identity.Name);

                #endregion Logging info
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
        public async Task<ActionResult> GetSelectedClothBrands(Guid clothId)
        {
            var Response = new ElBaytResponse<List<ClothBrandsDTO>>
            {
                Errors = new List<string>()
            };
            var correlationGuid = Guid.NewGuid();
            try
            {

                #region Logging info
                _logger.InfoInDetail(clothId, correlationGuid, nameof(ClothesController), nameof(GetClothBrands), 1, User.Identity.Name);
                #endregion Logging info

                var brands = await _departmentsServices.ClothesService.GetClothBrands(clothId);
                #region Result
                Response.Result = EnumResponseResult.Successed;
                Response.Data = brands;
                #endregion

                return Ok(Response);
            }
            catch (NotFoundException ex)
            {
                #region Logging info

                _logger.ErrorInDetail($"newException{clothId}", correlationGuid,
                    $"{nameof(ClothesController)}_{nameof(GetClothBrands)}_{nameof(NotFoundException)}",
                    ex, 1, User.Identity.Name);

                #endregion Logging info
                #region Result
                Response.Result = EnumResponseResult.Failed;
                Response.Data = null;
                Response.Errors.Add(ex.Message);
                #endregion

                return NotFound(Response);
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail($"newException {clothId}", correlationGuid,
                    $"{nameof(ClothesController)}_{nameof(GetClothBrands)}_{nameof(Exception)}", ex, 1, User.Identity.Name);

                #endregion Logging info
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
        public async Task<ActionResult> GetClothDBLInfo(Guid clothId)
        {
            var Response = new ElBaytResponse<ClothDBLDataDTO>
            {
                Errors = new List<string>()
            };
            var correlationGuid = Guid.NewGuid();
            try
            {

                #region Logging info
                _logger.InfoInDetail(clothId, correlationGuid, nameof(ClothesController), nameof(GetClothDBLInfo), 1, User.Identity.Name);
                #endregion Logging info

                var Info = await _departmentsServices.ClothesService.GetClothDBLInfo(clothId);
                #region Result
                Response.Result = EnumResponseResult.Successed;
                Response.Data = Info;
                #endregion

                return Ok(Response);
            }
            catch (NotFoundException ex)
            {
                #region Logging info

                _logger.ErrorInDetail($"newException{clothId}", correlationGuid,
                    $"{nameof(ClothesController)}_{nameof(GetClothBrands)}_{nameof(NotFoundException)}",
                    ex, 1, User.Identity.Name);

                #endregion Logging info
                #region Result
                Response.Result = EnumResponseResult.Failed;
                Response.Data = null;
                Response.Errors.Add(ex.Message);
                #endregion

                return NotFound(Response);
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail($"newException {clothId}", correlationGuid,
                    $"{nameof(ClothesController)}_{nameof(GetClothDBLInfo)}_{nameof(Exception)}", ex, 1, User.Identity.Name);

                #endregion Logging info
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
        public async Task<ActionResult> AddClothInfo(ClothInfoDTO ClothInfo)
        {

            var Response = new ElBaytResponse<string>
            {
                Errors = new List<string>()
            };


            var correlationGuid = Guid.NewGuid();
            try
            {

                #region Logging info
                _logger.InfoInDetail(ClothInfo, correlationGuid, nameof(ClothesController), nameof(AddClothInfo), 1, User.Identity.Name);
                #endregion Logging info

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
                #region Logging info

                _logger.ErrorInDetail($"newException {ClothInfo}", correlationGuid,
                    $"{nameof(ClothesController)}_{nameof(AddClothInfo)}_{nameof(NotFoundException)}",
                    ex, 1, User.Identity.Name);

                #endregion Logging info
                #region Result
                Response.Result = EnumResponseResult.Failed;
                Response.Data = ex.Message;
                Response.Errors.Add(ex.ErrorMessage);
                #endregion

                return NotFound(Response);
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail($"newException {ClothInfo}", correlationGuid,
                    $"{nameof(ClothesController)}_{nameof(AddClothInfo)}_{nameof(Exception)}", ex, 1, User.Identity.Name);

                #endregion Logging info
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
        public async Task<ActionResult> GetClothInfo(Guid clothId)
        {
            var Response = new ElBaytResponse<object>
            {
                Errors = new List<string>()
            };
            var correlationGuid = Guid.NewGuid();
            try
            {

                #region Logging info
                _logger.InfoInDetail(clothId, correlationGuid, nameof(ClothesController), nameof(GetClothInfo), 1, User.Identity.Name);
                #endregion Logging info

                var Info = await _departmentsServices.ClothesService.GetClothInfo(clothId);
                #region Result
                Response.Result = EnumResponseResult.Successed;
                Response.Data = Info;
                #endregion

                return Ok(Response);
            }
            catch (NotFoundException ex)
            {
                #region Logging info

                _logger.ErrorInDetail($"newException{clothId}", correlationGuid,
                    $"{nameof(ClothesController)}_{nameof(GetClothInfo)}_{nameof(NotFoundException)}",
                    ex, 1, User.Identity.Name);

                #endregion Logging info
                #region Result
                Response.Result = EnumResponseResult.Failed;
                Response.Data = null;
                Response.Errors.Add(ex.Message);
                #endregion

                return NotFound(Response);
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail($"newException {clothId}", correlationGuid,
                    $"{nameof(ClothesController)}_{nameof(GetClothInfo)}_{nameof(Exception)}", ex, 1, User.Identity.Name);

                #endregion Logging info
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
        public async Task<ActionResult> DeleteClothInfo(Guid Id)
        {
            var Response = new ElBaytResponse<bool>
            {
                Errors = new List<string>()
            };
            var correlationGuid = Guid.NewGuid();
            try
            {

                #region Logging info
                _logger.InfoInDetail(Id, correlationGuid, nameof(ClothesController), nameof(DeleteClothInfo), 1, User.Identity.Name);
                #endregion Logging info

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
                #region Logging info

                _logger.ErrorInDetail($"newException {Id}", correlationGuid,
                    $"{nameof(ClothesController)}_{nameof(DeleteClothInfo)}_{nameof(NotFoundException)}",
                    ex, 1, User.Identity.Name);

                #endregion Logging info
                #region Result
                Response.Result = EnumResponseResult.Failed;
                Response.Data = false;
                Response.Errors.Add(ex.Message);
                #endregion

                return NotFound(Response);
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail($"newException {Id}", correlationGuid,
                    $"{nameof(ClothesController)}_{nameof(DeleteClothInfo)}_{nameof(Exception)}", ex, 1, User.Identity.Name);

                #endregion Logging info
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
        public async Task<ActionResult> GetInfo(Guid Id)
        {
            var Response = new ElBaytResponse<ClothInfoDTO>
            {
                Errors = new List<string>()
            };
            var correlationGuid = Guid.NewGuid();
            try
            {

                #region Logging info
                _logger.InfoInDetail(Id, correlationGuid, nameof(ClothesController), nameof(GetInfo), 1, User.Identity.Name);
                #endregion Logging info

                var Info = await _departmentsServices.ClothesService.GetInfo(Id);
                #region Result
                Response.Result = EnumResponseResult.Successed;
                Response.Data = Info;
                #endregion

                return Ok(Response);
            }
            catch (NotFoundException ex)
            {
                #region Logging info

                _logger.ErrorInDetail($"newException {Id}", correlationGuid,
                    $"{nameof(ClothesController)}_{nameof(GetInfo)}_{nameof(NotFoundException)}",
                    ex, 1, User.Identity.Name);

                #endregion Logging info
                #region Result
                Response.Result = EnumResponseResult.Failed;
                Response.Data = null;
                Response.Errors.Add(ex.Message);
                #endregion

                return NotFound(Response);
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail($"newException {Id}", correlationGuid,
                    $"{nameof(ClothesController)}_{nameof(GetInfo)}_{nameof(Exception)}", ex, 1, User.Identity.Name);

                #endregion Logging info
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
