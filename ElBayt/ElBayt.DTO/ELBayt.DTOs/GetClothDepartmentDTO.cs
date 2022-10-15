using ElBayt.Common.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElBayt.DTO.ELBayt.DBDTOs
{
    public class GetClothDepartmentDTO : BasicDto<int>
    {
        public string Name { get; set; }
        public string DepartmentPic { get; set; }
    }
}
