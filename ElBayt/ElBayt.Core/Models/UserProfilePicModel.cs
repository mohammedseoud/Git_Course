using ElBayt.Common.Common;
using ElBayt.Common.Infra.Models;
using ElBayt.Common.Security;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElBayt.Core.Models
{
    [Table("UserProfilePic", Schema = "dbo")]
    public class UserProfilePicModel : BaseModel<int>
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
