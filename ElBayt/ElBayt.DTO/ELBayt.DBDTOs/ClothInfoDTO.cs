using ElBayt.Common.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElBayt.DTO.ELBayt.DBDTOs
{
    public class ClothInfoDTO : BaseDto<Guid>, IBaseDTO
    {
        public Guid ClothId { get; set; }
        public string Price { get; set; }
        public string PriceAfterDiscount { get; set; }
        public Guid SizeId { get; set; }
        public string ColorId { get; set; }
        public string Amount { get; set; }
        public string BrandId { get; set; }


        public virtual ClothBrandDTO Brands { get; set; }
        public virtual ClothDTO Clothes { get; set; }
        public virtual ClothSizeDTO Sizes { get; set; }
        public virtual ColorDTO Colors { get; set; }
    }
}
