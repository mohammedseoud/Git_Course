using ElBayt.Common.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElBayt.DTO.ELBaytDTO_s
{
    public class AreaDTO : BaseDto<Guid>, IBaseDTO
    { 
        public string Name { get; set; }
        public Guid GovernorateId { get; set; }

        public GovernorateDTO Governorates { get; set; }
    }
}
