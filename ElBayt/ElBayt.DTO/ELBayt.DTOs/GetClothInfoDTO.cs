using ElBayt.Common.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElBayt.DTO.ELBayt.DTOs
{
    public class GetClothInfoDTO : BasicDto<int>
    {
        public int ClothId { get; set; }
        public decimal Price { get; set; }
        public decimal PriceAfterDiscount { get; set; }
        public int SizeId { get; set; }
        public string ColorId { get; set; }
        public int Amount { get; set; }
        public string BrandId { get; set; }

    }
}
