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
    public interface IClothTypeService
    {
        public Task<ClothTypeDTO> AddNewClothType(IFormCollection Form, string DiskDirectory);
        public Task<List<GetClothTypeDTO>> GetClothTypes();
        public Task<string> DeleteClothType(int Id);
        public Task<EnumUpdatingResult> UpdateClothType(IFormCollection Form, string DiskDirectory, string machineDirectory);
        public Task<ClothTypeDTO> GetClothType(int Id);
       
    }
}
