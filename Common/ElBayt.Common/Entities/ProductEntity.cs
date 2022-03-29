using ElBayt.Common.Common;
using ElBayt.Common.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ElBayt.Common.Entities
{
    public class ProductEntity : EnhancedEntity<Guid> , BaseEntity
    {
        [Required]
        [StringLength(General.SINGLE_LINE_MAX_LENGTH)]
        public string Name { get; set; }

       
        [StringLength(General.BIG_LINE_MAX_LENGTH)]
        [Required]
        public string Description { get; set; }

        [StringLength(General.MULTIPLE_LINE_MAX_LENGTH)]
        [Required]
        public string ProductImageURL1 { get; set; }

        [StringLength(General.MULTIPLE_LINE_MAX_LENGTH)]
        public string ProductImageURL2 { get; set; }

    }
}
