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
        public Task<string> DeleteCloth(int Id);
        public Task<List<string>> UpdateCloth(IFormCollection Form, string DiskDirectory, string MachineDirectory);
        public Task<NumberClothDTO> GetCloth(int Id);
        public Task<ClothImageDTO> SaveClothImage(string ProductId, IFormFile file, string DiskDirectory);
        public Task<ClothImageDTO> GetClothImage(int Id);
        public Task<List<ClothImageDTO>> GetClothImages(int ProductId);
        public Task<string> DeleteClothImage(int ImageId);
        public Task<string> DeleteClothImageByURL(string URL);
        public Task<string> AddClothBrands(SelectedBrandsDTO selectedBrands);
        public Task<List<ClothBrandsDTO>> GetClothBrands(int ClothId);
        public Task<ClothDBLDataDTO> GetClothDBLInfo(int ClothId);
        public Task<string> AddClothInfo(ClothInfoDTO ClothInfo);
        public Task<List<ClothInfoDataDTO>> GetClothInfo(int ClothId);
        public Task<string> DeleteClothInfo(int Id);
        public Task<ClothInfoDTO> GetInfo(int Id);

    }
}
