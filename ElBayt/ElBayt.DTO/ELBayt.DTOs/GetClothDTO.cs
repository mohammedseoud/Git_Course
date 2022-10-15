using ElBayt.Common.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElBayt.DTO.ELBayt.DBDTOs
{
    public class GetClothDTO : BasicDto<int>
    {
        public int ClothCategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ProductImageURL1 { get; set; }
        public string ProductImageURL2 { get; set; }
        public int Quantity { get; set; }
        public int Reviews { get; set; }
        public decimal Ratings { get; set; }
    }
}
