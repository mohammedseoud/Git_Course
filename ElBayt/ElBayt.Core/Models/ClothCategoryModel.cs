using ElBayt.Common.Common;
using ElBayt.Common.Infra.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ElBayt.Core.Models
{
    [Table("ClothCategory", Schema = "dbo")]
    public class ClothCategoryModel : ProductCategoryModel
    {
        [Required]
        [ForeignKey(nameof(ClothTypes))]
        public int ClothTypeId { get; set; }
        public virtual ClothTypeModel ClothTypes { get; set; }

    }
}
