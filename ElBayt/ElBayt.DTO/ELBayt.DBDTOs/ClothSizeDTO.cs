using ElBayt.Common.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElBayt.DTO.ELBayt.DBDTOs
{
    public class ClothSizeDTO : ProductSizeDTO
    {
        public Guid ClothId { get; set; }
        public virtual ClothDTO Clothes { get; set; }
    }
}
