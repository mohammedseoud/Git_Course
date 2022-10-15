﻿using ElBayt.Common.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElBayt.DTO.ELBayt.DTOs
{
    public class GetGovernorateDTO : BasicDto<Guid>
    {
        public string Name { get; set; }
        public Guid CountryId { get; set; }

    }
}