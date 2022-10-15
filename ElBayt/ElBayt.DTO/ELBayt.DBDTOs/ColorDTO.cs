using ElBayt.Common.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElBayt.DTO.ELBayt.DBDTOs
{
    public class ColorDTO : BaseDto<int>, IBaseDTO
    {
        public string Name { get; set; }
    }
}
