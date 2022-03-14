using ElBayt.Common.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElBayt.DTO.ELBayt.DBDTOs
{
    public class ClothImageDTO : BaseDto<Guid>, IBaseDTO
    {
        public string URL { get; set; }

        public Guid ClothId { get; set; }

        public virtual ProductDTO Products { get; set; }
    }
}
