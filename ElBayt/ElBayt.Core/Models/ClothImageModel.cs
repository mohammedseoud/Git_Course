using ElBayt.Common.Common;
using ElBayt.Common.Infra.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

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
