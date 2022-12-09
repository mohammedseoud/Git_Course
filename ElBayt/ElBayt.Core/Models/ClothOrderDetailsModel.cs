using ElBayt.Common.Common;
using ElBayt.Common.Infra.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElBayt.Core.Models
{
    [Table("ClothOrderDetails", Schema = "dbo")]
    public class ClothOrderDetailsModel : BaseModel<int>
    {
        [Required]
        public int Quantity { get; set; }
        [Required]
        [ForeignKey(nameof(Orders))]
        public int OrderId { get; set; }
        [Required]
        [ForeignKey(nameof(Clothes))]
        public int ClothtId { get; set; }
        [Required]
        [ForeignKey(nameof(ClothSizes))]
        public int SizeId { get; set; } 
        [ForeignKey(nameof(Colors))]
        public int ColorId { get; set; }
        [ForeignKey(nameof(Brands))]
        public int BrandId { get; set; }
        [Required]
        public decimal Price { get; set; }
        [StringLength(General.BIG_LINE_MAX_LENGTH)]
        public string NoteOnProduct { get; set; }

        public virtual ClothOrderModel Orders { get; set; }
        public virtual ClothModel Clothes { get; set; }
        public virtual ClothSizeModel ClothSizes { get; set; }
        public virtual ColorModel Colors { get; set; }
        public virtual ClothBrandModel Brands { get; set; }
    }
}
