using ElBayt.Common.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElBayt.Common.Core.Services
{
    public class ProductCategoryDTO : BaseDto<Guid>, IBaseDTO
    { 
        public string Name { get; set; }
    }
}
