using ElBayt.Common.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElBayt.DTO.ELBayt.DBUDTDTOs
{
    public class UTDProductDepartmentDTO : BaseDto<int>, IBaseDTO
    {
        public string Name { get; set; }
        public string DepartmentPic { get; set; }

    }
}
