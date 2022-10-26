using ElBayt.Common.Common;
using System.ComponentModel.DataAnnotations;

namespace ElBayt.Common.Infra.Models
{
    public class ProductSizeModel : BaseModel<int>
    {
        [Required]
        [StringLength(General.SINGLE_LINE_MAX_LENGTH)]
        public string Name { get; set; }
        public int? Height { get; set; }
        public int? Width { get; set; }
       
    }
}
