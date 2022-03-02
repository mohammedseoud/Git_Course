using System;
using System.Collections.Generic;
using System.Text;

namespace ElBayt.DTO.ELBayt.DTOs
{
    public class ShopProductInfoDTO
    {
        public ShopProductDTO current { set; get; }
        public ShopProductDTO next { set; get; }
        public ShopProductDTO prev { set; get; }
        public List<ShopProductDTO> related { set; get; }
    }
}
