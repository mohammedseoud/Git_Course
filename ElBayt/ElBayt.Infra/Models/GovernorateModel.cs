using ElBayt.Common.Common;
using ElBayt.Common.Infra.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ElBayt.Infra.Models
{
    [Table("Governorate", Schema = "dbo")]
    public class GovernorateModel : BaseModel<Guid>
    {
        [Required]
        [StringLength(General.SINGLE_LINE_MAX_LENGTH)]
        public string Name { get; set; }
        [ForeignKey(nameof(Countries))]
        public Guid CountryId { get; set; }

        public virtual CountryModel Countries { get; set; }
    }
}
