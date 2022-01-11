using ElBayt.Common.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElBayt.DTO.ELBayt.DBDTOs
{
    public class ProductTypeDTO : BaseDto<Guid>, IBaseDTO
    {
        public string Name { get; set; }
        public Guid DepartmentId { get; set; }
    }
}
