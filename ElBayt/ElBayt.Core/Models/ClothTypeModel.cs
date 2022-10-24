using ElBayt.Common.Infra.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElBayt.Core.Models
{
    [Table("ClothType", Schema = "dbo")]
    public class ClothTypeModel : ProductTypeModel
    {
        [Required]
        [ForeignKey(nameof(ClothDepartments))]
        public int ClothDepartmentId { get; set; }
        public virtual ClothDepartmentModel ClothDepartments { get; set; }
    }
}
