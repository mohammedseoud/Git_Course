using ElBayt.Common.Common;
using ElBayt.Common.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ElBayt.Common.Entities
{
    public class ProductBrandEntity : EnhancedEntity<Guid>, BaseEntity
    {
        [Required]
        [StringLength(General.SINGLE_LINE_MAX_LENGTH)]
        public string Name { get; set; }
      
        [StringLength(General.SINGLE_LINE_MAX_LENGTH)]
        public string BrandPic { get; set; }

    }
}
