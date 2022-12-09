﻿using ElBayt.Common.Common;
using ElBayt.Common.Infra.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElBayt.Core.Models
{
    [Table("Client", Schema = "dbo")]
    public class ClientModel : BaseModel<int>
    {
        [Required]
        [StringLength(General.SINGLE_LINE_MAX_LENGTH)]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }

    
    }
}
