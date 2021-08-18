using ElBayt.Common.Common;
using ElBayt.Common.Infra.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElBayt.Infra.Models
{
    [Table("ProductCategory", Schema = "dbo")]
    public class ProductCategoryModel : BaseModel<Guid>
    {
        [Required]
        [StringLength(General.SINGLE_LINE_MAX_LENGTH)]
        public string Name { get; set; }
        [ForeignKey(nameof(ProductTypes))]
        public Guid ProductTypeId { get; set; }

        public virtual ProductTypeModel ProductTypes { get; set; }
    }
}
