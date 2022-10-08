using ElBayt.Common.Common;
using ElBayt.Common.Infra.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ElBayt.Core.Models
{
    [Table("ClothBrands", Schema = "dbo")]
    public class ClothBrandsModel :  BaseModel<Guid>
    {
        [Required]
        [ForeignKey(nameof(Clothes))]
        public Guid ClothId { get; set; }
        [Required]
        [ForeignKey(nameof(ClothBrands))]
        public Guid BrandId { get; set; }
        public virtual ClothBrandModel ClothBrands { get; set; }
        public virtual ClothModel Clothes { get; set; }

    }
}
