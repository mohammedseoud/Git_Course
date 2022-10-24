using Dapper;
using ElBayt.Common.Enums;
using ElBayt.Common.Utilities;
using ElBayt.DTO.ELBayt.DBDTOs;
using ElBayt.DTO.ELBayt.DBUDTDTOs;
using ElBayt.Infra.SPs;
using ElBayt.Services.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ElBayt.Core.Models;
using ElBayt.DTO.ELBayt.DTOs;
using ElBayt.Common.Common;

namespace ElBayt.Services.Implementations
{
    public partial class ClothesService : IClothesService
    {
        #region Categories
        public async Task<EnumInsertingResult> AddNewClothCategory(ClothCategoryDTO clothCategory)
        {
            try
            {
                var Category = await _unitOfWork.ClothCategoryRepository.GetClothCategoryByName(clothCategory.Name.Trim(), clothCategory.Id);;
                if (Category == null)
                {
                    var Entity = _mapper.Map<ClothCategoryDTO, ClothCategoryModel>(clothCategory);
                    await _unitOfWork.ClothCategoryRepository.AddAsync(Entity);
                    await _unitOfWork.SaveAsync();
                    return EnumInsertingResult.Successed;
                }
                return EnumInsertingResult.Failed;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<GetClothCategoryDTO>> GetClothCategories()
        {
            try
            {
                var Categories = (await _unitOfWork.ClothCategoryRepository.GetAllAsync())
                    .Select(c =>
                    new GetClothCategoryDTO {
                        ClothTypeId = c.ClothTypeId,
                        Id = c.Id,
                        Name = c.Name,
                    });

                return Categories.ToList();

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<string> DeleteClothCategory(int Id)
        {
            try
            {
                var SPParameters = new DynamicParameters();
                SPParameters.Add("@CategoryId", Id);
                return await _unitOfWork.SP.OneRecordAsnyc<string>(StoredProcedure.DELETECLOTHCATEGORY, SPParameters);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<EnumUpdatingResult> UpdateClothCategory(ClothCategoryDTO clothCategory, string machineDirectory)
        {
            try
            {
                var Category = await _unitOfWork.ClothCategoryRepository.GetClothCategoryByName(clothCategory.Name.Trim(), clothCategory.Id);

                if (Category == null)
                {
                    var identityName = _userIdentity?.Name ?? "Unknown";
                    var UTDClothCategory = _mapper.Map<ClothCategoryDTO, UTDClothCategoryDTO>(clothCategory);
                    var ClothCategories = new List<UTDClothCategoryDTO>
                    {
                       UTDClothCategory
                    };
                    UTDClothCategory.ModifiedBy = identityName;
                    UTDClothCategory.ModifiedDate = DateTime.Now;

                    var clothCategorytable = ObjectDatatableConverter.ToDataTable(ClothCategories);
                    var SPParameters = new DynamicParameters();
                    SPParameters.Add("@UDTClothCategory", clothCategorytable.AsTableValuedParameter(UDT.UDTCLOTHCATEGORY));
                    var URL = await _unitOfWork.SP.ListAsnyc<string, string>(StoredProcedure.UPDATECLOTHCATEGORY, SPParameters);
                    var URL1 = Path.Combine(machineDirectory, URL.Item1.FirstOrDefault());
                    var URL2 = Path.Combine(machineDirectory, URL.Item2.FirstOrDefault());
                 
                    if (URL1 != URL2)
                        _filemapper.MoveDataBetweenTwoFile(URL1, URL2);

                    return EnumUpdatingResult.Successed;
                }

                return EnumUpdatingResult.Failed;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ClothCategoryDTO> GetClothCategory(int Id)
        {
            try
            {   
                var Model = await _unitOfWork.ClothCategoryRepository.GetAsync(Id);
                return _mapper.Map<ClothCategoryModel, ClothCategoryDTO>(Model);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<string> AddClothCategoryBrands(SelectedCategoryBrandsDTO selectedCategoryBrands)
        {
            try
            {
                var identityName = _userIdentity?.Name ?? "Unknown";
                var clothBrands = new List<UTDClothBrandDTO>();

                foreach (var brand in selectedCategoryBrands.Brands)
                {

                    var clothBrand = new ClothBrandDTO
                    {
                        Id = brand
                    };
                    var clothBrandEntity = _mapper.Map<ClothBrandDTO, UTDClothBrandDTO>(clothBrand);
                    clothBrandEntity.CreatedDate = DateTime.Now;
                    clothBrandEntity.CreatedBy = identityName;
                    clothBrands.Add(clothBrandEntity);
                }

                var clothDepartmenttable = ObjectDatatableConverter.ToDataTable(clothBrands);
                var SPParameters = new DynamicParameters();
                SPParameters.Add("@UDTClothBrand", clothDepartmenttable.AsTableValuedParameter(UDT.UDTCLOTHBRAND));
                SPParameters.Add("@ClothId", selectedCategoryBrands.ClothCategoryId);

                await _unitOfWork.SP.ExecuteAsnyc<ClothBrandDTO>(StoredProcedure.ADDCLOTHCATEGORORYBRANDS, SPParameters);
                return CommonMessages.SUCCESSFULLY_ADDING;

            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        public async Task<List<ClothCategoryBrandsDTO>> GetClothCategoryBrands(int ClothCategoryId)
        {
            try
            {
                var SPParameters = new DynamicParameters();
                SPParameters.Add("@ClothCategoryId", ClothCategoryId);


                return (await _unitOfWork.SP.ListAsnyc<ClothCategoryBrandsDTO>(StoredProcedure.GETCLOTHCATEGORORYBRANDS, SPParameters)).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion
    }
}
