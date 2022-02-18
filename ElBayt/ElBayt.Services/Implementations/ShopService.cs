using Dapper;
using ElBayt.Common.Core.Logging;
using ElBayt.Common.Core.Mapping;
using ElBayt.Common.Core.SecurityModels;
using ElBayt.Core.Entities;
using ElBayt.Core.IUnitOfWork;
using ElBayt.DTO.ELBayt.DBDTOs;
using ElBayt.DTO.ELBayt.DTOs;
using ElBayt.Infra.SPs;
using ElBayt.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElBayt.Services.Implementations
{
    public class ShopService : IShopService
    {
        private readonly IELBaytUnitOfWork _unitOfWork;
        private readonly IUserIdentity _userIdentity;
        private readonly ILogger _logger;
        private readonly ITypeMapper _mapper;

        public ShopService(IELBaytUnitOfWork unitOfWork, IUserIdentity userIdentity, ILogger logger,
              ITypeMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _userIdentity = userIdentity ?? throw new ArgumentNullException(nameof(userIdentity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        public async Task<ShopDataDTO> GetShopData(string DepartmentName)
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info

                _logger.InfoInDetail(DepartmentName, correlationGuid, nameof(ShopService), nameof(GetShopData), 1, _userIdentity.Name);

                #endregion Logging info
                var ShopData = new ShopDataDTO();

                var SPParameters = new DynamicParameters();
                SPParameters.Add("@ProductDepartmentName", DepartmentName);

                var AllShopData = await _unitOfWork.SP.MultiListAsnyc<ProductTypesDataDTO, ShopCategoryDTO, ProductDataDTO>(StoredProcedure.GETSHOPDATA, SPParameters);
                ShopData.ProductTypes = AllShopData.Item1.ToList();
                ShopData.ProductCategories = AllShopData.Item2.ToList();
                var products = AllShopData.Item3.ToList();
                var Shopproducts = new List<ShopProductDTO>();
                foreach (var product in products)
                {
                    var Shopproduct = new ShopProductDTO
                    {
                        Id = product.Id.ToString(),
                        Name = product.Name,
                        Description = product.Description,
                        Price = Convert.ToDecimal(product.Price),
                        ProductCategoryId = product.ProductCategoryId.ToString(),
                        category = new List<ShopProductCategoryDTO> { new ShopProductCategoryDTO { name = product.ProductCategoryName } },
                        sale_price = Convert.ToDecimal(product.PriceAfterDiscount),
                        pictures = new List<ShopProductImageDTO> { new ShopProductImageDTO { url = product.ProductImageURL1 }, new ShopProductImageDTO { url = product.ProductImageURL2 } },
                        sm_pictures = new List<ShopProductImageDTO> { new ShopProductImageDTO { url = product.ProductImageURL1 }, new ShopProductImageDTO { url = product.ProductImageURL2 } },
                        variants = new List<ShopProductVariantSizeDTO>(),
                        ratings = 5,
                    };
                    Shopproducts.Add(Shopproduct);
                }
                ShopData.Products = Shopproducts;
                return ShopData;
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail(DepartmentName, correlationGuid, $"{nameof(ShopService)}_{nameof(GetShopData)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

                #endregion Logging info

                throw;
            }
        }


    }
}
