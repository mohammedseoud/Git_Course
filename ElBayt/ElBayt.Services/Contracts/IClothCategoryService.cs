using ElBayt.Common.Enums;
using ElBayt.DTO.ELBayt.DBDTOs;
using ElBayt.DTO.ELBayt.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ElBayt.Services.Contracts
{
    public interface IClothCategoryService
    {
        public Task<EnumInsertingResult> AddNewClothCategory(ClothCategoryDTO clothCategory);
        public Task<List<GetClothCategoryDTO>> GetClothCategories();
        public Task<string> DeleteClothCategory(int Id);
        public Task<EnumUpdatingResult> UpdateClothCategory(ClothCategoryDTO clothCategory, string machineDirectory);
        public Task<ClothCategoryDTO> GetClothCategory(int Id);
        public Task<string> AddClothCategoryBrands(SelectedCategoryBrandsDTO selectedCategoryBrands);
        public Task<List<ClothCategoryBrandsDTO>> GetClothCategoryBrands(int ClothCategoryId);
        public Task<string> AddClothCategorySizes(SelectedCategorySizesDTO selectedCategorySizes);
        public Task<List<ClothCategorySizesDTO>> GetClothCategorySizes(int ClothCategoryId);
    }
}
