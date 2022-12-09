using System;
using System.ComponentModel.DataAnnotations;
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
        public virtual string CreatedBy { get; set; }
        public virtual DateTime? ModifiedDate { get; set; }
        public virtual string ModifiedBy { get; set; }
    }
}
