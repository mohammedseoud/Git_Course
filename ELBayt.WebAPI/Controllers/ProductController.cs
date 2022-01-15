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

namespace ElBayt_ECommerce.WebAPI.Controllers
{
    [EnableCors(CorsOrigin.LOCAL_ORIGIN)]
    [ApiController]
    [Route("api/v1.0/ElBayt/Product")]
    public class ProductController : Controller
    {
        private readonly IElBaytServices _elBaytServices;
        private readonly ILogger _logger;
        private readonly IConfiguration _config;
        private readonly IUserIdentity _userIdentity;

        public ProductController(IElBaytServices elBaytServices, ILogger logger, IConfiguration config
            , IUserIdentity userIdentity)
        {
            _elBaytServices = elBaytServices ?? throw new ArgumentNullException(nameof(elBaytServices));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _config= config ?? throw new ArgumentNullException(nameof(config));
            _userIdentity = userIdentity ?? throw new ArgumentNullException(nameof(userIdentity));
        }

        #region Products


        [HttpPost]
        [Route(nameof(AddNewProduct))]
        public async Task<IActionResult> AddNewProduct(ProductDTO Product)
        {

            var Response = new ElBaytResponse<string>();
            Response.Errors = new List<string>();
            var correlationGuid = Guid.NewGuid();
            try
            {

                #region Logging info
                _logger.InfoInDetail(Product, correlationGuid, nameof(ProductController), nameof(AddNewProduct), 1, User.Identity.Name);
                #endregion Logging info

                await _elBaytServices.ProductService.AddNewProduct(Product);

                #region Result
                Response.Result = EnumResponseResult.Successed;
                Response.Data = "Success in Adding";
                #endregion

                return Ok(Response);
            }
            catch (NotFoundException ex)
            {
                #region Logging info

                _logger.ErrorInDetail($"newException {Product}", correlationGuid, $"{nameof(ProductController)}_{nameof(AddNewProduct)}_{nameof(NotFoundException)}", ex, 1, User.Identity.Name);

                #endregion Logging info


                #region Result
                Response.Result = EnumResponseResult.Failed;
                Response.Data = "Failed in Adding";
                Response.Errors.Add(ex.Message);
                #endregion

                return NotFound(Response);
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail($"newException {Product}", correlationGuid,
                    $"{nameof(ProductController)}_{nameof(AddNewProduct)}_{nameof(Exception)}", ex, 1, User.Identity.Name);

                #endregion Logging info

                #region Result
                Response.Result = EnumResponseResult.Failed;
                Response.Data = "Failed in Adding";
               
                Response.Errors.Add(ex.Message);
                #endregion

                return BadRequest(Response);
            }
        }

        [HttpGet]
        [Route(nameof(GetProducts))]
        public ActionResult GetProducts()
        {
            var Response = new ElBaytResponse<object>
            {
                Errors = new List<string>()
            };
            var correlationGuid = Guid.NewGuid();
            try
            {

                #region Logging info
                _logger.InfoInDetail("GetAll", correlationGuid, nameof(ProductController), nameof(GetProducts), 1, User.Identity.Name);
                #endregion Logging info

                var Departments = _elBaytServices.ProductService.GetProducts();
                #region Result
                Response.Result = EnumResponseResult.Successed;
                Response.Data = Departments;
                #endregion

                return Ok(Response);
            }
            catch (NotFoundException ex)
            {
                #region Logging info

                _logger.ErrorInDetail($"newException GetProducts", correlationGuid,
                    $"{nameof(ProductController)}_{nameof(GetProducts)}_{nameof(NotFoundException)}",
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

                _logger.ErrorInDetail($"newException GetProducts", correlationGuid,
                    $"{nameof(ProductController)}_{nameof(GetProducts)}_{nameof(Exception)}", ex, 1, User.Identity.Name);

                #endregion Logging info
                #region Result
                Response.Result = EnumResponseResult.Failed;
                Response.Data = null;

                Response.Errors.Add(ex.Message);
                #endregion

                return BadRequest(Response);
            }
        }

        [HttpGet]
        [Route(nameof(GetProduct))]
        public async Task<ActionResult> GetProduct(Guid Id)
        {
            var Response = new ElBaytResponse<ProductDTO>
            {
                Errors = new List<string>()
            };
            var correlationGuid = Guid.NewGuid();
            try
            {

                #region Logging info
                _logger.InfoInDetail(Id, correlationGuid, nameof(ProductController), nameof(GetProduct), 1, User.Identity.Name);
                #endregion Logging info

                var Product = await _elBaytServices.ProductService.GetProduct(Id);
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
                    $"{nameof(ProductController)}_{nameof(GetProduct)}_{nameof(NotFoundException)}",
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
                    $"{nameof(ProductController)}_{nameof(GetProduct)}_{nameof(Exception)}", ex, 1, User.Identity.Name);

                #endregion Logging info
                #region Result
                Response.Result = EnumResponseResult.Failed;
                Response.Data = null;

                Response.Errors.Add(ex.Message);
                #endregion

                return BadRequest(Response);
            }
        }


        [HttpDelete]
        [Route(nameof(DeleteProduct))]
        public async Task<ActionResult> DeleteProduct(Guid Id)
        {
            var Response = new ElBaytResponse<bool>
            {
                Errors = new List<string>()
            };
            var correlationGuid = Guid.NewGuid();
            try
            {

                #region Logging info
                _logger.InfoInDetail(Id, correlationGuid, nameof(ProductController), nameof(DeleteProduct), 1, User.Identity.Name);
                #endregion Logging info

                var Res = await _elBaytServices.ProductService.DeleteProduct(Id);

                #region Result
                if (Res == "true")
                {

                    Response.Result = EnumResponseResult.Successed;
                    Response.Data = true;
                }
                else
                {
                    Response.Errors.Add(Res);
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
                    $"{nameof(ProductController)}_{nameof(DeleteProductCategory)}_{nameof(NotFoundException)}",
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
                    $"{nameof(ProductController)}_{nameof(DeleteProductCategory)}_{nameof(Exception)}", ex, 1, User.Identity.Name);

                #endregion Logging info
                #region Result
                Response.Result = EnumResponseResult.Failed;
                Response.Data = false;

                Response.Errors.Add(ex.Message);
                #endregion

                return BadRequest(Response);
            }
        }


        [HttpPut]
        [Route(nameof(UpdateProduct))]
        public async Task<ActionResult> UpdateProduct(ProductDTO product)
        {
            var Response = new ElBaytResponse<bool>
            {
                Errors = new List<string>()
            };
            var correlationGuid = Guid.NewGuid();
            try
            {

                #region Logging info
                _logger.InfoInDetail(product, correlationGuid, nameof(ProductController), nameof(UpdateProduct), 1, User.Identity.Name);
                #endregion Logging info

                await _elBaytServices.ProductService.UpdateProduct(product);

                #region Result


                Response.Result = EnumResponseResult.Successed;
                Response.Data = true;

                #endregion

                return Ok(Response);
            }
            catch (NotFoundException ex)
            {
                #region Logging info

                _logger.ErrorInDetail($"newException {product}", correlationGuid,
                    $"{nameof(ProductController)}_{nameof(UpdateProduct)}_{nameof(NotFoundException)}",
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

                _logger.ErrorInDetail($"newException {product}", correlationGuid,
                    $"{nameof(ProductController)}_{nameof(UpdateProduct)}_{nameof(Exception)}", ex, 1, User.Identity.Name);

                #endregion Logging info
                #region Result
                Response.Result = EnumResponseResult.Failed;
                Response.Data = false;

                Response.Errors.Add(ex.Message);
                #endregion

                return BadRequest(Response);
            }
        }

        [HttpPost]
        [Route(nameof(UploadProductImage))]
        public async Task<ActionResult> UploadProductImage()
        {
            
            var Response = new ElBaytResponse<ProductImageDTO>
            {
                Errors = new List<string>()
            };


            var correlationGuid = Guid.NewGuid();
            try
            {
                
                #region Logging info
                _logger.InfoInDetail(Request.Form.Files[0], correlationGuid, nameof(ProductController), nameof(UploadProductImage), 1, User.Identity.Name);
                #endregion Logging info

                var file = Request.Form.Files[0];

                if (file.Length > 0)
                {
   
                    var DisImage = await _elBaytServices.ProductService.SaveProductImage(Request.Form["ProductId"].ToString(), file, _config["FilesInfo:WebFolder"]);
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
                    $"{nameof(ProductController)}_{nameof(UpdateProduct)}_{nameof(NotFoundException)}",
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
                    $"{nameof(ProductController)}_{nameof(UpdateProduct)}_{nameof(Exception)}", ex, 1, User.Identity.Name);

                #endregion Logging info
                #region Result
                Response.Result = EnumResponseResult.Failed;
                Response.Data = null;

                Response.Errors.Add(ex.Message);
                #endregion

                return BadRequest(Response);
            }
        }

        [HttpGet]
        [Route(nameof(GetProductImages))]
        public  ActionResult GetProductImages(Guid ProductId)
        {
            var Response = new ElBaytResponse<object>
            {
                Errors = new List<string>()
            };
            var correlationGuid = Guid.NewGuid();
            try
            {

                #region Logging info
                _logger.InfoInDetail(ProductId, correlationGuid, nameof(ProductController), nameof(GetProductImages), 1, User.Identity.Name);
                #endregion Logging info

                var Images = _elBaytServices.ProductService.GetProductImages(ProductId);
                #region Result
                Response.Result = EnumResponseResult.Successed;
                Response.Data = Images;
                #endregion

                return Ok(Response);
            }
            catch (NotFoundException ex)
            {
                #region Logging info

                _logger.ErrorInDetail($"newException GetProducts", correlationGuid,
                    $"{nameof(ProductController)}_{nameof(GetProductImages)}_{nameof(NotFoundException)}",
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

                _logger.ErrorInDetail($"newException {ProductId}", correlationGuid,
                    $"{nameof(ProductController)}_{nameof(GetProductImages)}_{nameof(Exception)}", ex, 1, User.Identity.Name);

                #endregion Logging info
                #region Result
                Response.Result = EnumResponseResult.Failed;
                Response.Data = null;

                Response.Errors.Add(ex.Message);
                #endregion

                return BadRequest(Response);
            }
        }

        [HttpDelete]
        [Route(nameof(DeleteProductImage))]
        public async Task<ActionResult> DeleteProductImage(Guid ImageId)
        {

            var Response = new ElBaytResponse<bool>
            {
                Errors = new List<string>()
            };


            var correlationGuid = Guid.NewGuid();
            try
            {

                #region Logging info
                _logger.InfoInDetail(ImageId, correlationGuid, nameof(ProductController), nameof(DeleteProductImage), 1, User.Identity.Name);
                #endregion Logging info

                var Image = await _elBaytServices.ProductService.GetProductImage(ImageId);
                if (Image != null)
                {
                    var Res = await _elBaytServices.ProductService.DeleteProductImage(ImageId);
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
                    $"{nameof(ProductController)}_{nameof(DeleteProductImage)}_{nameof(NotFoundException)}",
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
                    $"{nameof(ProductController)}_{nameof(DeleteProductImage)}_{nameof(Exception)}", ex, 1, User.Identity.Name);

                #endregion Logging info
                #region Result
                Response.Result = EnumResponseResult.Failed;
                Response.Data = false;

                Response.Errors.Add(ex.Message);
                #endregion

                return BadRequest(Response);
            }
        }

        #endregion

        #region Categories
        [HttpPost]
        [Route(nameof(AddNewProductCategory))]
        public async Task<IActionResult> AddNewProductCategory(ProductCategoryDTO ProductCategory)
        {

            var Response = new ElBaytResponse<string>();
            Response.Errors = new List<string>();
            var correlationGuid = Guid.NewGuid();
            try
            {

                #region Logging info
                _logger.InfoInDetail(ProductCategory, correlationGuid, nameof(ProductController), nameof(AddNewProductCategory), 1, User.Identity.Name);
                #endregion Logging info

                await _elBaytServices.ProductService.AddNewProductCategory(ProductCategory);

                #region Result
                Response.Result = EnumResponseResult.Successed;
                Response.Data = "Success in Adding";
                #endregion

                return Ok(Response);
            }
            catch (NotFoundException ex)
            {
                #region Logging info

                _logger.ErrorInDetail($"newException {ProductCategory}", correlationGuid, $"{nameof(ProductController)}_{nameof(AddNewProductCategory)}_{nameof(NotFoundException)}", ex, 1, User.Identity.Name);

                #endregion Logging info


                #region Result
                Response.Result = EnumResponseResult.Failed;
                Response.Data = "Failed in Adding";
                Response.Errors.Add(ex.Message);
                #endregion

                return NotFound(Response);
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail($"newException {ProductCategory}", correlationGuid,
                    $"{nameof(ProductController)}_{nameof(AddNewProductCategory)}_{nameof(Exception)}", ex, 1, User.Identity.Name);

                #endregion Logging info

                #region Result
                Response.Result = EnumResponseResult.Failed;
                Response.Data = "Failed in Adding";

                Response.Errors.Add(ex.Message);
                #endregion

                return BadRequest(Response);
            }
        }

        [HttpGet]
        [Route(nameof(GetProductCategories))]
        public ActionResult GetProductCategories()
        {
            var Response = new ElBaytResponse<object>
            {
                Errors = new List<string>()
            };
            var correlationGuid = Guid.NewGuid();
            try
            {

                #region Logging info
                _logger.InfoInDetail("GetAll", correlationGuid, nameof(ProductController), nameof(GetProductCategories), 1, User.Identity.Name);
                #endregion Logging info

                var Categories = _elBaytServices.ProductService.GetProductCategories();
                #region Result
                Response.Result = EnumResponseResult.Successed;
                Response.Data = Categories;
                #endregion

                return Ok(Response);
            }
            catch (NotFoundException ex)
            {
                #region Logging info

                _logger.ErrorInDetail($"newException GetProductCategories", correlationGuid,
                    $"{nameof(ProductController)}_{nameof(GetProductCategories)}_{nameof(NotFoundException)}",
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

                _logger.ErrorInDetail($"newException GetProductCategories", correlationGuid,
                    $"{nameof(ProductController)}_{nameof(GetProductCategories)}_{nameof(Exception)}", ex, 1, User.Identity.Name);

                #endregion Logging info
                #region Result
                Response.Result = EnumResponseResult.Failed;
                Response.Data = null;

                Response.Errors.Add(ex.Message);
                #endregion

                return BadRequest(Response);
            }
        }

        [HttpGet]
        [Route(nameof(GetProductCategory))]
        public async Task<ActionResult> GetProductCategory(Guid Id)
        {
            var Response = new ElBaytResponse<ProductCategoryDTO>
            {
                Errors = new List<string>()
            };
            var correlationGuid = Guid.NewGuid();
            try
            {

                #region Logging info
                _logger.InfoInDetail(Id, correlationGuid, nameof(ProductController), nameof(GetProductCategory), 1, User.Identity.Name);
                #endregion Logging info

                var Category = await _elBaytServices.ProductService.GetProductCategory(Id);
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
                    $"{nameof(ProductController)}_{nameof(GetProductCategory)}_{nameof(NotFoundException)}",
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
                    $"{nameof(ProductController)}_{nameof(GetProductCategory)}_{nameof(Exception)}", ex, 1, User.Identity.Name);

                #endregion Logging info
                #region Result
                Response.Result = EnumResponseResult.Failed;
                Response.Data = null;

                Response.Errors.Add(ex.Message);
                #endregion

                return BadRequest(Response);
            }
        }


        [HttpDelete]
        [Route(nameof(DeleteProductCategory))]
        public async Task<ActionResult> DeleteProductCategory(Guid Id)
        {
            var Response = new ElBaytResponse<bool>
            {
                Errors = new List<string>()
            };
            var correlationGuid = Guid.NewGuid();
            try
            {

                #region Logging info
                _logger.InfoInDetail(Id, correlationGuid, nameof(ProductController), nameof(DeleteProductCategory), 1, User.Identity.Name);
                #endregion Logging info

                var Res = await _elBaytServices.ProductService.DeleteProductCategory(Id);

                #region Result
                if (Res == "true")
                {

                    Response.Result = EnumResponseResult.Successed;
                    Response.Data = true;
                }
                else
                {
                    Response.Errors.Add(Res);
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
                    $"{nameof(ProductController)}_{nameof(DeleteProductCategory)}_{nameof(NotFoundException)}",
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
                    $"{nameof(ProductController)}_{nameof(DeleteProductCategory)}_{nameof(Exception)}", ex, 1, User.Identity.Name);

                #endregion Logging info
                #region Result
                Response.Result = EnumResponseResult.Failed;
                Response.Data = false;

                Response.Errors.Add(ex.Message);
                #endregion

                return BadRequest(Response);
            }
        }


        [HttpPut]
        [Route(nameof(UpdateProductCategory))]
        public async Task<ActionResult> UpdateProductCategory(ProductCategoryDTO productCategory)
        {
            var Response = new ElBaytResponse<bool>
            {
                Errors = new List<string>()
            };
            var correlationGuid = Guid.NewGuid();
            try
            {

                #region Logging info
                _logger.InfoInDetail(productCategory, correlationGuid, nameof(ProductController), nameof(UpdateProductCategory), 1, User.Identity.Name);
                #endregion Logging info

                await _elBaytServices.ProductService.UpdateProductCategory(productCategory);

                #region Result


                Response.Result = EnumResponseResult.Successed;
                Response.Data = true;

                #endregion

                return Ok(Response);
            }
            catch (NotFoundException ex)
            {
                #region Logging info

                _logger.ErrorInDetail($"newException {productCategory}", correlationGuid,
                    $"{nameof(ProductController)}_{nameof(UpdateProductCategory)}_{nameof(NotFoundException)}",
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

                _logger.ErrorInDetail($"newException {productCategory}", correlationGuid,
                    $"{nameof(ProductController)}_{nameof(UpdateProductCategory)}_{nameof(Exception)}", ex, 1, User.Identity.Name);

                #endregion Logging info
                #region Result
                Response.Result = EnumResponseResult.Failed;
                Response.Data = false;

                Response.Errors.Add(ex.Message);
                #endregion

                return BadRequest(Response);
            }
        }

        #endregion

        #region Types

        [HttpPost]
        [Route(nameof(AddNewProductType))]
        public async Task<IActionResult> AddNewProductType(ProductTypeDTO ProductType)
        {

            var Response = new ElBaytResponse<string>();
            Response.Errors = new List<string>();
            var correlationGuid = Guid.NewGuid();
            try
            {

                #region Logging info
                _logger.InfoInDetail(ProductType, correlationGuid, nameof(ProductController), nameof(AddNewProductType), 1, User.Identity.Name);
                #endregion Logging info

                await _elBaytServices.ProductService.AddNewProductType(ProductType);

                #region Result
                Response.Result = EnumResponseResult.Successed;
                Response.Data = "Success in Adding";
                #endregion

                return Ok(Response);
            }
            catch (NotFoundException ex)
            {
                #region Logging info

                _logger.ErrorInDetail($"newException {ProductType}", correlationGuid, $"{nameof(ProductController)}_{nameof(AddNewProductType)}_{nameof(NotFoundException)}", ex, 1, User.Identity.Name);

                #endregion Logging info


                #region Result
                Response.Result = EnumResponseResult.Failed;
                Response.Data = "Failed in Adding";
                Response.Errors.Add(ex.Message);
                #endregion

                return NotFound(Response);
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail($"newException {ProductType}", correlationGuid,
                    $"{nameof(ProductController)}_{nameof(AddNewProductType)}_{nameof(Exception)}", ex, 1, User.Identity.Name);

                #endregion Logging info

                #region Result
                Response.Result = EnumResponseResult.Failed;
                Response.Data = "Failed in Adding";

                Response.Errors.Add(ex.Message);
                #endregion

                return BadRequest(Response);
            }
        }

        [HttpGet]
        [Route(nameof(GetProductTypes))]
        public ActionResult GetProductTypes()
        {
            var Response = new ElBaytResponse<object>
            {
                Errors = new List<string>()
            };
            var correlationGuid = Guid.NewGuid();
            try
            {

                #region Logging info
                _logger.InfoInDetail("GetAll", correlationGuid, nameof(ProductController), nameof(GetProductTypes), 1, User.Identity.Name);
                #endregion Logging info

                var Types = _elBaytServices.ProductService.GetProductTypes();
                #region Result
                Response.Result = EnumResponseResult.Successed;
                Response.Data = Types;
                #endregion

                return Ok(Response);
            }
            catch (NotFoundException ex)
            {
                #region Logging info

                _logger.ErrorInDetail($"newException GetProductTypes", correlationGuid,
                    $"{nameof(ProductController)}_{nameof(GetProductTypes)}_{nameof(NotFoundException)}",
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

                _logger.ErrorInDetail($"newException GetProductTypes", correlationGuid,
                    $"{nameof(ProductController)}_{nameof(GetProductTypes)}_{nameof(Exception)}", ex, 1, User.Identity.Name);

                #endregion Logging info
                #region Result
                Response.Result = EnumResponseResult.Failed;
                Response.Data = null;

                Response.Errors.Add(ex.Message);
                #endregion

                return BadRequest(Response);
            }
        }

        [HttpGet]
        [Route(nameof(GetProductType))]
        public async Task<ActionResult> GetProductType(Guid Id)
        {
            var Response = new ElBaytResponse<ProductTypeDTO>
            {
                Errors = new List<string>()
            };
            var correlationGuid = Guid.NewGuid();
            try
            {

                #region Logging info
                _logger.InfoInDetail(Id, correlationGuid, nameof(ProductController), nameof(GetProductType), 1, User.Identity.Name);
                #endregion Logging info

                var Type = await _elBaytServices.ProductService.GetProductType(Id);
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
                    $"{nameof(ProductController)}_{nameof(GetProductType)}_{nameof(NotFoundException)}",
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
                    $"{nameof(ProductController)}_{nameof(GetProductType)}_{nameof(Exception)}", ex, 1, User.Identity.Name);

                #endregion Logging info
                #region Result
                Response.Result = EnumResponseResult.Failed;
                Response.Data = null;

                Response.Errors.Add(ex.Message);
                #endregion

                return BadRequest(Response);
            }
        }


        [HttpDelete]
        [Route(nameof(DeleteProductType))]
        public async Task<ActionResult> DeleteProductType(Guid Id)
        {
            var Response = new ElBaytResponse<bool>
            {
                Errors = new List<string>()
            };
            var correlationGuid = Guid.NewGuid();
            try
            {

                #region Logging info
                _logger.InfoInDetail(Id, correlationGuid, nameof(ProductController), nameof(DeleteProductType), 1, User.Identity.Name);
                #endregion Logging info

                var Res = await _elBaytServices.ProductService.DeleteProductType(Id);

                #region Result
                if (Res == "true")
                {

                    Response.Result = EnumResponseResult.Successed;
                    Response.Data = true;
                }
                else
                {
                    Response.Errors.Add(Res);
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
                    $"{nameof(ProductController)}_{nameof(DeleteProductType)}_{nameof(NotFoundException)}",
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
                    $"{nameof(ProductController)}_{nameof(DeleteProductType)}_{nameof(Exception)}", ex, 1, User.Identity.Name);

                #endregion Logging info
                #region Result
                Response.Result = EnumResponseResult.Failed;
                Response.Data = false;

                Response.Errors.Add(ex.Message);
                #endregion

                return BadRequest(Response);
            }
        }


        [HttpPut]
        [Route(nameof(UpdateProductType))]
        public async Task<ActionResult> UpdateProductType(ProductTypeDTO productType)
        {
            var Response = new ElBaytResponse<bool>
            {
                Errors = new List<string>()
            };
            var correlationGuid = Guid.NewGuid();
            try
            {

                #region Logging info
                _logger.InfoInDetail(productType, correlationGuid, nameof(ProductController), nameof(UpdateProductType), 1, User.Identity.Name);
                #endregion Logging info

                await _elBaytServices.ProductService.UpdateProductType(productType);

                #region Result


                Response.Result = EnumResponseResult.Successed;
                Response.Data = true;

                #endregion

                return Ok(Response);
            }
            catch (NotFoundException ex)
            {
                #region Logging info

                _logger.ErrorInDetail($"newException {productType}", correlationGuid,
                    $"{nameof(ProductController)}_{nameof(UpdateProductType)}_{nameof(NotFoundException)}",
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

                _logger.ErrorInDetail($"newException {productType}", correlationGuid,
                    $"{nameof(ProductController)}_{nameof(UpdateProductType)}_{nameof(Exception)}", ex, 1, User.Identity.Name);

                #endregion Logging info
                #region Result
                Response.Result = EnumResponseResult.Failed;
                Response.Data = false;

                Response.Errors.Add(ex.Message);
                #endregion

                return BadRequest(Response);
            }
        }

        #endregion

        #region Departments
        [HttpPost]
        [Route(nameof(AddNewProductDepartment))]
        public async Task<IActionResult> AddNewProductDepartment(ProductDepartmentDTO ProductDepartment)
        {

            var Response = new ElBaytResponse<string>();
            Response.Errors = new List<string>();
            var correlationGuid = Guid.NewGuid();
            try
            {

                #region Logging info
                _logger.InfoInDetail(ProductDepartment, correlationGuid, nameof(ProductController), nameof(AddNewProductDepartment), 1, User.Identity.Name);
                #endregion Logging info

                await _elBaytServices.ProductService.AddNewProductDepartment(ProductDepartment);

                #region Result
                Response.Result = EnumResponseResult.Successed;
                Response.Data = "Success in Adding";
                #endregion

                return Ok(Response);
            }
            catch (NotFoundException ex)
            {
                #region Logging info

                _logger.ErrorInDetail($"newException {ProductDepartment}", correlationGuid, $"{nameof(ProductController)}_{nameof(AddNewProductDepartment)}_{nameof(NotFoundException)}", ex, 1, User.Identity.Name);

                #endregion Logging info


                #region Result
                Response.Result = EnumResponseResult.Failed;
                Response.Data = "Failed in Adding";
                Response.Errors.Add(ex.Message);
                #endregion

                return NotFound(Response);
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail($"newException {ProductDepartment}", correlationGuid,
                    $"{nameof(ProductController)}_{nameof(AddNewProductDepartment)}_{nameof(Exception)}", ex, 1, User.Identity.Name);

                #endregion Logging info

                #region Result
                Response.Result = EnumResponseResult.Failed;
                Response.Data = "Failed in Adding";

                Response.Errors.Add(ex.Message);
                #endregion

                return BadRequest(Response);
            }
        }

        [HttpGet]
        [Route(nameof(GetProductDepartments))]
        public ActionResult GetProductDepartments()
        {
          
            var Response = new ElBaytResponse<object>
            {
                Errors = new List<string>()
            };
            var correlationGuid = Guid.NewGuid();
            try
            {

                #region Logging info

                _logger.InfoInDetail("GetAll", correlationGuid, nameof(ProductController), nameof(GetProductDepartments), 1, User.Identity.Name);
                #endregion Logging info
              
                var Departments = _elBaytServices.ProductService.GetProductDepartments();
            
                #region Result
                Response.Result = EnumResponseResult.Successed;
                Response.Data = Departments;
                #endregion

                return Ok(Response);
            }
            catch (NotFoundException ex)
            {
                #region Logging info

                _logger.ErrorInDetail($"newException GetProductDepartments", correlationGuid,
                    $"{nameof(ProductController)}_{nameof(GetProductDepartments)}_{nameof(NotFoundException)}",
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

                _logger.ErrorInDetail($"newException GetProductDepartments", correlationGuid,
                    $"{nameof(ProductController)}_{nameof(GetProductDepartments)}_{nameof(Exception)}", ex, 1, User.Identity.Name);

                #endregion Logging info
                #region Result
                Response.Result = EnumResponseResult.Failed;
                Response.Data = null;

                Response.Errors.Add(ex.Message);
                #endregion

                return BadRequest(Response);
            }
        }

        [HttpGet]
        [Route(nameof(GetProductDepartment))]
        public async Task<ActionResult> GetProductDepartment(Guid Id)
        {
            var Response = new ElBaytResponse<ProductDepartmentDTO>
            {
                Errors = new List<string>()
            };
            var correlationGuid = Guid.NewGuid();
            try
            {

                #region Logging info
                _logger.InfoInDetail(Id, correlationGuid, nameof(ProductController), nameof(GetProductDepartment), 1, User.Identity.Name);
                #endregion Logging info

                var Department = await _elBaytServices.ProductService.GetProductDepartment(Id);
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
                    $"{nameof(ProductController)}_{nameof(GetProductDepartment)}_{nameof(NotFoundException)}",
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
                    $"{nameof(ProductController)}_{nameof(GetProductDepartment)}_{nameof(Exception)}", ex, 1, User.Identity.Name);

                #endregion Logging info
                #region Result
                Response.Result = EnumResponseResult.Failed;
                Response.Data = null;

                Response.Errors.Add(ex.Message);
                #endregion

                return BadRequest(Response);
            }
        }


        [HttpDelete]
        [Route(nameof(DeleteProductDepartment))]
        public async Task< ActionResult> DeleteProductDepartment(Guid Id)
        {
            var Response = new ElBaytResponse<bool>
            {
                Errors = new List<string>()
            };
            var correlationGuid = Guid.NewGuid();
            try
            {

                #region Logging info
                _logger.InfoInDetail(Id, correlationGuid, nameof(ProductController), nameof(DeleteProductDepartment), 1, User.Identity.Name);
                #endregion Logging info

                var Res = await _elBaytServices.ProductService.DeleteProductDepartment(Id);
              
                #region Result
                if (Res == "true")
                {
                  
                    Response.Result = EnumResponseResult.Successed;
                    Response.Data = true;
                 }
                else
                {
                    Response.Errors.Add(Res);
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
                    $"{nameof(ProductController)}_{nameof(DeleteProductDepartment)}_{nameof(NotFoundException)}",
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
                    $"{nameof(ProductController)}_{nameof(DeleteProductDepartment)}_{nameof(Exception)}", ex, 1, User.Identity.Name);

                #endregion Logging info
                #region Result
                Response.Result = EnumResponseResult.Failed;
                Response.Data = false;

                Response.Errors.Add(ex.Message);
                #endregion

                return BadRequest(Response);
            }
        }


        [HttpPut]
        [Route(nameof(UpdateProductDepartment))]
        public async Task<ActionResult> UpdateProductDepartment(ProductDepartmentDTO productDepartment)
        {
            var Response = new ElBaytResponse<bool>
            {
                Errors = new List<string>()
            };
            var correlationGuid = Guid.NewGuid();
            try
            {

                #region Logging info
                _logger.InfoInDetail(productDepartment, correlationGuid, nameof(ProductController), nameof(UpdateProductDepartment), 1, User.Identity.Name);
                #endregion Logging info

                 await _elBaytServices.ProductService.UpdateProductDepartment(productDepartment);

                #region Result


                Response.Result = EnumResponseResult.Successed;
                Response.Data = true;
               
                #endregion

                return Ok(Response);
            }
            catch (NotFoundException ex)
            {
                #region Logging info

                _logger.ErrorInDetail($"newException {productDepartment}", correlationGuid,
                    $"{nameof(ProductController)}_{nameof(UpdateProductDepartment)}_{nameof(NotFoundException)}",
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

                _logger.ErrorInDetail($"newException {productDepartment}", correlationGuid,
                    $"{nameof(ProductController)}_{nameof(UpdateProductDepartment)}_{nameof(Exception)}", ex, 1, User.Identity.Name);

                #endregion Logging info
                #region Result
                Response.Result = EnumResponseResult.Failed;
                Response.Data = false;

                Response.Errors.Add(ex.Message);
                #endregion

                return BadRequest(Response);
            }
        }

        #endregion

    }
}
