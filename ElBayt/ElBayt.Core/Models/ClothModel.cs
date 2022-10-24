﻿using ElBayt.Common.Infra.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElBayt.Core.Models
{
    [Table("Cloth", Schema = "dbo")]
    public class ClothModel : ProductModel
    {
        [Required]
        [ForeignKey(nameof(ClothCategories))]
        public int ClothCategoryId { get; set; }
        public virtual ClothCategoryModel ClothCategories { get; set; }
    }
}
