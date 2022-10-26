using ElBayt.Common.Infra.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElBayt.Core.Models
{
    [Table("ClothCategorySizes", Schema = "dbo")]
    public class ClothCategorySizesModel :  BaseModel<int>
    {
        [Required]
        [ForeignKey(nameof(ClothCategories))]
        public int ClothCategoryId { get; set; }
        [Required]
        [ForeignKey(nameof(ClothSizes))]
        public int SizeId { get; set; }
        public virtual ClothSizeModel ClothSizes { get; set; }
        public virtual ClothCategoryModel ClothCategories { get; set; }

    }
}
