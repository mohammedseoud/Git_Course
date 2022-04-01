using AutoMapper;
using ElBayt.Common.Common;
using ElBayt.Common.Core.Logging;
using ElBayt.Common.Core.Mapping;
using ElBayt.Common.Core.SecurityModels;
using ElBayt.Common.Enums;
using ElBayt.Common.Security;
using ElBayt.Core.Entities;
using ElBayt.Core.IUnitOfWork;
using ElBayt.DTO.ELBayt.DBDTOs;
using ElBayt.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ElBayt.Services.Implementations
{
    public class GeneralService : IGeneralService
    {
        private readonly IELBaytUnitOfWork _unitOfWork;
        private readonly IUserIdentity _userIdentity;
        private readonly ILogger _logger;
        private readonly ITypeMapper _mapper;

        public GeneralService(IELBaytUnitOfWork unitOfWork, IUserIdentity userIdentity, ILogger logger,
              ITypeMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _userIdentity = userIdentity ?? throw new ArgumentNullException(nameof(userIdentity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #region Sizes
        public async Task<EnumInsertingResult> AddNewColor(ColorDTO Color)
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info

                _logger.InfoInDetail(Color, correlationGuid, nameof(ClothesService), nameof(AddNewColor), 1, _userIdentity.Name);

                #endregion Logging info

                var Size = await _unitOfWork.ColorRepository.GetColorByName(Color.Name.Trim(), Color.Id); ;
                if (Size == null)
                {
                    var Entity = _mapper.Map<ColorDTO, ColorEntity>(Color);
                    Entity.Id = Guid.NewGuid();
                    await _unitOfWork.ColorRepository.AddAsync(Entity);
                    await _unitOfWork.SaveAsync();
                    return EnumInsertingResult.Successed;
                }
                return EnumInsertingResult.Failed;
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail(Color, correlationGuid, $"{nameof(ClothesService)}_{nameof(AddNewColor)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

                #endregion Logging info

                throw;
            }
        }

        public object GetSizes()
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info

                _logger.InfoInDetail("GetSizes", correlationGuid, nameof(ClothesService), nameof(GetSizes), 1, _userIdentity.Name);

                #endregion Logging info

                return _unitOfWork.ColorRepository.GetAll();

            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail("GetColors", correlationGuid, $"{nameof(ClothesService)}_{nameof(GetSizes)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

                #endregion Logging info

                throw;
            }
        }

        public async Task<string> DeleteColor(Guid Id)
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info

                _logger.InfoInDetail(Id, correlationGuid, nameof(ClothesService), nameof(DeleteColor), 1, _userIdentity.Name);

                #endregion Logging info

                var IsDeleted = await _unitOfWork.ColorRepository.RemoveAsync(Id);
                if (IsDeleted)
                {
                    await _unitOfWork.SaveAsync();
                    return CommonMessages.SUCCESSFULLY_DELETING;
                }
                return CommonMessages.ITEM_NOT_EXISTS;
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail(Id, correlationGuid, $"{nameof(ClothesService)}_{nameof(DeleteColor)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

                #endregion Logging info

                return ex.Message;
            }
        }

        public async Task<EnumUpdatingResult> UpdateColor(ColorDTO Color)
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info

                _logger.InfoInDetail(Color, correlationGuid, nameof(ClothesService), nameof(UpdateColor), 1, _userIdentity.Name);

                #endregion Logging info

                var _color = await _unitOfWork.ColorRepository.GetColorByName(Color.Name.Trim(), Color.Id);

                if (_color == null)
                {
                    var color = _mapper.Map<ColorDTO, ColorEntity>(Color);
                    await _unitOfWork.ColorRepository.UpdateColor(color);
                    await _unitOfWork.SaveAsync();
                    return EnumUpdatingResult.Successed;
                }

                return EnumUpdatingResult.Failed;
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail(Color, correlationGuid, $"{nameof(ClothesService)}_{nameof(UpdateColor)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

                #endregion Logging info

                throw;
            }
        }

        public async Task<ColorDTO> GetColor(Guid Id)
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info

                _logger.InfoInDetail(Id, correlationGuid, nameof(ClothesService), nameof(GetColor), 1, _userIdentity.Name);

                #endregion Logging info

                var Model = await _unitOfWork.ColorRepository.GetAsync(Id);
                return _mapper.Map<ColorEntity, ColorDTO>(Model);
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail(Id, correlationGuid, $"{nameof(ClothesService)}_{nameof(GetColor)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

                #endregion Logging info

                throw;
            }
        }

        public object GetColors()
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info

                _logger.InfoInDetail("GetColors", correlationGuid, nameof(ClothesService), nameof(GetColors), 1, _userIdentity.Name);

                #endregion Logging info

                return  _unitOfWork.ColorRepository.GetAll();

            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail("GetColors", correlationGuid, $"{nameof(ClothesService)}_{nameof(GetColors)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

                #endregion Logging info

                throw;
            }
        }
        #endregion
    }
}
