using ElBayt.Common.Common;
using ElBayt.Common.Infra.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElBayt.Core.Models
{
    [Table("ClientInfo", Schema = "dbo")]
    public class ClientInfoModel : BaseModel<int>
    {
        [Required]
        [ForeignKey(nameof(Clients))]
        public int ClothId { get; set; }
        [Required]
        [StringLength(General.SINGLE_LINE_MAX_LENGTH)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(General.SINGLE_LINE_MAX_LENGTH)]
        public string LastName { get; set; }
        [StringLength(General.SINGLE_LINE_MAX_LENGTH)]
        public string CompanyName { get; set; }
        [Required]
        [StringLength(General.SINGLE_LINE_MAX_LENGTH)]
        public string Email { get; set; }
        [Required]
        [StringLength(General.SINGLE_LINE_MAX_LENGTH)]
        public string Phone { get; set; }
        [Required]
        [ForeignKey(nameof(City1))]
        public int CityId { get; set; }
        [Required]
        [StringLength(General.MULTIPLE_LINE_MAX_LENGTH)]
        public string StreetAddress { get; set; }
        [Required]
        [StringLength(General.MULTIPLE_LINE_MAX_LENGTH)]
        public string AppartmentAddress { get; set; }
        [Required]
        [StringLength(General.SINGLE_LINE_MAX_LENGTH)]
        public string Postcode { get; set; }
        
        [StringLength(General.SINGLE_LINE_MAX_LENGTH)]
        public string SecondFirstName { get; set; }
        
        [StringLength(General.SINGLE_LINE_MAX_LENGTH)]
        public string SecondLastName { get; set; }
        [StringLength(General.SINGLE_LINE_MAX_LENGTH)]
        public string SecondCompanyName { get; set; }
       
        [ForeignKey(nameof(City2))]
        public int SecondCityId { get; set; }
        
        [StringLength(General.MULTIPLE_LINE_MAX_LENGTH)]
        public string SecondStreetAddress { get; set; }
        
        [StringLength(General.MULTIPLE_LINE_MAX_LENGTH)]
        public string SecondAppartmentAddress { get; set; }
        
        [StringLength(General.SINGLE_LINE_MAX_LENGTH)]
        public string SecondPostcode { get; set; }

        public virtual ClientModel Clients { get; set; }
        public virtual CityModel City1 { get; set; }
        public virtual CityModel City2 { get; set; }
    }
}
