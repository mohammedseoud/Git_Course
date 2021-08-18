using ElBayt.Common.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElBayt.DTO.ELBaytDTO_s
{
    public class ProductDTO : BaseDto<Guid>, IBaseDTO
    { 
        public string Name { get; set; }
        public decimal Price { get; set; }      
        public string Description { get; set; }
        public Guid ProductCategoryId { get; set; }

        public virtual ProductCategoryDTO ProductCategories { get; set; }
    }
}
