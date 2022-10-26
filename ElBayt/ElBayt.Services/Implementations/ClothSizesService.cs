using ElBayt.Common.Common;
using ElBayt.Common.Enums;
using ElBayt.DTO.ELBayt.DBDTOs;
using ElBayt.DTO.ELBayt.DTOs;
using ElBayt.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElBayt.Core.Models;

namespace ElBayt.Services.Implementations
{
    public partial class ClothesService : IClothesService
    {

        #region Sizes
        public async Task<EnumInsertingResult> AddNewClothSize(ClothSizeDTO clothSize)
        {
            try
            {
                var identityName = _userIdentity?.Name ?? "Unknown";
                var Size = await _unitOfWork.ClothSizeRepository.GetClothSizeByName(clothSize.Name.Trim(), clothSize.Id); ;
                if (Size == null)
                {
                    var Entity = _mapper.Map<ClothSizeDTO, ClothSizeModel>(clothSize);
                    Entity.CreatedBy = _userIdentity.Name;
                    Entity.CreatedDate = DateTime.Now;
                    await _unitOfWork.ClothSizeRepository.AddAsync(Entity);
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

        public async Task<List<GetClothSizeDTO>> GetSizes()
        {
            try
            {
                var Sizes = (await _unitOfWork.ClothSizeRepository.GetAllAsync()).Select(
                    c => new GetClothSizeDTO
                    {
                        Name = c.Name,
                        Id = c.Id,
                        Height = c.Height != null ? c.Height : null,
                        Width = c.Width != null ? c.Width : null,
                        Abbreviation = c.Abbreviation,
                    });

                return Sizes.ToList();

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<string> DeleteClothSize(int Id)
        {
            try
            {
                var IsDeleted = await _unitOfWork.ClothSizeRepository.RemoveAsync(Id);
                if (IsDeleted)
                {
                    await _unitOfWork.SaveAsync();
                    return CommonMessages.SUCCESSFULLY_DELETING;
                }
                return CommonMessages.ITEM_NOT_EXISTS;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<EnumUpdatingResult> UpdateClothSize(ClothSizeDTO clothSize)
        {
            try
            {
                var Size = await _unitOfWork.ClothSizeRepository.GetClothSizeByName(clothSize.Name.Trim(), clothSize.Id);

                if (Size == null)
                {
                    var ClothSize = _mapper.Map<ClothSizeDTO, ClothSizeModel>(clothSize);
                    ClothSize.ModifiedBy = _userIdentity.Name;
                    ClothSize.ModifiedDate = DateTime.Now;
                    await _unitOfWork.ClothSizeRepository.UpdateClothSize(ClothSize);
                    await _unitOfWork.SaveAsync();
                    return EnumUpdatingResult.Successed;
                }

                return EnumUpdatingResult.Failed;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ClothSizeDTO> GetClothSize(int Id)
        {
            try
            {
                var Model = await _unitOfWork.ClothSizeRepository.GetAsync(Id);
                return _mapper.Map<ClothSizeModel, ClothSizeDTO>(Model);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<GetClothSizeDTO>> GetClothSizes(int ClothId)
        {
            try
            {
                var Sizes = (await _unitOfWork.ClothSizeRepository.GetAllAsync()).
                    ToList();
                var SizesClothes = Sizes.Select(c =>
                    new GetClothSizeDTO
                    {
                        Width = (int)c.Width,
                        Height = (int)c.Height,
                        Id = c.Id,
                        Name = c.Name
                    });
                return SizesClothes.ToList();

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

    }
}
