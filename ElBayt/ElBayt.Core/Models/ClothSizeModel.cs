using ElBayt.Common.Infra.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElBayt.Core.Models
{
    [Table("ClothSize", Schema = "dbo")]
    public class ClothSizeModel : ProductSizeModel
    {
        [Required]
        [ForeignKey(nameof(Clothes))]
        public int ClothId { get; set; }
        public virtual ClothModel Clothes { get; set; }
    }
}
