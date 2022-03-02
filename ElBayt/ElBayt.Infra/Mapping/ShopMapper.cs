using ElBayt.Core.Mapping;
using ElBayt.DTO.ELBayt.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElBayt.Infra.Mapping
{
    public class ShopMapper : IShopMapper
    {
        public ShopMapper() { }

        public Tuple<List<int>, List<ShopProductDTO>> MapNoProductsToShopProducts(List<NumberProductDTO> products)
        {
         //   var Shopproducts = new Tuple<List<int>, List<ShopProductDTO>>();

            var Shopproducts = new List<ShopProductDTO>();
            var Rows = new List<int>();

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
                    ratings = product.Ratings,
                    stock = product.Quantity,
                }; ;
                Shopproducts.Add(Shopproduct);
                Rows.Add(product.RowNum);
            }
            return new Tuple<List<int>, List<ShopProductDTO>>(Rows, Shopproducts);
        }

        public Tuple<int, ShopProductDTO> MapNoProductToShopProduct(NumberProductDTO product)
        {
             
            var Rownumber = product.RowNum;
            var _product= new ShopProductDTO
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
                ratings = product.Ratings,
                stock = product.Quantity,
            };
            return new Tuple<int, ShopProductDTO>(Rownumber, _product);
        }

        public List<ShopProductDTO> MapProductsToShopProducts(List<ProductDataDTO> products)
        {
            var Shopproducts = new List<ShopProductDTO>();
            foreach (var product in products)
            {
                var Shopproduct = MapProductToShopProduct(product);
                Shopproducts.Add(Shopproduct);
            }
            return Shopproducts;
        }

        public ShopProductDTO MapProductToShopProduct(ProductDataDTO product)
        {
            return new ShopProductDTO
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
                ratings = product.Ratings,
                stock = product.Quantity,
            };
        }
    }
}
