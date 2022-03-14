﻿using ElBayt.Common.Common;
using ElBayt.Common.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ElBayt.Common.Entities
{
    public class ProductDepartmentEntity : EnhancedEntity<Guid> , BaseEntity
    {
        [Required]
        [StringLength(General.SINGLE_LINE_MAX_LENGTH)]
        public string Name { get; set; }
        [Required]
        [StringLength(General.SINGLE_LINE_MAX_LENGTH)]
        public string DepartmentPic { get; set; }

    }
}
