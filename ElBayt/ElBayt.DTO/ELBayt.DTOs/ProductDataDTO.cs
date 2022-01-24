using System;
using System.Collections.Generic;
using System.Text;

namespace ElBayt.DTO.ELBayt.DTOs
{
    public class ProductDataDTO
    {
        public Guid Id { set; get; }
        public string Name { set; get; }
        public string Price { set; get; }
        public string PriceAfterDiscount { get; set; }
        public string Description { get; set; }
        public string ProductImageURL1 { get; set; }
        public string ProductImageURL2 { get; set; }
        public Guid ProductCategoryId { get; set; }
        public string ProductCategoryName { get; set; }
    }
}
