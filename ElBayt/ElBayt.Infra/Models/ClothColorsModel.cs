using ElBayt.Common.Common;
using ElBayt.Common.Infra.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ElBayt.Infra.Models
{
    [Table("ClothColors", Schema = "dbo")]
    public class ClothColorsModel : BaseModel<Guid>
    {
        [Required]
        [ForeignKey(nameof(Clothes))]
        public Guid ClothId { get; set; }
     
        [Required]
        [ForeignKey(nameof(Colors))]
        public Guid ColorId { get; set; }

        public virtual ClothModel Clothes { get; set; }
        public virtual ColorModel Colors { get; set; }
    }
}
