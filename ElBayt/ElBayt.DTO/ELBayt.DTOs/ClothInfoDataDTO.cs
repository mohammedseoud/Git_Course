using ElBayt.Common.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElBayt.DTO.ELBayt.DBDTOs
{
    public class ClothInfoDataDTO 
    {
        public int Id { get; set; }
        public string Price { get; set; }
        public string PriceAfterDiscount { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public string Amount { get; set; }
        public string Brand { get; set; }

    }
}
