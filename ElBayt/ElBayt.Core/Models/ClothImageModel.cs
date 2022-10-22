using ElBayt.Common.Infra.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElBayt.Core.Models
{
    [Table("ClothImage", Schema = "dbo")]
    public class ClothImageModel : ProductImageModel
    {
        [Required]
        [ForeignKey(nameof(Cloths))]
        public int ClothId { get; set; }

        public virtual ClothModel Cloths { get; set; }
    }
}
