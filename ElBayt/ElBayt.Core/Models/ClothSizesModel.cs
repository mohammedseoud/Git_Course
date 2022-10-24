using ElBayt.Common.Infra.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElBayt.Core.Models
{
    [Table("ClothSizes", Schema = "dbo")]
    public class ClothSizesModel :  BaseModel<int>
    {
        [Required]
        [ForeignKey(nameof(Clothes))]
        public int ClothId { get; set; }
        [Required]
        [ForeignKey(nameof(ClothSizes))]
        public int SizeId { get; set; }
        public virtual ClothSizeModel ClothSizes { get; set; }
        public virtual ClothModel Clothes { get; set; }

    }
}
