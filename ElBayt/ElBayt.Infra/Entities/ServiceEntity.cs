using ElBayt.Common.Common;
using ElBayt.Common.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ElBayt.Infra.Entities
{
    public class ServiceEntity : EnhancedEntity<Guid> , BaseEntity
    {
        [Required]
        [StringLength(General.SINGLE_LINE_MAX_LENGTH)]
        public string Name { get; set; }

        [Required]
         public decimal Price { get; set; }

        [StringLength(General.BIG_LINE_MAX_LENGTH)]
        [Required]
        public string Description { get; set; }
        
    }
}
