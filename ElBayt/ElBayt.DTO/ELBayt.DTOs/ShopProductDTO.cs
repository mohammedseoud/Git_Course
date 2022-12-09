using System;
using System.Collections.Generic;
using System.Text;

namespace ElBayt.DTO.ELBayt.DTOs
{
    public class ShopProductDTO
    {
        public string Id { set; get; }
        public string Name { set; get; }
        public decimal Price { set; get; }
        public string Description { set; get; }
        public int? stock { set; get; }
        public string ProductCategoryId { set; get; }
        public string slug { set; get; }
        public int? review { set; get; }
        public decimal? ratings { set; get; }
        public bool? New { set; get; }
        public bool? top { set; get; }
        public decimal? sale_price { set; get; }
        public string until { set; get; }
        public bool short_desc { set; get; }
        public List<ShopProductVariantDTO> variants{ set; get; }
        public List<ShopProductCategoryDTO> category { set; get; }
        public List<ShopProductImageDTO> pictures { set; get; }
        public List<ShopProductImageDTO> sm_pictures { set; get; }

    }
}
