using ElBayt.DTO.ELBayt.DBDTOs;
using ElBayt.DTO.ELBayt.DTOs;
using System;
using System.Collections.Generic;

namespace ElBayt.Core.Mapping
{
    public interface IShopMapper
    {
        ShopProductDTO MapProductToShopProduct(ProductDataDTO product);
        List<ShopProductDTO> MapProductsToShopProducts(List<ProductDataDTO> products);
        Tuple<int, ShopProductDTO> MapNoProductToShopProduct(List<NumberProductDTO> product);
        Tuple<List<int>, List<ShopProductDTO>> MapNoProductsToShopProducts(List<NumberProductDTO> products);
    }
}