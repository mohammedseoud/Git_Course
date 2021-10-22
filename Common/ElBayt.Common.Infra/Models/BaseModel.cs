using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ElBayt.Common.Common;

namespace ElBayt.Common.Infra.Models
{
    public class BaseModel<T> : BaseGeneralModel
    {
        [Key]
        [Required]
        public virtual T Id { get; set; }
    }

    public class BaseGeneralModel : BaseModel
    {
        public virtual DateTime CreatedDate { get; set; }
        [Required]
        [MaxLength(50)]
        public virtual string CreatedBy { get; set; }
        public virtual DateTime ModifiedDate { get; set; }
        [Required]
        [MaxLength(50)]
        public virtual string ModifiedBy { get; set; }
    }
}
