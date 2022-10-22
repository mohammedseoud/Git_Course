using Microsoft.AspNetCore.Http;

namespace ElBayt.DTO.ELBayt.DTOs
{
    public class AddClothImageDTO
    {
        public IFormFile File { get; set; }
        public int ClothId { get; set; }
    }
}
