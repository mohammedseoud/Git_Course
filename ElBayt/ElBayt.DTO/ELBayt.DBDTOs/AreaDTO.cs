﻿using ElBayt.Common.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElBayt.DTO.ELBayt.DBDTOs
{
    public class AreaDTO : BaseDto<int>, IBaseDTO
    { 
        public string Name { get; set; }
        public int GovernorateId { get; set; }

        public GovernorateDTO Governorates { get; set; }
    }
}
