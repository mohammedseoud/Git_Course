using ElBayt.Common.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElBayt.DTO.ELBayt.DBDTOs
{
    public class ClothDTO : ProductDTO
    {
        public Guid ClothCategoryId { get; set; }
        public virtual ClothDTO ClothCategories { get; set; }
    }
}
