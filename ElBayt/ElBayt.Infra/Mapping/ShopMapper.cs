using ElBayt.Core.Mapping;
using ElBayt.DTO.ELBayt.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;

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
                    variants = new List<ShopProductVariantDTO>(),
                    ratings = product.Ratings,
                    stock = product.Quantity,
                }; ;
                Shopproducts.Add(Shopproduct);
                Rows.Add(product.RowNum);
            }
            return new Tuple<List<int>, List<ShopProductDTO>>(Rows, Shopproducts);
        }

        public Tuple<int, ShopProductDTO> MapNoProductToShopProduct(List<NumberProductDTO> products)
        {


            var Rownumber = products[0].RowNum;
            var _product = new ShopProductDTO
            {
                Id = products[0].Id.ToString(),
                Name = products[0].Name,
                Description = products[0].Description,
                Price = Convert.ToDecimal(products[0].Price),
                ProductCategoryId = products[0].ProductCategoryId.ToString(),
                category = new List<ShopProductCategoryDTO> { new ShopProductCategoryDTO { name = products[0].ProductCategoryName } },
                sale_price = Convert.ToDecimal(products[0].PriceAfterDiscount),
                variants = new List<ShopProductVariantDTO>(),
                ratings = products[0].Ratings,
                stock = products[0].Quantity,                
            };
            _product.variants = new List<ShopProductVariantDTO>();
            _product.category = new List<ShopProductCategoryDTO>
            {
                new ShopProductCategoryDTO { name = products[0].ProductCategoryName }
            };
            var Colors = products.Select(c => c.ColorName).Distinct();
           
            foreach (var Color in Colors) 
            {
                var variant = new ShopProductVariantDTO
                {
                    color= "#669933",
                    color_name = Color,
                    price = Convert.ToDecimal(products[0].Price),
                };
                var Sizes = new List<ShopProductVariantSizeDTO>();
                var ColorProducts = products.Where(c => c.ColorName == Color.ToString());
                foreach (var ColorProduct in ColorProducts) 
                {
                    Sizes.Add(new ShopProductVariantSizeDTO { name= ColorProduct.SizeName });
                }
                variant.size = Sizes;
                _product.variants.Add(variant);
            }

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
                variants = new List<ShopProductVariantDTO>(),
                ratings = product.Ratings,
                stock = product.Quantity,
            };
        }
    }
}
