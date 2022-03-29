using ElBayt.Common.Common;
using ElBayt.Common.Infra.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ElBayt.Infra.Models
{
    [Table("ClothInfo", Schema = "dbo")]
    public class ClothInfoModel : BaseModel<Guid>
    {
        [Required]
        [ForeignKey(nameof(Clothes))]
        public Guid ClothId { get; set; }
        [Required]
        public decimal Price { get; set; }
        public decimal? PriceAfterDiscount { get; set; }
        [Required]
        [ForeignKey(nameof(Sizes))]
        public Guid SizeId { get; set; }
     
        [ForeignKey(nameof(Colors))]
        public Guid ColorId { get; set; }

        [Required]
        public int Amount { get; set; }
        [ForeignKey(nameof(Brands))]
        public Guid BrandId { get; set; }

      
        public virtual ClothBrandModel Brands { get; set; }
        public virtual ClothModel Clothes { get; set; }
        public virtual ClothSizeModel Sizes { get; set; }
        public virtual ColorModel Colors { get; set; }

    }
}
