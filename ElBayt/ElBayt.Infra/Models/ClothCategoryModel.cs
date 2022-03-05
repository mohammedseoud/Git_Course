using ElBayt.Common.Common;
using ElBayt.Common.Infra.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ElBayt.Infra.Models
{
    [Table("ClothCategory", Schema = "dbo")]
    public class ClothCategoryModel : ProductCategoryModel
    {
        [ForeignKey(nameof(ClothTypes))]
        public Guid ClothTypeId { get; set; }
        public virtual ClothTypeModel ClothTypes { get; set; }

    }
}
