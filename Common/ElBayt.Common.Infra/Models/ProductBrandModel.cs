using ElBayt.Common.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ElBayt.Common.Infra.Models
{
    public class ProductBrandModel : BaseModel<int>
    {
        [Required]
        [StringLength(General.SINGLE_LINE_MAX_LENGTH)]
        public string Name { get; set; }
      
        [StringLength(General.SINGLE_LINE_MAX_LENGTH)]
        public string BrandPic { get; set; }

    }
}
