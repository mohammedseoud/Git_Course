using ElBayt.Common.Common;
using ElBayt.Common.Infra.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ElBayt.Core.Models
{
    [Table("Country", Schema = "dbo")]
    public class CountryModel : BaseModel<Guid>
    {
        [Required]
        [StringLength(General.SINGLE_LINE_MAX_LENGTH)]
        public string Name { get; set; }
    }
}
