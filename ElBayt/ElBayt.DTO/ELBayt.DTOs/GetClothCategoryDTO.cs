using ElBayt.Common.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElBayt.DTO.ELBayt.DBDTOs
{
    public class GetClothCategoryDTO : BasicDto<Guid>
    { 
        public string Name { get; set; }
        public Guid ClothTypeId { get; set; }
    }
}
