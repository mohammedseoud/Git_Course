using ElBayt.Common.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElBayt.DTO.ELBaytDTO_s
{
    public class ProductDepartmentDTO : BaseDto<Guid>, IBaseDTO
    { 
        public string Name { get; set; }
     }
}
