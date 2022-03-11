using ElBayt.Common.Common;
using ElBayt.Common.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ElBayt.Core.Entities
{
    public class ClothCategoryEntity : ProductCategoryEntity
    {
        [Required]
        public Guid ClothTypeId { get; set; }
        public virtual ClothTypeEntity ClothTypes { get; set; }
    }
}
