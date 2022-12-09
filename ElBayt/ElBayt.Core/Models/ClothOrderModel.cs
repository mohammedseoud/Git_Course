using ElBayt.Common.Common;
using ElBayt.Common.Infra.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElBayt.Core.Models
{
    [Table("ClothOrder", Schema = "dbo")]
    public class ClothOrderModel : BaseModel<int>
    {
        [Required]
        [StringLength(General.BIG_LINE_MAX_LENGTH)]
        public string Notes { get; set; }
        [Required]
        [ForeignKey(nameof(Clients))]
        public int ClientId { get; set; }

        public virtual ClientModel Clients { get; set; }
    }
}
