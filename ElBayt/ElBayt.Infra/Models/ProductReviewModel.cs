using ElBayt.Common.Common;
using ElBayt.Common.Infra.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ElBayt.Infra.Models
{
    [Table("ProductReview", Schema = "dbo")]
    public class ProductReviewModel : BaseModel<Guid>
    {
        [Required]
        [StringLength(General.BIG_LINE_MAX_LENGTH)]
        public string Comment { get; set; }
        [Required]
        public float Rate { get; set; }
        [Required]
        [ForeignKey(nameof(Clients))]
        public Guid ClientId { get; set; }
        public virtual ClientModel Clients { get; set; }

    }
}
