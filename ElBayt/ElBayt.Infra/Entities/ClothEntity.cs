using ElBayt.Common.Common;
using ElBayt.Common.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ElBayt.Infra.Entities
{
    public class ClothEntity : ProductEntity
    {

        [Required]
        public Guid ClothCategoryId { get; set; }
        public virtual ClothCategoryEntity ClothCategories { get; set; }
    }
}
