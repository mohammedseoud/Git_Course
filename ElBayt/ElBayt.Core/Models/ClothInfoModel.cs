using ElBayt.Common.Infra.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElBayt.Core.Models
{
    [Table("ClothInfo", Schema = "dbo")]
    public class ClothInfoModel : BaseModel<int>
    {
        [Required]
        [ForeignKey(nameof(Clothes))]
        public int ClothId { get; set; }
        [Required]
        public decimal Price { get; set; }
        public decimal? PriceAfterDiscount { get; set; }
        [Required]
        [ForeignKey(nameof(Sizes))]
        public int SizeId { get; set; }
     
        [ForeignKey(nameof(Colors))]
        public int? ColorId { get; set; }

        [Required]
        public int Amount { get; set; }
        [ForeignKey(nameof(Brands))]
        public int? BrandId { get; set; }

      
        public virtual ClothBrandModel Brands { get; set; }
        public virtual ClothModel Clothes { get; set; }
        public virtual ClothSizeModel Sizes { get; set; }
        public virtual ColorModel Colors { get; set; }

    }
}
