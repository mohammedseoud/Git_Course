using AutoMapper;
using Dapper;
using ElBayt.Common.Common;
using ElBayt.Common.Core.Logging;
using ElBayt.Common.Core.Mapping;
using ElBayt.Common.Core.SecurityModels;
using ElBayt.Common.Enums;
using ElBayt.Common.Utilities;
using ElBayt.Core.Entities;
using ElBayt.Core.IUnitOfWork;
using ElBayt.Core.Mapping;
using ElBayt.DTO.ELBayt.DBDTOs;
using ElBayt.DTO.ELBayt.DTOs;
using ElBayt.Infra.SPs;
using ElBayt.Services.Contracts;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ElBayt.Services.Implementations
{
    public class ClothesService : IClothesService
    {
        private readonly IELBaytUnitOfWork _unitOfWork;
        private readonly IUserIdentity _userIdentity;
        private readonly ILogger _logger;
        private readonly ITypeMapper _mapper;

        public ClothesService(IELBaytUnitOfWork unitOfWork, IUserIdentity userIdentity, ILogger logger,
              ITypeMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _userIdentity = userIdentity ?? throw new ArgumentNullException(nameof(userIdentity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        #region Types

        public async Task<ClothTypeDTO> AddNewClothType(IFormCollection Form, string DiskDirectory)
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info
                _logger.InfoInDetail(Form, correlationGuid, nameof(ClothesService), nameof(AddNewClothType), 1, _userIdentity.Name);
                #endregion Logging info

                var ClothType = await _unitOfWork.ClothTypeRepository.GetClothTypeByName(Form["Name"].ToString().Trim(), Guid.NewGuid());
                if (ClothType == null)
                {

                    var identityName = _userIdentity?.Name ?? "Unknown";

                    var clothtype = new ClothTypeDTO
                    {
                        Id = Guid.NewGuid(),
                        CreatedBy = identityName,
                        CreatedDate = DateTime.Now,
                        ModifiedBy = identityName,
                        ModifiedDate = DateTime.Now,
                        Name = Form["Name"].ToString(),
                    };
                    var clothtypeEntity = _mapper.Map<ClothTypeDTO, UTDClothTypeDTO>(clothtype);
                    var clothtypes = new List<UTDClothTypeDTO>
                        {
                           clothtypeEntity
                        };
                   
                    var clothtypetable = ObjectDatatableConverter.ToDataTable(clothtypes);
                    var SPParameters = new DynamicParameters();
                    SPParameters.Add("@UDTClothType", clothtypetable.AsTableValuedParameter(UDT.UDTCLOTHTYPE));
                    SPParameters.Add("@Extension", Path.GetExtension(Form.Files[0].FileName));
                    SPParameters.Add("@DiskDirectory", DiskDirectory);

                    var List = await _unitOfWork.SP.ListAsnyc<ClothTypeDTO>(StoredProcedure.ADDCLOTHTYPE, SPParameters);
                    return List.FirstOrDefault();
                }
                return null;
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail(Form, correlationGuid, $"{nameof(ClothesService)}_{nameof(AddNewClothType)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

                #endregion Logging info

                throw;
            }
        }

        public object GetClothTypes()
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info

                _logger.InfoInDetail("GetClothTypes", correlationGuid, nameof(ClothesService), nameof(GetClothTypes), 1, _userIdentity.Name);

                #endregion Logging info

                return _unitOfWork.ClothTypeRepository.GetAll();

            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail("GetClothTypes", correlationGuid, $"{nameof(ClothesService)}_{nameof(GetClothTypes)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

                #endregion Logging info

                throw;
            }
        }

        public async Task<string> DeleteClothType(Guid Id)
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info

                _logger.InfoInDetail(Id, correlationGuid, nameof(ClothesService), nameof(DeleteClothType), 1, _userIdentity.Name);

                #endregion Logging info

                var SPParameters = new DynamicParameters();
                SPParameters.Add("@TypeId", Id);
                return await _unitOfWork.SP.OneRecordAsnyc<string>(StoredProcedure.DELETEPRODUCTTYPE, SPParameters);
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail(Id, correlationGuid, $"{nameof(ClothesService)}_{nameof(DeleteClothType)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

                #endregion Logging info

                return ex.Message;
            }
        }

        public async Task<EnumUpdatingResult> UpdateClothType(ClothTypeDTO clothType, string DiskDirectory)
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info

                _logger.InfoInDetail(clothType, correlationGuid, nameof(ClothesService), nameof(UpdateClothType), 1, _userIdentity.Name);

                #endregion Logging info

                var _clothType = await _unitOfWork.ClothTypeRepository.GetClothTypeByName(clothType.Name.Trim(), clothType.Id);
                if (_clothType == null)
                {
                    var UTDClothType = _mapper.Map<ClothTypeDTO, UTDClothTypeDTO>(clothType);
                    var ClothTypes = new List<UTDClothTypeDTO>
                    {
                       UTDClothType
                    };

                    var clothTypetable = ObjectDatatableConverter.ToDataTable(ClothTypes);
                    var SPParameters = new DynamicParameters();
                    SPParameters.Add("@UDTClothType", clothTypetable.AsTableValuedParameter(UDT.UDTPRODUCTTYPE));
                    var URL = await _unitOfWork.SP.ListAsnyc<string, string>(StoredProcedure.UPDATEPRODUCTTYPE, SPParameters);
                    var URL1 = Path.Combine(DiskDirectory, URL.Item1.FirstOrDefault());
                    var URL2 = Path.Combine(DiskDirectory, URL.Item2.FirstOrDefault());
                    if (URL1 != URL2)
                    {
                        if (Directory.Exists(URL1))
                        {
                            if (!Directory.Exists(URL2))
                            {
                                Directory.CreateDirectory(URL2);
                                var Directories = Directory.GetDirectories(URL2);
                                Directory.Delete(URL2);
                                if (Directories.Length > 0)
                                    Directory.CreateDirectory(Directories[^1]);
                            }
                            Directory.Move(URL1, URL2);
                        }
                    }

                    return EnumUpdatingResult.Successed;
                }

                return EnumUpdatingResult.Failed;
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail(clothType, correlationGuid, $"{nameof(ProductService)}_{nameof(UpdateClothType)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

                #endregion Logging info

                throw;
            }
        }

        public async Task<ClothTypeDTO> GetClothType(Guid Id)
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info

                _logger.InfoInDetail(Id, correlationGuid, nameof(ProductService), nameof(GetClothType), 1, _userIdentity.Name);

                #endregion Logging info

                var Model = await _unitOfWork.ClothTypeRepository.GetAsync(Id);
                return _mapper.Map<ClothTypeEntity, ClothTypeDTO>(Model);
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail(Id, correlationGuid, $"{nameof(ProductService)}_{nameof(GetClothType)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

                #endregion Logging info

                throw;
            }
        }

        #endregion

        #region Categories
        public async Task<EnumInsertingResult> AddNewClothCategory(ClothCategoryDTO clothCategory)
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info

                _logger.InfoInDetail(clothCategory, correlationGuid, nameof(ClothesService), nameof(AddNewClothCategory), 1, _userIdentity.Name);

                #endregion Logging info

                var Category = await _unitOfWork.ClothCategoryRepository.GetClothCategoryByName(clothCategory.Name.Trim(), clothCategory.Id);;
                if (Category == null)
                {
                    var Entity = _mapper.Map<ClothCategoryDTO, ClothCategoryEntity>(clothCategory);
                    Entity.Id = Guid.NewGuid();
                    await _unitOfWork.ClothCategoryRepository.AddAsync(Entity);
                    await _unitOfWork.SaveAsync();
                    return EnumInsertingResult.Successed;
                }
                return EnumInsertingResult.Failed;
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail(clothCategory, correlationGuid, $"{nameof(ProductService)}_{nameof(AddNewClothCategory)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

                #endregion Logging info

                throw;
            }
        }

        public object GetClothCategories()
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info

                _logger.InfoInDetail("GetClothCategories", correlationGuid, nameof(ClothesService), nameof(GetClothCategories), 1, _userIdentity.Name);

                #endregion Logging info

                return _unitOfWork.ClothCategoryRepository.GetAll();

            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail("GetClothCategories", correlationGuid, $"{nameof(ClothesService)}_{nameof(GetClothCategories)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

                #endregion Logging info

                throw;
            }
        }

        public async Task<string> DeleteClothCategory(Guid Id)
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info

                _logger.InfoInDetail(Id, correlationGuid, nameof(ClothesService), nameof(DeleteClothCategory), 1, _userIdentity.Name);

                #endregion Logging info

                var SPParameters = new DynamicParameters();
                SPParameters.Add("@CategoryId", Id);
                return await _unitOfWork.SP.OneRecordAsnyc<string>(StoredProcedure.DELETEPRODUCTCATEGORY, SPParameters);
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail(Id, correlationGuid, $"{nameof(ClothesService)}_{nameof(DeleteClothCategory)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

                #endregion Logging info

                return ex.Message;
            }
        }

        public async Task<EnumUpdatingResult> UpdateClothCategory(ClothCategoryDTO clothCategory)
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info

                _logger.InfoInDetail(clothCategory, correlationGuid, nameof(ClothesService), nameof(UpdateClothCategory), 1, _userIdentity.Name);

                #endregion Logging info

                var Category = await _unitOfWork.ClothCategoryRepository.GetClothCategoryByName(clothCategory.Name.Trim(), clothCategory.Id);

                if (Category == null)
                {
                    var ClothCategory = _mapper.Map<ClothCategoryDTO, ClothCategoryEntity>(clothCategory);
                    await _unitOfWork.ClothCategoryRepository.UpdateClothCategory(ClothCategory);

                    return EnumUpdatingResult.Successed;
                }

                return EnumUpdatingResult.Failed;
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail(clothCategory, correlationGuid, $"{nameof(ClothesService)}_{nameof(UpdateClothCategory)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

                #endregion Logging info

                throw;
            }
        }

        public async Task<ClothCategoryDTO> GetClothCategory(Guid Id)
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info

                _logger.InfoInDetail(Id, correlationGuid, nameof(ClothesService), nameof(GetClothCategory), 1, _userIdentity.Name);

                #endregion Logging info

                var Model = await _unitOfWork.ClothCategoryRepository.GetAsync(Id);
                return _mapper.Map<ClothCategoryEntity, ClothCategoryDTO>(Model);
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail(Id, correlationGuid, $"{nameof(ClothesService)}_{nameof(GetClothCategory)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

                #endregion Logging info

                throw;
            }
        }

        #endregion
    }
}
