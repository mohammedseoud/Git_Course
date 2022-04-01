﻿using ElBayt.Common.Common;
using ElBayt.Common.Infra.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ElBayt.Common.Infra.Models
{
    public class ProductModel : BaseModel<Guid>
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


        //[ForeignKey(nameof(ProductCategories))]
        //public Guid ProductCategoryId { get; set; }


        //public virtual ProductCategoryModel ProductCategories { get; set; }
    }
}