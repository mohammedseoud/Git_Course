﻿using ElBayt.Common.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElBayt.DTO.ELBayt.DBUDTDTOs
{
    public class UTDProductTypeDTO : BaseDto<int>, IBaseDTO
    {
        public string Name { get; set; }
        public string TypePic { get; set; }

    }
}
