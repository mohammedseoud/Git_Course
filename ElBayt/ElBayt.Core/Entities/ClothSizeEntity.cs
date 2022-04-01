﻿using ElBayt.Common.Common;
using ElBayt.Common.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ElBayt.Core.Entities
{
    public class ClothSizeEntity : ProductSizeEntity
    {
        [Required]
        public Guid ClothId { get; set; }
        public virtual ClothEntity Clothes { get; set; }
    }
}