using ElBayt.Common.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElBayt.DTO.ELBayt.DTOs
{
    public class NumberProductDTO 
    { 
        public int RowNum { get; set; }
        public int Id { set; get; }
        public string Name { set; get; }
        public string Price { set; get; }
        public string PriceAfterDiscount { get; set; }
        public string Description { get; set; }
        public string ProductImageURL1 { get; set; }
        public string ProductImageURL2 { get; set; }
        public int ProductCategoryId { get; set; }
        public string SizeName { get; set; }
        public string ColorName { get; set; }
        public int Quantity { get; set; }
        public int Reviews { get; set; }
        public decimal Ratings { get; set; }
        public string ProductCategoryName { get; set; }

    }
}
