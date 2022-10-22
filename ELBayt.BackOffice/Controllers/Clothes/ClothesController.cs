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
using Microsoft.AspNetCore.Http;

namespace ElBayt_ECommerce.WebAPI.Controllers
{

    public class ClothesController : ELBaytController
    {
        private readonly IDepartmentsServices _departmentsServices;
        private readonly ILogger _logger;
        private readonly IUserIdentity _userIdentity;

        public ClothesController(IDepartmentsServices departmentsServices, ILogger logger, IConfiguration config
            , IUserIdentity userIdentity) : base(config)
        {
            _departmentsServices = departmentsServices ?? throw new ArgumentNullException(nameof(departmentsServices));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _userIdentity = userIdentity ?? throw new ArgumentNullException(nameof(userIdentity));
        }

        #region Clothes

        [Authorize]
        [HttpPost]
        [Route(nameof(AddNewCloth))]
        [Produces(General.JSONCONTENTTYPE, Type = typeof(ElBaytResponse<string>))]
        public async Task<IActionResult> AddNewCloth([FromForm] AddClothDTO cloth)
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
        [Produces(General.JSONCONTENTTYPE, Type = typeof(ElBaytResponse<List<GetClothDTO>>))]
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
        [Produces(General.JSONCONTENTTYPE, Type = typeof(ElBaytResponse<NumberClothDTO>))]
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
                    DeleteDirectory(URL);
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
        [Route(nameof(UpdateCloth))]
        [Produces(General.JSONCONTENTTYPE, Type = typeof(ElBaytResponse<ClothImageDTO>))]
        public async Task<IActionResult> UpdateCloth([FromForm] AddClothDTO cloth)
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
                            SaveFile(imageUrls[3], fileindex);                        
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
        [Produces(General.JSONCONTENTTYPE, Type = typeof(ElBaytResponse<ClothImageDTO>))]
        public async Task<IActionResult> UploadClothImage([FromForm] AddClothImageDTO image)
        {

            var Response = new ElBaytResponse<ClothImageDTO>
            {
                Errors = new List<string>()
            };

            try
            {
                if (Request.Form.Files.Count > 0)
                {
                    var file = Request.Form.Files[0];

                    if (file.Length > 0)
                    {
                        var Image = await _departmentsServices.ClothesService.SaveClothImage(Request.Form["ClothId"].ToString(), file, _config["FilesInfo:WebFolder"]);
                        if (!string.IsNullOrEmpty(Image.URL))
                            SaveFile(Image.URL, 0);
                        Response.Result = EnumResponseResult.Successed;
                        Response.Data = Image;
                        return Ok(Response);
                    }

                    #region Result

                    Response.Errors.Add(CommonMessages.ZERO_FILE_SIZE);
                    Response.Result = EnumResponseResult.Failed;
                    Response.Data = null;

                    #endregion
                }
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
        [Route(nameof(GetClothImages))]
        [Produces(General.JSONCONTENTTYPE, Type = typeof(ElBaytResponse<List<ClothImageDTO>>))]
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
        [Produces(General.JSONCONTENTTYPE, Type = typeof(ElBaytResponse<bool>))]
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
                    if (Res == CommonMessages.SUCCESSFULLY_DELETING)
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
        [Produces(General.JSONCONTENTTYPE, Type = typeof(ElBaytResponse<bool>))]
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
        [Produces(General.JSONCONTENTTYPE, Type = typeof(ElBaytResponse<bool>))]
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
        [Produces(General.JSONCONTENTTYPE, Type = typeof(ElBaytResponse<List<ClothBrandsDTO>>))]
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
        [Produces(General.JSONCONTENTTYPE, Type = typeof(ElBaytResponse<ClothDBLDataDTO>))]
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
        [Produces(General.JSONCONTENTTYPE, Type = typeof(ElBaytResponse<string>))]
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
        [Produces(General.JSONCONTENTTYPE, Type = typeof(ElBaytResponse<List<ClothInfoDataDTO>>))]
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
        [Produces(General.JSONCONTENTTYPE, Type = typeof(ElBaytResponse<bool>))]
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
        [Produces(General.JSONCONTENTTYPE, Type = typeof(ElBaytResponse<ClothInfoDTO>))]
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
