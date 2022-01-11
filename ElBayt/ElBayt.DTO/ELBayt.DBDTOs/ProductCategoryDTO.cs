using ElBayt.Common.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElBayt.DTO.ELBayt.DBDTOs
{
    public class ProductCategoryDTO : BaseDto<Guid>, IBaseDTO
    { 
        public string Name { get; set; }
        public Guid ProductTypeId { get; set; }

        public virtual ProductTypeDTO ProductTypes { get; set; }
    }
}
