using ElBayt.Common.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace ElBayt.Common.Infra.Models
{
    public class ProductDepartmentModel : BaseModel<Guid>
    {
        [Required]
        [StringLength(General.SINGLE_LINE_MAX_LENGTH)]
        public string Name { get; set; }
        [Required]
        [StringLength(General.SINGLE_LINE_MAX_LENGTH)]
        public string DepartmentPic { get; set; }
    }
}
