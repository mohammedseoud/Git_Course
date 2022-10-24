using ElBayt.Common.Common;
using ElBayt.Common.Core.Logging;
using ElBayt.Common.Core.Mapping;
using ElBayt.Common.Core.SecurityModels;
using ElBayt.Common.Enums;
using ElBayt.Core.IUnitOfWork;
using ElBayt.DTO.ELBayt.DBDTOs;
using ElBayt.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ElBayt.Core.Models;
using ElBayt.DTO.ELBayt.DTOs;
using System.Linq;

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

        #region Colors
        public async Task<EnumInsertingResult> AddNewColor(ColorDTO Color)
        {
    
            try
            {
                var identityName = _userIdentity?.Name ?? "Unknown";
                var _color = await _unitOfWork.ColorRepository.GetColorByName(Color.Name.Trim(), Color.Id); ;
                if (_color == null)
                {
                    var Entity = _mapper.Map<ColorDTO, ColorModel>(Color);
                    Entity.CreatedDate = DateTime.Now;
                    Entity.CreatedBy = identityName;
                    await _unitOfWork.ColorRepository.AddAsync(Entity);
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

        public object GetSizes()
        {
            try
            {
                return _unitOfWork.ColorRepository.GetAll();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<string> DeleteColor(int Id)
        {
            try
            {
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
                return ex.Message;
            }
        }

        public async Task<EnumUpdatingResult> UpdateColor(ColorDTO Color)
        {
            try
            {
                var _color = await _unitOfWork.ColorRepository.GetColorByName(Color.Name.Trim(), Color.Id);

                if (_color == null)
                {
                    var identityName = _userIdentity?.Name ?? "Unknown";
                    var color = _mapper.Map<ColorDTO, ColorModel>(Color);
                    color.ModifiedDate = DateTime.Now;
                    color.ModifiedBy = identityName;
                    await _unitOfWork.ColorRepository.UpdateColor(color);
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

        public async Task<ColorDTO> GetColor(int Id)
        {
            try
            {
                var Model = await _unitOfWork.ColorRepository.GetAsync(Id);
                return _mapper.Map<ColorModel, ColorDTO>(Model);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<GetColorDTO>> GetColors()
        {
            try
            {
                var Colors = (await _unitOfWork.ColorRepository.GetAllAsync()).ToList().
                    Select(c => new GetColorDTO
                    {
                        Id = c.Id,
                        Name = c.Name
                    });
               
                return  Colors.ToList();

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion
    }
}
