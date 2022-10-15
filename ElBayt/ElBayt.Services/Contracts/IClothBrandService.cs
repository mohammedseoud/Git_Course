using ElBayt.Common.Enums;
using ElBayt.DTO.ELBayt.DBDTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ElBayt.Services.Contracts
{
    public interface IClothBrandService
    {
        public Task<ClothBrandDTO> AddNewClothBrand(IFormCollection Form, string DiskDirectory);
        public Task<List<GetClothBrandDTO>> GetClothBrands();
        public Task<string> DeleteClothBrand(int Id);
        public Task<EnumUpdatingResult> UpdateClothBrand(IFormCollection Form, string DiskDirectory, string MachineDirectory);
        public Task<ClothBrandDTO> GetClothBrand(int Id);
    }
}
