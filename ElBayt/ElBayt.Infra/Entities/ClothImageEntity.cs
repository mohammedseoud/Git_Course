using ElBayt.Common.Common;
using ElBayt.Common.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ElBayt.Infra.Entities
{
    public class ClothImageEntity : ProductImageEntity
    {
        [Required]
        public Guid ClothId { get; set; }

        public virtual ClothEntity Cloths { get; set; }
    }
}
