using ElBayt.Common.Enums;
using ElBayt.DTO.ELBayt.DBDTOs;
using ElBayt.DTO.ELBayt.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace ElBayt.Services.Contracts
{
    public interface IClothesService
    {
        #region Types
        public Task<ClothTypeDTO> AddNewClothType(IFormCollection Form, string DiskDirectory);
        public object GetClothTypes();
        public Task<string> DeleteClothType(Guid Id);
        public Task<EnumUpdatingResult> UpdateClothType(ClothTypeDTO clothType, string DiskDirectory);
        public Task<ClothTypeDTO> GetClothType(Guid Id);
        #endregion
        #region Categories
        public Task<EnumInsertingResult> AddNewClothCategory(ClothCategoryDTO clothCategory);
        public object GetClothCategories();
        public Task<string> DeleteClothCategory(Guid Id);
        public Task<EnumUpdatingResult> UpdateClothCategory(ClothCategoryDTO clothCategory);
        public Task<ClothCategoryDTO> GetClothCategory(Guid Id);
        #endregion

    }
}
