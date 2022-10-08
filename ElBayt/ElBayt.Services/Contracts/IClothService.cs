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
    public interface IClothService
    {
        public Task<NumberClothDTO> AddNewCloth(IFormCollection form, string DiskDirectory);
        public Task<List<GetClothInfoDTO>> GetClothesInfo();
        public Task<List<GetClothDTO>> GetClothes();
        public Task<string> DeleteCloth(Guid Id);
        public Task<List<string>> UpdateCloth(IFormCollection Form, string DiskDirectory, string MachineDirectory);
        public Task<NumberClothDTO> GetCloth(Guid Id);
        public Task<ClothImageDTO> SaveClothImage(string ProductId, IFormFile file, string DiskDirectory);
        public Task<ClothImageDTO> GetClothImage(Guid Id);
        public Task<List<ClothImageDTO>> GetClothImages(Guid ProductId);
        public Task<string> DeleteClothImage(Guid ImageId);
        public Task<string> DeleteClothImageByURL(string URL);
        public Task<string> AddClothBrands(SelectedBrandsDTO selectedBrands);
        public Task<List<ClothBrandsDTO>> GetClothBrands(Guid ClothId);
        public Task<ClothDBLDataDTO> GetClothDBLInfo(Guid ClothId);
        public Task<string> AddClothInfo(ClothInfoDTO ClothInfo);
        public Task<List<ClothInfoDataDTO>> GetClothInfo(Guid ClothId);
        public Task<string> DeleteClothInfo(Guid Id);
        public Task<ClothInfoDTO> GetInfo(Guid Id);

    }
}
