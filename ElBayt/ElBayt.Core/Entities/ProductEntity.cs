using ElBayt.Common.Common;
using ElBayt.Common.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ElBayt.Core.Entities
{
    public class ProductEntity : EnhancedEntity<Guid> , BaseEntity
    {
        [Required]
        [StringLength(General.SINGLE_LINE_MAX_LENGTH)]
        public string Name { get; set; }

         [Required]
         public decimal Price { get; set; }

        public decimal PriceAfterDiscout { get; set; }

        [StringLength(General.BIG_LINE_MAX_LENGTH)]
        [Required]
        public string Description { get; set; }
        
        public Guid ProductCategoryId { get; set; }
        
        public virtual ProductCategoryEntity ProductCategories { get; set; }
    }
}
