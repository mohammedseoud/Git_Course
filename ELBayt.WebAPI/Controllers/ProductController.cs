using ElBayt.Common.Common;
using ElBayt.Common.Enums;
using ElBayt.Common.Core.Logging;
using ElBayt.Common.Security;
using ElBayt.DTO.ELBaytDTO_s;
using ElBayt.Services.IElBaytServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using ElBayt.Common.Core.Services;


namespace ElBayt_ECommerce.WebAPI.Controllers
{
    [EnableCors(CorsOrigin.LOCAL_ORIGIN)]
    [ApiController]
    [Route("api/v1.0/ElBayt/Product")]
    public class ProductController : Controller
    {
        private readonly IElBaytServices _elBaytServices;
        private readonly ILogger _logger;
      
        public ProductController(IElBaytServices elBaytServices, ILogger logger)
        {
            _elBaytServices = elBaytServices ?? throw new ArgumentNullException(nameof(elBaytServices));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

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

                var Departments =  _elBaytServices.ProductService.GetProductDepartments();
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
    }
}
