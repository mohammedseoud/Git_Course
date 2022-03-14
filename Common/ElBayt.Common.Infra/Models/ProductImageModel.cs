using ElBayt.Common.Common;
using ElBayt.Common.Infra.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ElBayt.Common.Infra.Models
{
    public class ProductImageModel : BaseModel<Guid>
    {
        [Required]
        [StringLength(General.MULTIPLE_LINE_MAX_LENGTH)]
        public string URL { get; set; }
     
    }
}
