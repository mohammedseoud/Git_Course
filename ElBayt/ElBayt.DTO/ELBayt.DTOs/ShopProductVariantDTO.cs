using System;
using System.Collections.Generic;
using System.Text;

namespace ElBayt.DTO.ELBayt.DTOs
{
    public class ShopProductVariantDTO
    {
        public string color { set; get; }
        public string color_name { set; get; }
        public decimal? price { set; get; }
        public List<ShopProductVariantSizeDTO> size { set; get; }
    }
}
