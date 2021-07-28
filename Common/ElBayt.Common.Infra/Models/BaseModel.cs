using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ElBayt.Common.Infra.Models
{
    public abstract class BaseModel<T> : BaseGeneralModel
    {
        [Key]
        [Required]
        public virtual T Id { get; set; }
    }

    public abstract class BaseGeneralModel
    {
        public virtual DateTime CreatedDate { get; set; }
        [Required]
        [MaxLength(50)] 
        public virtual string CreatedBy { get; set; }
        public virtual DateTime ModifiedDate { get; set; }
        [Required]
        [MaxLength(50)] 
        public virtual string ModifiedBy { get; set; }
        [DefaultValue(false)]
        public bool IsDeleted { get; set; }
    }
}
