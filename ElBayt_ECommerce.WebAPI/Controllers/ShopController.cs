using ElBayt.Common.Common;
using ElBayt.Common.Core.Logging;
using ElBayt.Common.Enums;
using ElBayt.DTO.ELBayt.DTOs;
using ElBayt.Services.IElBaytServices;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElBayt_ECommerce.WebAPI.Controllers
{
    [EnableCors(CorsOrigin.LOCAL_ORIGIN)]
    [ApiController]
    [Route("api/v1.0/ElBaytECommerce/Shop")]
    public class ShopController : Controller
    {
        private readonly IElBaytServices _elBaytServices;
        private readonly ILogger _logger;

        public ShopController(IElBaytServices elBaytServices, ILogger logger)
        {
            _elBaytServices = elBaytServices ?? throw new ArgumentNullException(nameof(elBaytServices));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        //#region Products

        //[HttpGet]
        //[Route(nameof(GetProducts))]
        //public ActionResult GetProducts()
        //{
        //    var Response = new ElBaytResponse<object>
        //    {
        //        Errors = new List<string>()
        //    };
        //    var correlationGuid = Guid.NewGuid();
        //    try
        //    {

        //        #region Logging info
        //        _logger.InfoInDetail("GetAll", correlationGuid, nameof(ShopController), nameof(GetProducts), 1, User.Identity.Name);
        //        #endregion Logging info

        //        var Departments = _elBaytServices.ProductService.GetProducts();
        //        #region Result
        //        Response.Result = EnumResponseResult.Successed;
        //        Response.Data = Departments;
        //        #endregion

        //        return Ok(Response);
        //    }
        //    catch (NotFoundException ex)
        //    {
        //        #region Logging info

        //        _logger.ErrorInDetail($"newException GetProducts", correlationGuid,
        //            $"{nameof(ShopController)}_{nameof(GetProducts)}_{nameof(NotFoundException)}",
        //            ex, 1, User.Identity.Name);

        //        #endregion Logging info
        //        #region Result
        //        Response.Result = EnumResponseResult.Failed;
        //        Response.Data = null;
        //        Response.Errors.Add(ex.Message);
        //        #endregion

        //        return NotFound(Response);
        //    }
        //    catch (Exception ex)
        //    {
        //        #region Logging info

        //        _logger.ErrorInDetail($"newException GetProducts", correlationGuid,
        //            $"{nameof(ShopController)}_{nameof(GetProducts)}_{nameof(Exception)}", ex, 1, User.Identity.Name);

        //        #endregion Logging info
        //        #region Result
        //        Response.Result = EnumResponseResult.Failed;
        //        Response.Data = null;

        //        Response.Errors.Add(ex.Message);
        //        #endregion

        //        return BadRequest(Response);
        //    }
        //}

        //[HttpGet]
        //[Route(nameof(GetProduct))]
        //public async Task<ActionResult> GetProduct(Guid Id)
        //{
        //    var Response = new ElBaytResponse<ProductDTO>
        //    {
        //        Errors = new List<string>()
        //    };
        //    var correlationGuid = Guid.NewGuid();
        //    try
        //    {

        //        #region Logging info
        //        _logger.InfoInDetail(Id, correlationGuid, nameof(ShopController), nameof(GetProduct), 1, User.Identity.Name);
        //        #endregion Logging info

        //        var Product = await _elBaytServices.ProductService.GetProduct(Id);
        //        #region Result
        //        Response.Result = EnumResponseResult.Successed;
        //        Response.Data = Product;
        //        #endregion

        //        return Ok(Response);
        //    }
        //    catch (NotFoundException ex)
        //    {
        //        #region Logging info

        //        _logger.ErrorInDetail($"newException {Id}", correlationGuid,
        //            $"{nameof(ShopController)}_{nameof(GetProduct)}_{nameof(NotFoundException)}",
        //            ex, 1, User.Identity.Name);

        //        #endregion Logging info
        //        #region Result
        //        Response.Result = EnumResponseResult.Failed;
        //        Response.Data = null;
        //        Response.Errors.Add(ex.Message);
        //        #endregion

        //        return NotFound(Response);
        //    }
        //    catch (Exception ex)
        //    {
        //        #region Logging info

        //        _logger.ErrorInDetail($"newException {Id}", correlationGuid,
        //            $"{nameof(ShopController)}_{nameof(GetProduct)}_{nameof(Exception)}", ex, 1, User.Identity.Name);

        //        #endregion Logging info
        //        #region Result
        //        Response.Result = EnumResponseResult.Failed;
        //        Response.Data = null;

        //        Response.Errors.Add(ex.Message);
        //        #endregion

        //        return BadRequest(Response);
        //    }
        //}

        //[HttpGet]
        //[Route(nameof(GetProductImages))]
        //public ActionResult GetProductImages(Guid ProductId)
        //{
        //    var Response = new ElBaytResponse<object>
        //    {
        //        Errors = new List<string>()
        //    };
        //    var correlationGuid = Guid.NewGuid();
        //    try
        //    {

        //        #region Logging info
        //        _logger.InfoInDetail(ProductId, correlationGuid, nameof(ShopController), nameof(GetProductImages), 1, User.Identity.Name);
        //        #endregion Logging info

        //        var Images = _elBaytServices.ProductService.GetProductImages(ProductId);
        //        #region Result
        //        Response.Result = EnumResponseResult.Successed;
        //        Response.Data = Images;
        //        #endregion

        //        return Ok(Response);
        //    }
        //    catch (NotFoundException ex)
        //    {
        //        #region Logging info

        //        _logger.ErrorInDetail($"newException GetProducts", correlationGuid,
        //            $"{nameof(ShopController)}_{nameof(GetProductImages)}_{nameof(NotFoundException)}",
        //            ex, 1, User.Identity.Name);

        //        #endregion Logging info
        //        #region Result
        //        Response.Result = EnumResponseResult.Failed;
        //        Response.Data = null;
        //        Response.Errors.Add(ex.Message);
        //        #endregion

        //        return NotFound(Response);
        //    }
        //    catch (Exception ex)
        //    {
        //        #region Logging info

        //        _logger.ErrorInDetail($"newException {ProductId}", correlationGuid,
        //            $"{nameof(ShopController)}_{nameof(GetProductImages)}_{nameof(Exception)}", ex, 1, User.Identity.Name);

        //        #endregion Logging info
        //        #region Result
        //        Response.Result = EnumResponseResult.Failed;
        //        Response.Data = null;

        //        Response.Errors.Add(ex.Message);
        //        #endregion

        //        return BadRequest(Response);
        //    }
        //}

        //#endregion

        //#region Categories

        //[HttpGet]
        //[Route(nameof(GetProductCategories))]
        //public ActionResult GetProductCategories()
        //{
        //    var Response = new ElBaytResponse<object>
        //    {
        //        Errors = new List<string>()
        //    };
        //    var correlationGuid = Guid.NewGuid();
        //    try
        //    {

        //        #region Logging info
        //        _logger.InfoInDetail("GetAll", correlationGuid, nameof(ShopController), nameof(GetProductCategories), 1, User.Identity.Name);
        //        #endregion Logging info

        //        var Categories = _elBaytServices.ProductService.GetProductCategories();
        //        #region Result
        //        Response.Result = EnumResponseResult.Successed;
        //        Response.Data = Categories;
        //        #endregion

        //        return Ok(Response);
        //    }
        //    catch (NotFoundException ex)
        //    {
        //        #region Logging info

        //        _logger.ErrorInDetail($"newException GetProductCategories", correlationGuid,
        //            $"{nameof(ShopController)}_{nameof(GetProductCategories)}_{nameof(NotFoundException)}",
        //            ex, 1, User.Identity.Name);

        //        #endregion Logging info
        //        #region Result
        //        Response.Result = EnumResponseResult.Failed;
        //        Response.Data = null;
        //        Response.Errors.Add(ex.Message);
        //        #endregion

        //        return NotFound(Response);
        //    }
        //    catch (Exception ex)
        //    {
        //        #region Logging info

        //        _logger.ErrorInDetail($"newException GetProductCategories", correlationGuid,
        //            $"{nameof(ShopController)}_{nameof(GetProductCategories)}_{nameof(Exception)}", ex, 1, User.Identity.Name);

        //        #endregion Logging info
        //        #region Result
        //        Response.Result = EnumResponseResult.Failed;
        //        Response.Data = null;

        //        Response.Errors.Add(ex.Message);
        //        #endregion

        //        return BadRequest(Response);
        //    }
        //}

        //[HttpGet]
        //[Route(nameof(GetProductCategory))]
        //public async Task<ActionResult> GetProductCategory(Guid Id)
        //{
        //    var Response = new ElBaytResponse<ProductCategoryDTO>
        //    {
        //        Errors = new List<string>()
        //    };
        //    var correlationGuid = Guid.NewGuid();
        //    try
        //    {

        //        #region Logging info
        //        _logger.InfoInDetail(Id, correlationGuid, nameof(ShopController), nameof(GetProductCategory), 1, User.Identity.Name);
        //        #endregion Logging info

        //        var Category = await _elBaytServices.ProductService.GetProductCategory(Id);
        //        #region Result
        //        Response.Result = EnumResponseResult.Successed;
        //        Response.Data = Category;
        //        #endregion

        //        return Ok(Response);
        //    }
        //    catch (NotFoundException ex)
        //    {
        //        #region Logging info

        //        _logger.ErrorInDetail($"newException {Id}", correlationGuid,
        //            $"{nameof(ShopController)}_{nameof(GetProductCategory)}_{nameof(NotFoundException)}",
        //            ex, 1, User.Identity.Name);

        //        #endregion Logging info
        //        #region Result
        //        Response.Result = EnumResponseResult.Failed;
        //        Response.Data = null;
        //        Response.Errors.Add(ex.Message);
        //        #endregion

        //        return NotFound(Response);
        //    }
        //    catch (Exception ex)
        //    {
        //        #region Logging info

        //        _logger.ErrorInDetail($"newException {Id}", correlationGuid,
        //            $"{nameof(ShopController)}_{nameof(GetProductCategory)}_{nameof(Exception)}", ex, 1, User.Identity.Name);

        //        #endregion Logging info
        //        #region Result
        //        Response.Result = EnumResponseResult.Failed;
        //        Response.Data = null;

        //        Response.Errors.Add(ex.Message);
        //        #endregion

        //        return BadRequest(Response);
        //    }
        //}

        //#endregion

        //#region Types

        //[HttpGet]
        //[Route(nameof(GetProductTypes))]
        //public ActionResult GetProductTypes()
        //{
        //    var Response = new ElBaytResponse<object>
        //    {
        //        Errors = new List<string>()
        //    };
        //    var correlationGuid = Guid.NewGuid();
        //    try
        //    {

        //        #region Logging info
        //        _logger.InfoInDetail("GetAll", correlationGuid, nameof(ShopController), nameof(GetProductTypes), 1, User.Identity.Name);
        //        #endregion Logging info

        //        var Types = _elBaytServices.ProductService.GetProductTypes();
        //        #region Result
        //        Response.Result = EnumResponseResult.Successed;
        //        Response.Data = Types;
        //        #endregion

        //        return Ok(Response);
        //    }
        //    catch (NotFoundException ex)
        //    {
        //        #region Logging info

        //        _logger.ErrorInDetail($"newException GetProductTypes", correlationGuid,
        //            $"{nameof(ShopController)}_{nameof(GetProductTypes)}_{nameof(NotFoundException)}",
        //            ex, 1, User.Identity.Name);

        //        #endregion Logging info
        //        #region Result
        //        Response.Result = EnumResponseResult.Failed;
        //        Response.Data = null;
        //        Response.Errors.Add(ex.Message);
        //        #endregion

        //        return NotFound(Response);
        //    }
        //    catch (Exception ex)
        //    {
        //        #region Logging info

        //        _logger.ErrorInDetail($"newException GetProductTypes", correlationGuid,
        //            $"{nameof(ShopController)}_{nameof(GetProductTypes)}_{nameof(Exception)}", ex, 1, User.Identity.Name);

        //        #endregion Logging info
        //        #region Result
        //        Response.Result = EnumResponseResult.Failed;
        //        Response.Data = null;

        //        Response.Errors.Add(ex.Message);
        //        #endregion

        //        return BadRequest(Response);
        //    }
        //}

        //[HttpGet]
        //[Route(nameof(GetProductType))]
        //public async Task<ActionResult> GetProductType(Guid Id)
        //{
        //    var Response = new ElBaytResponse<ProductTypeDTO>
        //    {
        //        Errors = new List<string>()
        //    };
        //    var correlationGuid = Guid.NewGuid();
        //    try
        //    {

        //        #region Logging info
        //        _logger.InfoInDetail(Id, correlationGuid, nameof(ShopController), nameof(GetProductType), 1, User.Identity.Name);
        //        #endregion Logging info

        //        var Type = await _elBaytServices.ProductService.GetProductType(Id);
        //        #region Result
        //        Response.Result = EnumResponseResult.Successed;
        //        Response.Data = Type;
        //        #endregion

        //        return Ok(Response);
        //    }
        //    catch (NotFoundException ex)
        //    {
        //        #region Logging info

        //        _logger.ErrorInDetail($"newException {Id}", correlationGuid,
        //            $"{nameof(ShopController)}_{nameof(GetProductType)}_{nameof(NotFoundException)}",
        //            ex, 1, User.Identity.Name);

        //        #endregion Logging info
        //        #region Result
        //        Response.Result = EnumResponseResult.Failed;
        //        Response.Data = null;
        //        Response.Errors.Add(ex.Message);
        //        #endregion

        //        return NotFound(Response);
        //    }
        //    catch (Exception ex)
        //    {
        //        #region Logging info

        //        _logger.ErrorInDetail($"newException {Id}", correlationGuid,
        //            $"{nameof(ShopController)}_{nameof(GetProductType)}_{nameof(Exception)}", ex, 1, User.Identity.Name);

        //        #endregion Logging info
        //        #region Result
        //        Response.Result = EnumResponseResult.Failed;
        //        Response.Data = null;

        //        Response.Errors.Add(ex.Message);
        //        #endregion

        //        return BadRequest(Response);
        //    }
        //}


        //#endregion

        //#region Departments

        //[HttpGet]
        //[Route(nameof(GetProductDepartments))]
        //public ActionResult GetProductDepartments()
        //{

        //    var Response = new ElBaytResponse<object>
        //    {
        //        Errors = new List<string>()
        //    };
        //    var correlationGuid = Guid.NewGuid();
        //    try
        //    {

        //        #region Logging info

        //        _logger.InfoInDetail("GetAll", correlationGuid, nameof(ShopController), nameof(GetProductDepartments), 1, User.Identity.Name);
        //        #endregion Logging info

        //        var Departments = _elBaytServices.ProductService.GetProductDepartments();

        //        #region Result
        //        Response.Result = EnumResponseResult.Successed;
        //        Response.Data = Departments;
        //        #endregion

        //        return Ok(Response);
        //    }
        //    catch (NotFoundException ex)
        //    {
        //        #region Logging info

        //        _logger.ErrorInDetail($"newException GetProductDepartments", correlationGuid,
        //            $"{nameof(ShopController)}_{nameof(GetProductDepartments)}_{nameof(NotFoundException)}",
        //            ex, 1, User.Identity.Name);

        //        #endregion Logging info
        //        #region Result
        //        Response.Result = EnumResponseResult.Failed;
        //        Response.Data = null;
        //        Response.Errors.Add(ex.Message);
        //        #endregion

        //        return NotFound(Response);
        //    }
        //    catch (Exception ex)
        //    {
        //        #region Logging info

        //        _logger.ErrorInDetail($"newException GetProductDepartments", correlationGuid,
        //            $"{nameof(ShopController)}_{nameof(GetProductDepartments)}_{nameof(Exception)}", ex, 1, User.Identity.Name);

        //        #endregion Logging info
        //        #region Result
        //        Response.Result = EnumResponseResult.Failed;
        //        Response.Data = null;

        //        Response.Errors.Add(ex.Message);
        //        #endregion

        //        return BadRequest(Response);
        //    }
        //}

        //[HttpGet]
        //[Route(nameof(GetProductDepartment))]
        //public async Task<ActionResult> GetProductDepartment(Guid Id)
        //{
        //    var Response = new ElBaytResponse<ProductDepartmentDTO>
        //    {
        //        Errors = new List<string>()
        //    };
        //    var correlationGuid = Guid.NewGuid();
        //    try
        //    {

        //        #region Logging info
        //        _logger.InfoInDetail(Id, correlationGuid, nameof(ShopController), nameof(GetProductDepartment), 1, User.Identity.Name);
        //        #endregion Logging info

        //        var Department = await _elBaytServices.ProductService.GetProductDepartment(Id);
        //        #region Result
        //        Response.Result = EnumResponseResult.Successed;
        //        Response.Data = Department;
        //        #endregion

        //        return Ok(Response);
        //    }
        //    catch (NotFoundException ex)
        //    {
        //        #region Logging info

        //        _logger.ErrorInDetail($"newException {Id}", correlationGuid,
        //            $"{nameof(ShopController)}_{nameof(GetProductDepartment)}_{nameof(NotFoundException)}",
        //            ex, 1, User.Identity.Name);

        //        #endregion Logging info
        //        #region Result
        //        Response.Result = EnumResponseResult.Failed;
        //        Response.Data = null;
        //        Response.Errors.Add(ex.Message);
        //        #endregion

        //        return NotFound(Response);
        //    }
        //    catch (Exception ex)
        //    {
        //        #region Logging info

        //        _logger.ErrorInDetail($"newException {Id}", correlationGuid,
        //            $"{nameof(ShopController)}_{nameof(GetProductDepartment)}_{nameof(Exception)}", ex, 1, User.Identity.Name);

        //        #endregion Logging info
        //        #region Result
        //        Response.Result = EnumResponseResult.Failed;
        //        Response.Data = null;

        //        Response.Errors.Add(ex.Message);
        //        #endregion

        //        return BadRequest(Response);
        //    }
        //}


        //#endregion

        [HttpGet]
        [Route(nameof(GetShopData))]
        public async Task<ActionResult> GetShopData(Guid DepartmentId)
        {
            var Response = new ElBaytResponse<ShopDataDTO>
            {
                Errors = new List<string>()
            };
            var correlationGuid = Guid.NewGuid();
            try
            {

                #region Logging info
                _logger.InfoInDetail(DepartmentId, correlationGuid, nameof(ShopController), nameof(GetShopData), 1, User.Identity.Name);
                #endregion Logging info

                var ShopData = await _elBaytServices.ShopService.GetShopData(DepartmentId);
                #region Result
                Response.Result = EnumResponseResult.Successed;
                Response.Data = ShopData;
                #endregion

                return Ok(Response);
            }
            catch (NotFoundException ex)
            {
                #region Logging info

                _logger.ErrorInDetail($"newException {DepartmentId}", correlationGuid,
                    $"{nameof(ShopController)}_{nameof(GetShopData)}_{nameof(NotFoundException)}",
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

                _logger.ErrorInDetail($"newException {DepartmentId}", correlationGuid,
                    $"{nameof(ShopController)}_{nameof(GetShopData)}_{nameof(Exception)}", ex, 1, User.Identity.Name);

                #endregion Logging info
                #region Result
                Response.Result = EnumResponseResult.Failed;
                Response.Data = null;

                Response.Errors.Add(ex.Message);
                #endregion

                return BadRequest(Response);
            }
        }
    }
}
