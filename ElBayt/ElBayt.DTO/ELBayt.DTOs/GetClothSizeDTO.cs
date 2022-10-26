﻿using ElBayt.Common.Core.Services;

namespace ElBayt.DTO.ELBayt.DTOs
{
    public class GetClothSizeDTO : BasicDto<int>
    {
        public string Name { get; set; }
        public int? Height { get; set; }
        public int? Width { get; set; }
        public string Abbreviation { get; set; }

    }
}
