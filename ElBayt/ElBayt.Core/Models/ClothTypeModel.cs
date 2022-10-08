using ElBayt.Common.Common;
using ElBayt.Common.Infra.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ElBayt.Core.Models
{
    [Table("ClothType", Schema = "dbo")]
    public class ClothTypeModel : ProductTypeModel
    {
        [Required]
        [ForeignKey(nameof(ClothDepartments))]
        public Guid ClothDepartmentId { get; set; }
        public virtual ClothDepartmentModel ClothDepartments { get; set; }
    }
}
