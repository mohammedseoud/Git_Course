using ElBayt.Common.Enums;
using ElBayt.DTO.ELBayt.DBDTOs;
using ElBayt.DTO.ELBayt.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
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
        #region Products
        public Task<NumberClothDTO> AddNewCloth(IFormCollection form, string DiskDirectory);
        public object GetClothes();
        public Task<string> DeleteCloth(Guid Id);
        public Task<NumberClothDTO> UpdateCloth(IFormCollection Form, string DiskDirectory);
        public Task<NumberClothDTO> GetCloth(Guid Id);
        public Task<ClothImageDTO> SaveClothImage(string ProductId, IFormFile file, string DiskDirectory);
        public Task<ClothImageDTO> GetClothImage(Guid Id);
        public Task<List<ClothImageDTO>> GetClothImages(Guid ProductId);
        public Task<string> DeleteClothImage(Guid ImageId);
        public Task<string> DeleteClothImageByURL(string URL);
        #endregion
        #region Departments
        public Task<EnumInsertingResult> AddNewClothDepartment(ClothDepartmentDTO clothDepartment);
        public object GetClothDepartments();
        public Task<string> DeleteClothDepartment(Guid Id);
        public Task<EnumUpdatingResult> UpdateClothDepartment(ClothDepartmentDTO clothDepartment);
        public Task<ClothDepartmentDTO> GetClothDepartment(Guid Id);
        #endregion
    }
}
