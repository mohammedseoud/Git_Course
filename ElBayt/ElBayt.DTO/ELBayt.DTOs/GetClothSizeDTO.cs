using ElBayt.Common.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElBayt.DTO.ELBayt.DTOs
{
    public class GetClothSizeDTO : BasicDto<Guid>
    {
        public Guid ClothId { get; set; }
        public string Name { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }

    }
}
