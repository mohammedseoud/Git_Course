using ElBayt.Common.Common;
using ElBayt.Common.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ElBayt.Common.Entities
{
    public class ProductImageEntity : EnhancedEntity<Guid> , BaseEntity
    {
        [Required]
        [StringLength(General.SINGLE_LINE_MAX_LENGTH)]
        public string URL { get; set; }

        public Guid ProductId { get; set; }

        public virtual ProductEntity Products { get; set; }
    }
}
