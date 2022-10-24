using ElBayt.Common.Infra.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElBayt.Core.Models
{
    [Table("ClothDepartment", Schema = "dbo")]
    public class ClothDepartmentModel : ProductDepartmentModel
    {
    }
}
