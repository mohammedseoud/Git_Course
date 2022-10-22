using ElBayt.Common.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElBayt.DTO.ELBayt.DBDTOs
{
    public class ClothInfoDTO : BasicDto<int>, IBaseDTO
    {
        public int ClothId { get; set; }
        public string Price { get; set; }
        public string PriceAfterDiscount { get; set; }
        public int SizeId { get; set; }
        public int ColorId { get; set; }
        public string Amount { get; set; }
        public int BrandId { get; set; }
    }
}
