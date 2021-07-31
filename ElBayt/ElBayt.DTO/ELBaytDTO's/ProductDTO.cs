using ElBayt.Common.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElBayt.DTO.ELBaytDTO_s
{
    public class ProductDTO : BaseDto<Guid>
    { 
        public string Name { get; set; }
        public string Price { get; set; }
    }
}
