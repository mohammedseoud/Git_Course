using ElBayt.Common.Common;
using ElBayt.Common.Infra.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ElBayt.Core.Models
{
    [Table("Cloth", Schema = "dbo")]
    public class ClothModel : ProductModel
    {
        [Required]
        [ForeignKey(nameof(ClothCategories))]
        public Guid ClothCategoryId { get; set; }
        public virtual ClothCategoryModel ClothCategories { get; set; }
    }
}
