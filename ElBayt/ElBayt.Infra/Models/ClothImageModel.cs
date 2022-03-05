using ElBayt.Common.Common;
using ElBayt.Common.Infra.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ElBayt.Infra.Models
{
    [Table("ClothImage", Schema = "dbo")]
    public class ClothImageModel : ProductImageModel
    {
        [ForeignKey(nameof(Cloths))]
        public Guid ClothId { get; set; }

        public virtual ClothModel Cloths { get; set; }
    }
}
