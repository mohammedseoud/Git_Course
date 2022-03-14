using ElBayt.Common.Common;
using ElBayt.Common.Infra.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ElBayt.Infra.Models
{
    [Table("ClothCategories", Schema = "dbo")]
    public class ClothCategoriesModel : BaseModel<Guid>
    {
        [Required]
        [ForeignKey(nameof(Clothes))]
        public Guid ClothId { get; set; }
     
        [Required]
        [ForeignKey(nameof(Categories))]
        public Guid CategoryId { get; set; }
       
        public virtual ClothModel Clothes { get; set; }
        public virtual ClothCategoryModel Categories { get; set; }
    }
}
