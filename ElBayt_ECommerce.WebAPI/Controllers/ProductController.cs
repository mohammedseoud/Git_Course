using ElBayt.Common.Common;
using ElBayt.Common.Logging;
using ElBayt.Common.Security;
using ElBayt.DTO.ELBaytDTO_s;
using ElBayt.Services.IElBaytServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElBayt_ECommerce.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1.0/ElBayt/Product")]
    public class ProductController : Controller
    {
        private readonly IElBaytServices _elBaytServices;
        private readonly ILogger _logger;
        private readonly IUserIdentity _userIdentity;

        public ProductController(IElBaytServices elBaytServices, ILogger logger, IUserIdentity userIdentity)
        {
            _elBaytServices = elBaytServices ?? throw new ArgumentNullException(nameof(elBaytServices));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _userIdentity = userIdentity ?? throw new ArgumentNullException(nameof(userIdentity));
        }

        [HttpPost]
        [Route(nameof(AddNewProduct))]
        public async Task<IActionResult> AddNewProduct()
        {
            ProductDTO Product = new ProductDTO();
            var correlationGuid = Guid.NewGuid();
            try
            {

                #region Logging info
                _logger.InfoInDetail(Product, correlationGuid, nameof(ProductController), nameof(AddNewProduct), 1, _userIdentity.Name);
                #endregion Logging info



                await _elBaytServices.Product.AddNewProduct(Product);



                return Ok("Success In Adding" +
                    "");
            }
            catch (NotFoundException ex)
            {
                #region Logging info

                _logger.ErrorInDetail($"newException {Product}", correlationGuid, $"{nameof(ProductController)}_{nameof(AddNewProduct)}_{nameof(NotFoundException)}", ex, 1, _userIdentity.Name);

                #endregion Logging info

                return NotFound(ex);
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail($"newException {Product}", correlationGuid, $"{nameof(ProductController)}_{nameof(AddNewProduct)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

                #endregion Logging info

                return BadRequest(ex);
            }
        }
    }
}
