using ElBayt.Common.Common;
using ElBayt.Common.Infra.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElBayt.Core.Models
{
    [Table("Governorate", Schema = "dbo")]
    public class GovernorateModel : BaseModel<int>
    {
        [Required]
        [StringLength(General.SINGLE_LINE_MAX_LENGTH)]
        public string Name { get; set; }
        [ForeignKey(nameof(Countries))]
        public int CountryId { get; set; }

        public virtual CountryModel Countries { get; set; }
    }
}
