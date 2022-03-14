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
using ElBayt.DTO.ELBayt.DBUDTDTOs;
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

        #region Departments
        public async Task<EnumInsertingResult> AddNewClothDepartment(ClothDepartmentDTO clothDepartment)
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info

                _logger.InfoInDetail(clothDepartment, correlationGuid, nameof(ClothesService), nameof(AddNewClothDepartment), 1, _userIdentity.Name);

                #endregion Logging info

                var Department = await _unitOfWork.ClothDepartmentRepository.GetClothDepartmentByName(clothDepartment.Name.Trim(), clothDepartment.Id);
                if (Department == null)
                {
                    var Entity = _mapper.Map<ClothDepartmentDTO, ClothDepartmentEntity>(clothDepartment);
                    Entity.Id = Guid.NewGuid();
                    await _unitOfWork.ClothDepartmentRepository.AddAsync(Entity);
                    await _unitOfWork.SaveAsync();
                    return EnumInsertingResult.Successed;
                }

                return EnumInsertingResult.Failed;
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail(clothDepartment, correlationGuid, $"{nameof(ClothesService)}_{nameof(AddNewClothDepartment)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

                #endregion Logging info

                throw;
            }
        }

        public async Task<string> DeleteClothDepartment(Guid id)
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info

                _logger.InfoInDetail(id, correlationGuid, nameof(ClothesService), nameof(DeleteClothDepartment), 1, _userIdentity.Name);

                #endregion Logging info

                var SPParameters = new DynamicParameters();
                SPParameters.Add("@DepartmentId", id);
                return await _unitOfWork.SP.OneRecordAsnyc<string>(StoredProcedure.DELETEPRODUCTDEPARTMENT, SPParameters);
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail(id, correlationGuid, $"{nameof(ClothesService)}_{nameof(DeleteClothDepartment)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

                #endregion Logging info

                return ex.Message;
            }
        }

        public object GetClothDepartments()
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info

                _logger.InfoInDetail("GetClothDepartments", correlationGuid, nameof(ClothesService), nameof(GetClothDepartments), 1, _userIdentity.Name);

                #endregion Logging info

                return _unitOfWork.ClothDepartmentRepository.GetAll();

            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail("GetClothDepartments", correlationGuid, $"{nameof(ClothesService)}_{nameof(GetClothDepartments)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

                #endregion Logging info

                throw;
            }
        }

        public async Task<EnumUpdatingResult> UpdateClothDepartment(ClothDepartmentDTO ClothDepartment)
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info

                _logger.InfoInDetail(ClothDepartment, correlationGuid, nameof(ClothesService), nameof(UpdateClothDepartment), 1, _userIdentity.Name);

                #endregion Logging info

                var Department = await _unitOfWork.ClothDepartmentRepository.GetClothDepartmentByName(ClothDepartment.Name.Trim(), ClothDepartment.Id);
                if (Department == null)
                {
                    var Entity = _mapper.Map<ClothDepartmentDTO, ClothDepartmentEntity>(ClothDepartment);
                    await _unitOfWork.ClothDepartmentRepository.UpdateClothDepartment(Entity);
                    await _unitOfWork.SaveAsync();
                    return EnumUpdatingResult.Successed;
                }
                return EnumUpdatingResult.Failed;
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail(ClothDepartment, correlationGuid, $"{nameof(ClothesService)}_{nameof(UpdateClothDepartment)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

                #endregion Logging info

                throw;
            }
        }

        public async Task<ClothDepartmentDTO> GetClothDepartment(Guid Id)
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info

                _logger.InfoInDetail(Id, correlationGuid, nameof(ClothesService), nameof(GetClothDepartment), 1, _userIdentity.Name);

                #endregion Logging info

                var Model = await _unitOfWork.ClothDepartmentRepository.GetAsync(Id);
                return _mapper.Map<ClothDepartmentEntity, ClothDepartmentDTO>(Model);
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail(Id, correlationGuid, $"{nameof(ClothesService)}_{nameof(GetClothDepartment)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

                #endregion Logging info

                throw;
            }
        }

        #endregion

        #region Cloth

        public async Task<NumberClothDTO> AddNewCloth(IFormCollection Form, string DiskDirectory)
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info
                _logger.InfoInDetail(Form, correlationGuid, nameof(ClothesService), nameof(AddNewCloth), 1, _userIdentity.Name);
                #endregion Logging info

                var Cloth = await _unitOfWork.ClothRepository.GetClothByName(Form["Name"].ToString().Trim(), Guid.NewGuid());
                if (Cloth == null)
                {

                    var identityName = _userIdentity?.Name ?? "Unknown";

                    decimal PriceAfterDiscount;
                    var cloth = new ClothDTO
                    {
                        Id = Guid.NewGuid(),
                        CreatedBy = identityName,
                        CreatedDate = DateTime.Now,
                        ModifiedBy = identityName,
                        ModifiedDate = DateTime.Now,
                        Price = Form["Price"].ToString(),
                        PriceAfterDiscount = decimal.TryParse(Form["PriceAfterDiscount"].ToString(), out PriceAfterDiscount) ? PriceAfterDiscount.ToString() : null,
                        Name = Form["Name"].ToString(),
                        Description = Form["Description"].ToString(),
                     
                    };
                    var ClothEntity = _mapper.Map<ClothDTO, UTDClothDTO>(cloth);
                    var Clothes = new List<UTDClothDTO>
                    {
                       ClothEntity
                    };
                    var image1 = new ClothImageDTO
                    {
                        Id = Guid.NewGuid(),
                        ClothId = cloth.Id,
                        CreatedBy = identityName,
                        CreatedDate = DateTime.Now,
                        ModifiedBy = identityName,
                        ModifiedDate = DateTime.Now
                    };



                    var Image1Entity = _mapper.Map<ClothImageDTO, UTDClothImageDTO>(image1);

                    var Images1 = new List<UTDClothImageDTO>
                {
                    Image1Entity
                };
                    var Images2 = new List<UTDClothImageDTO>();
                    if (Form.Files.Count > 1)
                    {
                        var image2 = new ClothImageDTO
                        {
                            Id = Guid.NewGuid(),
                            ClothId = cloth.Id,
                            CreatedBy = identityName,
                            CreatedDate = DateTime.Now,
                            ModifiedBy = identityName,
                            ModifiedDate = DateTime.Now
                        };
                        var Image2Entity = _mapper.Map<ClothImageDTO, UTDClothImageDTO>(image2);

                        Images2 = new List<UTDClothImageDTO>
                {
                    Image2Entity
                };
                    }
                    var image1table = ObjectDatatableConverter.ToDataTable(Images1);
                    var image2table = ObjectDatatableConverter.ToDataTable(Images2);
                    var Producttable = ObjectDatatableConverter.ToDataTable(Clothes);
                    var SPParameters = new DynamicParameters();
                    SPParameters.Add("@UDTProduct", Producttable.AsTableValuedParameter(UDT.UDTPRODUCT));
                    SPParameters.Add("@UDTProductImage", image1table.AsTableValuedParameter(UDT.UDTPRODUCTIMAGE));
                    SPParameters.Add("@UDTProductImage2", image2table.AsTableValuedParameter(UDT.UDTPRODUCTIMAGE));
                    SPParameters.Add("@Extension1", Path.GetExtension(Form.Files[0].FileName));
                    if (Form.Files.Count > 1)
                        SPParameters.Add("@Extension2", Path.GetExtension(Form.Files[1].FileName));
                    else
                        SPParameters.Add("@Extension2", General.NOEXTENSION);

                    SPParameters.Add("@DiskDirectory", DiskDirectory);



                    var List = await _unitOfWork.SP.ListAsnyc<NumberClothDTO>(StoredProcedure.ADDPRODUCT, SPParameters);
                    return List.FirstOrDefault();
                }
                return null;
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail(Form, correlationGuid, $"{nameof(ClothesService)}_{nameof(AddNewCloth)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

                #endregion Logging info

                throw;
            }
        }

        public object GetClothes()
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info

                _logger.InfoInDetail("GetClothes", correlationGuid, nameof(ClothesService), nameof(GetClothes), 1, _userIdentity.Name);

                #endregion Logging info

                return _unitOfWork.ClothRepository.GetAll();

            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail("GetClothes", correlationGuid, $"{nameof(ClothesService)}_{nameof(GetClothes)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

                #endregion Logging info

                throw;
            }
        }

        public async Task<string> DeleteCloth(Guid Id)
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info

                _logger.InfoInDetail(Id, correlationGuid, nameof(ClothesService), nameof(DeleteCloth), 1, _userIdentity.Name);

                #endregion Logging info

                var SPParameters = new DynamicParameters();
                SPParameters.Add("@ProductId", Id);
                return await _unitOfWork.SP.OneRecordAsnyc<string>(StoredProcedure.DELETEPRODUCT, SPParameters);
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail(Id, correlationGuid, $"{nameof(ClothesService)}_{nameof(DeleteCloth)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

                #endregion Logging info

                return ex.Message;
            }
        }

        public async Task<NumberClothDTO> UpdateCloth(IFormCollection Form, string DiskDirectory)
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info

                _logger.InfoInDetail(Form, correlationGuid, nameof(ClothesService), nameof(UpdateCloth), 1, _userIdentity.Name);

                #endregion Logging info



                var cloth = await _unitOfWork.ClothRepository.GetClothByName(Form["Name"].ToString().Trim(), Guid.Parse(Form["Id"].ToString()));

                if (cloth == null)
                {
                    var identityName = _userIdentity?.Name ?? "Unknown";

                    var Newcloth = new ClothDTO
                    {
                        Id = Guid.Parse(Form["Id"].ToString()),
                        ModifiedBy = identityName,
                        ModifiedDate = DateTime.Now,
                        Price = Form["Price"].ToString(),
                        PriceAfterDiscount = !string.IsNullOrEmpty(Form["PriceAfterDiscount"].ToString()) && Form["PriceAfterDiscount"].ToString() != "null" ? Form["PriceAfterDiscount"].ToString() : null,
                        Name = Form["Name"].ToString(),
                        Description = Form["Description"].ToString(),
                    };

                    if (!string.IsNullOrEmpty(Form["Img1"]))
                    {
                        var url = Form["Img1"].ToString().Split(".")[0] + Path.GetExtension(Form.Files[0].FileName);
                        Newcloth.ProductImageURL1 = url;
                    }
                    string Extention2 = null;
                    if (Form.Files.Count > 0)
                    {
                        if (Form.Files[0].Name == "ImgFile2")
                            Extention2 = Path.GetExtension(Form.Files[0].FileName);
                        if (Form.Files.Count > 1)
                        {
                            if (Form.Files[1].Name == "ImgFile2")
                                Extention2 = Path.GetExtension(Form.Files[1].FileName);
                        }
                        if (Extention2 == null)
                        {
                            if (!string.IsNullOrEmpty(Newcloth.ProductImageURL2))
                            {
                                var url = Newcloth.ProductImageURL2.Split(".")[0] + Extention2;
                                Newcloth.ProductImageURL2 = url;
                            }
                        }
                    }

                    var UTDCloth = _mapper.Map<ClothDTO, UTDClothDTO>(Newcloth);
                    var Clothes = new List<UTDClothDTO>
                    {
                       UTDCloth
                    };

                    var Clothtable = ObjectDatatableConverter.ToDataTable(Clothes);
                    var SPParameters = new DynamicParameters();
                    SPParameters.Add("@UDTProduct", Clothtable.AsTableValuedParameter(UDT.UDTPRODUCT));
                    if (!string.IsNullOrEmpty(Extention2))
                        SPParameters.Add("@Img2Id", Guid.NewGuid());
                    else
                        SPParameters.Add("@Img2Id", Guid.Empty);
                    SPParameters.Add("@Extention2", Extention2);
                    SPParameters.Add("@DiskDirectory", DiskDirectory);


                    var List = await _unitOfWork.SP.ThreeListAsnyc<NumberClothDTO, string, string>(StoredProcedure.UPDATEPRODUCT, SPParameters);
                    var URL1 = Path.Combine(DiskDirectory, List.Item2.FirstOrDefault());
                    var URL2 = Path.Combine(DiskDirectory, List.Item3.FirstOrDefault());
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

                    return List.Item1.FirstOrDefault();
                }
                return null;
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail(Form, correlationGuid, $"{nameof(ClothesService)}_{nameof(UpdateCloth)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

                #endregion Logging info

                throw;
            }
        }

        public async Task<NumberClothDTO> GetCloth(Guid Id)
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info

                _logger.InfoInDetail(Id, correlationGuid, nameof(ClothesService), nameof(GetCloth), 1, _userIdentity.Name);

                #endregion Logging info

                var Model = await _unitOfWork.ClothRepository.GetAsync(Id);
                return _mapper.Map<ClothEntity, NumberClothDTO>(Model);
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail(Id, correlationGuid, $"{nameof(ClothesService)}_{nameof(GetCloth)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

                #endregion Logging info

                throw;
            }
        }

        public async Task<ClothImageDTO> SaveClothImage(string ClothId, IFormFile file
            , string DiskDirectory)
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info

                _logger.InfoInDetail(file, correlationGuid, nameof(ClothesService), nameof(SaveClothImage), 1, _userIdentity.Name);

                #endregion Logging info

                var identityName = _userIdentity?.Name ?? "Unknown";

                var image = new ClothImageDTO
                {
                    Id = Guid.NewGuid(),
                    ClothId = Guid.Parse(ClothId),
                    CreatedBy = identityName,
                    CreatedDate = DateTime.Now,
                    ModifiedBy = identityName,
                    ModifiedDate = DateTime.Now
                };


                var Entity = _mapper.Map<ClothImageDTO  , UTDClothImageDTO>(image);
                var Images = new List<UTDClothImageDTO>
                {
                    Entity
                };
                var table = ObjectDatatableConverter.ToDataTable(Images);

                var SPParameters = new DynamicParameters();
                SPParameters.Add("@UDTProductImage", table.AsTableValuedParameter(UDT.UDTPRODUCTIMAGE));
                SPParameters.Add("@Extension", Path.GetExtension(file.FileName));
                SPParameters.Add("@DiskDirectory", DiskDirectory);

                var Imglist = await _unitOfWork.SP.ListAsnyc<UTDClothImageDTO>(StoredProcedure.ADDPRODUCTIMAGE, SPParameters);
                var imageDTO = _mapper.Map<UTDProductImageDTO, ClothImageDTO>(Imglist.FirstOrDefault());
                return imageDTO;


            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail(file, correlationGuid, $"{nameof(ClothesService)}_{nameof(SaveClothImage)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

                #endregion Logging info

                throw;
            }
        }

        public async Task<ClothImageDTO> GetClothImage(Guid Id)
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info

                _logger.InfoInDetail(Id, correlationGuid, nameof(ClothesService), nameof(SaveClothImage), 1, _userIdentity.Name);

                #endregion Logging info


                var entity = await _unitOfWork.ClothImageRepository.GetAsync(Id);
                return _mapper.Map<ClothImageEntity, ClothImageDTO>(entity);
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail(Id, correlationGuid, $"{nameof(ClothesService)}_{nameof(SaveClothImage)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

                #endregion Logging info

                throw;
            }
        }

        public async Task<List<ClothImageDTO>> GetClothImages(Guid ClothId)
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info

                _logger.InfoInDetail(ClothId, correlationGuid, nameof(ClothesService), nameof(GetClothImages), 1, _userIdentity.Name);

                #endregion Logging info

                var SPParameters = new DynamicParameters();
                SPParameters.Add("@ProductId", ClothId);


                return (await _unitOfWork.SP.ListAsnyc<ClothImageDTO>(StoredProcedure.GETPRODUCTIMAGES, SPParameters)).ToList();
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail(ClothId, correlationGuid, $"{nameof(ClothesService)}_{nameof(GetClothImages)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

                #endregion Logging info

                throw;
            }
        }

        public async Task<string> DeleteClothImage(Guid ImageId)
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info

                _logger.InfoInDetail(ImageId, correlationGuid, nameof(ClothesService), nameof(DeleteClothImage), 1, _userIdentity.Name);

                #endregion Logging info

                var IsDeleted = await _unitOfWork.ClothImageRepository.RemoveAsync(ImageId);
                if (IsDeleted)
                {
                    var res = await _unitOfWork.SaveAsync();
                    return "true";

                }
                return "This Image Not Exist";
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail(ImageId, correlationGuid, $"{nameof(ClothesService)}_{nameof(DeleteClothImage)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

                #endregion Logging info

                return ex.Message;
            }
        }

        public async Task<string> DeleteClothImageByURL(string URL)
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info

                _logger.InfoInDetail(URL, correlationGuid, nameof(ClothesService), nameof(DeleteClothImageByURL), 1, _userIdentity.Name);

                #endregion Logging info

                var IsDeleted = _unitOfWork.ClothImageRepository.DeleteByURL(URL);
                if (IsDeleted)
                {
                    var res = await _unitOfWork.SaveAsync();
                    return "true";

                }
                return "This Image Not Exist";
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail(URL, correlationGuid, $"{nameof(ClothesService)}_{nameof(DeleteClothImageByURL)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

                #endregion Logging info

                return ex.Message;
            }

        }
        #endregion

    }
}
