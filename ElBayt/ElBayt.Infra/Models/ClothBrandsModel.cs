using ElBayt.Common.Common;
using ElBayt.Common.Infra.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ElBayt.Infra.Models
{
    [Table("ClothBrands", Schema = "dbo")]
    public class ClothBrandsModel : BaseModel<Guid>
    {
        [Required]
        [ForeignKey(nameof(Clothes))]
        public Guid ClothId { get; set; }
     
        [Required]
        [ForeignKey(nameof(Brands))]
        public Guid BrandId { get; set; }

        [Required]
        public int Amount { get; set; }

        public virtual ClothModel Clothes { get; set; }
        public virtual ClothBrandModel Brands { get; set; }
    }
}
