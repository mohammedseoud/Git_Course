using ElBayt.Common.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElBayt.DTO.ELBayt.DBDTOs
{
    public class GetClothCategoryDTO : BasicDto<int>
    { 
        public string Name { get; set; }
        public int ClothTypeId { get; set; }
    }
}
