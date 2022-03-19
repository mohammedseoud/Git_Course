using ElBayt.Common.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ElBayt.Common.Core.Services
{
    public class ProductBrandDTO : BaseDto<Guid>, IBaseDTO
    { 
        public string Name { get; set; }
      
        public string BrandPic { get; set; }

    }
}
