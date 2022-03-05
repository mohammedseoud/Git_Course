using ElBayt.Common.Common;
using ElBayt.Common.Infra.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ElBayt.Infra.Models
{
    [Table("ProductSize", Schema = "dbo")]
    public class ProductSizeModel: BaseModel<Guid>
    {
        //[Required]
        //[ForeignKey(nameof(Products))]
        //public Guid ProductId { get; set; }
        //[Required]
        //[ForeignKey(nameof(Sizes))]
        //public Guid SizeId { get; set; }
        public int Amount { get; set; }
       // public virtual ProductModel Products { get; set; }
   //     public virtual SizeModel Sizes { get; set; }
    }
}
