using System;
using System.Collections.Generic;
using System.Text;

namespace ElBayt.DTO.ELBayt.DTOs
{
    public class SelectedBrandsDTO
    {
        public List<Guid> Brands { set; get; }
        public Guid ClothId { set; get; }
    }
}
