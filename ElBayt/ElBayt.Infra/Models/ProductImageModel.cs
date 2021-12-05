using ElBayt.Common.Common;
using ElBayt.Common.Infra.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ElBayt.Infra.Models
{
    [Table("ProducImage", Schema = "dbo")]
    public class ProductImageModel : BaseModel<Guid>
    {
        [Required]
        [StringLength(General.SINGLE_LINE_MAX_LENGTH)]
        public string URL { get; set; }
        
        [ForeignKey(nameof(Products))]
        public Guid ProductId { get; set; }

        public virtual ProductModel Products { get; set; }
    }
}
