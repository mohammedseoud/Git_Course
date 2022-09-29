﻿using ElBayt.Common.Common;
using ElBayt.Common.Infra.Models;
using ElBayt.Common.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ElBayt.Infra.Models
{
    [Table("UserProfilePic", Schema = "dbo")]
    public class UserProfilePicModel : BaseModel<Guid>
    {
        [Required]
        [StringLength(General.MULTIPLE_LINE_MAX_LENGTH)]
        public string URL { get; set; }
        public bool IsSelected { get; set; }

        [ForeignKey(nameof(Users))]
        public string UserId { get; set; }

        public virtual AppUser Users { get; set; }
    }
}
