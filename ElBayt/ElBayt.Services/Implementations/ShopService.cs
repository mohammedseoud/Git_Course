using Dapper;
using ElBayt.Common.Core.Logging;
using ElBayt.Common.Core.Mapping;
using ElBayt.Common.Core.SecurityModels;
using ElBayt.Core.Entities;
using ElBayt.Core.IUnitOfWork;
using ElBayt.Core.Mapping;
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
        private readonly IShopMapper _shopmapper;

        public ShopService(IELBaytUnitOfWork unitOfWork, IUserIdentity userIdentity, ILogger logger,
              IShopMapper shopmapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _userIdentity = userIdentity ?? throw new ArgumentNullException(nameof(userIdentity));
            _shopmapper = shopmapper ?? throw new ArgumentNullException(nameof(shopmapper));
        }

        public async Task<ShopProductInfoDTO> GetProductData(string ProductName)
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info

                _logger.InfoInDetail(ProductName, correlationGuid, nameof(ShopService), nameof(GetProductData), 1, _userIdentity.Name);

                #endregion Logging info
                var ProductInfo = new ShopProductInfoDTO();

                var SPParameters = new DynamicParameters();
                SPParameters.Add("@ProductName", ProductName);

                var AllShopData = await _unitOfWork.SP.FourListAsnyc<NumberProductDTO,string, NumberProductDTO, ProductDataDTO>(StoredProcedure.GETPRODUCTDATA, SPParameters);
                var currentproduct = _shopmapper.MapNoProductToShopProduct(AllShopData.Item1.ToList().FirstOrDefault());


                ProductInfo.current = currentproduct.Item2;
                foreach (var url in AllShopData.Item2)
                {
                    ProductInfo.current.pictures.Add(new ShopProductImageDTO { url = url });
                    ProductInfo.current.sm_pictures.Add(new ShopProductImageDTO { url = url });
                }
                
                if (AllShopData.Item3.ToList().Count == 1)
                {
                    var _product = _shopmapper.MapNoProductToShopProduct(AllShopData.Item3.ToList().FirstOrDefault());
                    if (_product.Item1 > currentproduct.Item1)
                        ProductInfo.next = _product.Item2;
                    else
                        ProductInfo.prev = _product.Item2;
                }
                else if (AllShopData.Item3.ToList().Count == 2)
                {
                    var _products = _shopmapper.MapNoProductsToShopProducts(AllShopData.Item3.ToList());
                    var filteredproducts = _products.Item2.ToList();
                    ProductInfo.next = filteredproducts[1];
                    ProductInfo.prev = filteredproducts[0];
                }

                ProductInfo.related = _shopmapper.MapProductsToShopProducts(AllShopData.Item4.ToList());

                return ProductInfo;
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail(ProductName, correlationGuid, $"{nameof(ShopService)}_{nameof(GetProductData)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

                #endregion Logging info

                throw;
            }
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

                var AllShopData = await _unitOfWork.SP.ThreeListAsnyc<ProductTypesDataDTO, ShopCategoryDTO, ProductDataDTO>(StoredProcedure.GETSHOPDATA, SPParameters);
                ShopData.ProductTypes = AllShopData.Item1.ToList();
                ShopData.categories = AllShopData.Item2.ToList();
                var Shopproducts = _shopmapper.MapProductsToShopProducts(AllShopData.Item3.ToList());
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
