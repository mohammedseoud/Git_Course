using ElBayt.Common.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElBayt.DTO.ELBayt.DBUDTDTOs
{
    public class UTDProductDTO : BaseDto<Guid>, IBaseDTO
    {
        public string Name { get; set; }
        public string Price { get; set; }
        public string PriceAfterDiscount { get; set; }
        public string Description { get; set; }
        public Guid ProductCategoryId { get; set; }
        public string ProductImageURL1 { get; set; }
        public string ProductImageURL2 { get; set; }

    }
}
