using System;
using System.Collections.Generic;
using System.Text;

namespace ElBayt.DTO.ELBayt.DTOs
{
    public class ProductImageDataDTO
    {
        public Guid Id { set; get; }
        public string URL { set; get; }
        public Guid ProductId { get; set; }
    }
}
