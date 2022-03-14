using ElBayt.Common.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElBayt.Common.Core.Services
{
    public class ProductDTO : BaseDto<Guid>, IBaseDTO
    { 
        public string Name { get; set; }
        public string Price { get; set; }
        public string PriceAfterDiscount { get; set; }
        public string Description { get; set; }
        public string ProductImageURL1 { get; set; }
        public string ProductImageURL2 { get; set; }
        public int Quantity { get; set; }
        public int Reviews { get; set; }
        public decimal Ratings { get; set; }

    }
}
