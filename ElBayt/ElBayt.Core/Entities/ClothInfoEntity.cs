using ElBayt.Common.Common;
using ElBayt.Common.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ElBayt.Core.Entities
{
    public class ClothInfoEntity : EnhancedEntity<Guid>, BaseEntity
    {
        [Required]
        public Guid ClothId { get; set; }
        [Required]
        public decimal Price { get; set; }
        public decimal? PriceAfterDiscount { get; set; }
        [Required]
        public Guid SizeId { get; set; }

        public Guid? ColorId { get; set; }

        [Required]
        public int Amount { get; set; }
        public Guid? BrandId { get; set; }


        public virtual ClothBrandEntity Brands { get; set; }
        public virtual ClothEntity Clothes { get; set; }
        public virtual ClothSizeEntity Sizes { get; set; }
        public virtual ColorEntity Colors { get; set; }
    }
}
