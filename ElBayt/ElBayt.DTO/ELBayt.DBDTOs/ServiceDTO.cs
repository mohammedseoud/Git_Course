using ElBayt.Common.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElBayt.DTO.ELBayt.DBDTOs
{
    public class ServiceDTO : BaseDto<Guid>, IBaseDTO
    { 
        public string Name { get; set; }
        public decimal Price { get; set; }      
        public string Description { get; set; }
       
    }
}
