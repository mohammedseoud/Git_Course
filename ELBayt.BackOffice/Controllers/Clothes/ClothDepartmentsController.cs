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
    
    public class ClothDepartmentsController : ELBaytController
    {
        private readonly IDepartmentsServices _departmentsServices;
        private readonly ILogger _logger;
        private readonly IUserIdentity _userIdentity;

        public ClothDepartmentsController(IDepartmentsServices departmentsServices, ILogger logger, IConfiguration config
            , IUserIdentity userIdentity):base(config)
        {
            _departmentsServices = departmentsServices ?? throw new ArgumentNullException(nameof(departmentsServices));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _userIdentity = userIdentity ?? throw new ArgumentNullException(nameof(userIdentity));
        }

        #region Departments

        [Authorize]
        [HttpPost]
        [Route(nameof(AddNewClothDepartment))]
        [Produces(General.JSONCONTENTTYPE, Type = typeof(ElBaytResponse<string>))]
        public async Task<IActionResult> AddNewClothDepartment([FromForm] AddClothDepartmentDTO ClothDepartment)
        {

            var Response = new ElBaytResponse<string>
            {
                Errors = new List<string>()
            };
            try
            {
                var DepartmentPic = await _departmentsServices.ClothesService.AddNewClothDepartment(Request.Form, _config["FilesInfo:WebFolder"]);

                if (!string.IsNullOrEmpty(DepartmentPic))
                {
                    SaveFile(DepartmentPic, 0);
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
        [Route(nameof(GetClothDepartments))]
        [Produces(General.JSONCONTENTTYPE, Type = typeof(ElBaytResponse<List<GetClothDepartmentDTO>>))]
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
        [Produces(General.JSONCONTENTTYPE, Type = typeof(ElBaytResponse<ClothDepartmentDTO>))]
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
        [Produces(General.JSONCONTENTTYPE, Type = typeof(ElBaytResponse<bool>))]
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
        [Route(nameof(UpdateClothDepartment))]
        [Produces(General.JSONCONTENTTYPE, Type = typeof(ElBaytResponse<string>))]
        public async Task<IActionResult> UpdateClothDepartment([FromForm] AddClothDepartmentDTO ClothDepartment)
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
