using ElBayt.Common.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElBayt.DTO.ELBayt.DBDTOs
{
    public class ClothImageDTO : BaseDto<int>, IBaseDTO
    {
        public string URL { get; set; }

        public int ClothId { get; set; }
    }
}
