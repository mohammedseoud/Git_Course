using ElBayt.Common.Infra.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElBayt.Core.Models
{
    [Table("ClothCategoryBrands", Schema = "dbo")]
    public class ClothCategoryBrandsModel :  BaseModel<int>
    {
        [Required]
        [ForeignKey(nameof(ClothCategories))]
        public int ClothCategoryId { get; set; }
        [Required]
        [ForeignKey(nameof(ClothBrands))]
        public int BrandId { get; set; }
        public virtual ClothBrandModel ClothBrands { get; set; }
        public virtual ClothCategoryModel ClothCategories { get; set; }

    }
}
