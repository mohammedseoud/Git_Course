using ElBayt.Common.Common;
using ElBayt.Common.Infra.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ElBayt.Core.Models
{
    [Table("ClothSize", Schema = "dbo")]
    public class ClothSizeModel : ProductSizeModel
    {
        [Required]
        [ForeignKey(nameof(Clothes))]
        public Guid ClothId { get; set; }
        public virtual ClothModel Clothes { get; set; }
    }
}
