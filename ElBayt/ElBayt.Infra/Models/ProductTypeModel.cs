using ElBayt.Common.Common;
using ElBayt.Common.Infra.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ElBayt.Infra.Models
{
    [Table("ProductType", Schema = "dbo")]
    public class ProductTypeModel : BaseModel<Guid>
    {
        [Required]
        [StringLength(General.SINGLE_LINE_MAX_LENGTH)]
        public string Name { get; set; }
        [ForeignKey(nameof(Departments))]
        public Guid DepartmentId { get; set; }
        public virtual DepartmentModel Departments { get; set; }
    }
}
