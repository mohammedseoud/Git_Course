﻿using ElBayt.Common.Common;
using ElBayt.Common.Infra.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ElBayt.Common.Infra.Models
{
    public class ProductTypeModel : BaseModel<int>
    {
        [Required]
        [StringLength(General.SINGLE_LINE_MAX_LENGTH)]
        public string Name { get; set; }

        [Required]
        [StringLength(General.SINGLE_LINE_MAX_LENGTH)]
        public string TypePic { get; set; }
 
    }
}
