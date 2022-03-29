using ElBayt.Common.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElBayt.DTO.ELBayt.DBUDTDTOs
{
    public class UTDProductImageDTO : BaseDto<Guid>, IBaseDTO
    {
        public string URL { get; set; }

    }
}
