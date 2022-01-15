using System;
using System.Collections.Generic;
using System.Text;

namespace ElBayt.DTO.ELBayt.DTOs
{
    public class ShopDataDTO
    {
        public List<ProductTypesDataDTO> ProductTypes { set; get; }
        public List<ShopCategoryDTO> ProductCategories { set; get; }
    }
}
