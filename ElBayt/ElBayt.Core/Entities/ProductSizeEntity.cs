using ElBayt.Common.Common;
using ElBayt.Common.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ElBayt.Core.Entities
{
    public class ProductSizeEntity 
    {
        [Required]
        public Guid ProductId { get; set; }
        [Required] 
        public Guid SizeId { get; set; }
        public int Amount { get; set; }
        public virtual ProductEntity Products { get; set; }
        public virtual SizeEntity Sizes { get; set; }
    }
}
