using ElBayt.Common.Common;
using ElBayt.Common.Infra.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ElBayt.Infra.Models
{
    [Table("ClothSizes", Schema = "dbo")]
    public class ClothSizesModel : BaseModel<Guid>
    {
        [Required]
        [ForeignKey(nameof(Clothes))]
        public Guid ClothId { get; set; }
        
        [Required]
        [ForeignKey(nameof(Sizes))]
        public Guid SizeId { get; set; }

        [Required]
        public int Amount { get; set; }

        public virtual ClothModel Clothes { get; set; }
        public virtual ClothSizeModel Sizes { get; set; }
    }
}
