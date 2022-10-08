using ElBayt.Common.Enums;
using ElBayt.DTO.ELBayt.DBDTOs;
using ElBayt.DTO.ELBayt.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ElBayt.Services.Contracts
{
    public interface IClothSizeService
    {
        public Task<EnumInsertingResult> AddNewClothSize(ClothSizeDTO clothSize);
        public Task<List<GetClothSizeDTO>> GetSizes();
        public Task<List<GetClothSizeDTO>> GetClothSizes(Guid ClothID);
        public Task<string> DeleteClothSize(Guid Id);
        public Task<EnumUpdatingResult> UpdateClothSize(ClothSizeDTO clothSize);
        public Task<ClothSizeDTO> GetClothSize(Guid Id);
    }
}
