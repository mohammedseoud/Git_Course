using ElBayt.Common.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElBayt.DTO.ELBayt.DBUDTDTOs
{
    public class UTDProductBrandDTO : BaseDto<Guid>, IBaseDTO
    {
        public string Name { get; set; }
        public string BrandPic { get; set; }

    }
}
