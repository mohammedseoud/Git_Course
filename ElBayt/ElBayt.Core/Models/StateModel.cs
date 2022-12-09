using ElBayt.Common.Common;
using ElBayt.Common.Infra.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElBayt.Core.Models
{
    [Table("State", Schema = "dbo")]
    public class StateModel : BaseModel<int>
    {
        [Required]
        [StringLength(General.SINGLE_LINE_MAX_LENGTH)]
        public string Name { get; set; }
        [ForeignKey(nameof(States))]
        public int CityId { get; set; }

        public virtual CityModel States { get; set; }
    }
}
