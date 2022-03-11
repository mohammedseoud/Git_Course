using ElBayt.Common.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElBayt.DTO.ELBayt.DBDTOs
{
    public class ClothCategoryDTO : ProductCategoryDTO
    {
        public Guid ClothTypeId { get; set; }
    }
}
