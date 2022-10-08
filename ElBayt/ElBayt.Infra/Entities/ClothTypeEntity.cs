using ElBayt.Common.Common;
using ElBayt.Common.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ElBayt.Infra.Entities
{
    public class ClothTypeEntity : ProductTypeEntity
    {
        [Required]
        public Guid ClothDepartmentId { get; set; }
        public virtual ClothDepartmentEntity ClothDepartments { get; set; }
    }
}
