using ElBayt.Common.Core.Services;
using ElBayt.DTO.ELBayt.DBDTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElBayt.DTO.ELBayt.DTOs
{
    public class AddClothDTO 
    {
        public ClothDataDTO Cloth { get; set; }
        public IFormFileCollection Files { get; set; }
    }
}
