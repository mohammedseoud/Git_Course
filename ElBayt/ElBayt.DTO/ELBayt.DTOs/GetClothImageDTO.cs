using ElBayt.Common.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElBayt.DTO.ELBayt.DTOs
{
    public class GetClothImageDTO : BasicDto<Guid>
    {
        public string URL { get; set; }
        public Guid ClothId { get; set; }

    }
}
