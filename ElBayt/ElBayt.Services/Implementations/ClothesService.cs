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
        private readonly IFileMapper _filemapper;

        public ClothesService(IELBaytUnitOfWork unitOfWork, IUserIdentity userIdentity, ILogger logger,
              ITypeMapper mapper, IFileMapper filemapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _userIdentity = userIdentity ?? throw new ArgumentNullException(nameof(userIdentity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _filemapper = filemapper ?? throw new ArgumentNullException(nameof(filemapper));
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
                        ClothDepartmentId = Guid.Parse(Form["ClothDepartmentId"].ToString())
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
                return await _unitOfWork.SP.OneRecordAsnyc<string>(StoredProcedure.DELETECLOTHTYPE, SPParameters);
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail(Id, correlationGuid, $"{nameof(ClothesService)}_{nameof(DeleteClothType)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

                #endregion Logging info

                return ex.Message;
            }
        }

        public async Task<EnumUpdatingResult> UpdateClothType(IFormCollection Form, string DiskDirectory,string machineDirectory)
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info

                _logger.InfoInDetail(Form, correlationGuid, nameof(ClothesService), nameof(UpdateClothDepartment), 1, _userIdentity.Name);

                #endregion Logging info

                var _clothtype = await _unitOfWork.ClothTypeRepository.GetClothTypeByName(Form["Name"].ToString().Trim(), Guid.Parse(Form["Id"].ToString()));
                if (_clothtype == null)
                {
                    var identityName = _userIdentity?.Name ?? "Unknown";
                    var clothtype = new ClothTypeDTO
                    {
                        Id = Guid.Parse(Form["Id"].ToString()),
                        ModifiedBy = identityName,
                        ModifiedDate = DateTime.Now,
                        Name = Form["Name"].ToString(),
                        ClothDepartmentId = Guid.Parse(Form["ClothDepartmentId"].ToString())
                    };

                    var UTDClothType = _mapper.Map<ClothTypeDTO, UTDClothTypeDTO>(clothtype);
                    var ClothTypes = new List<UTDClothTypeDTO>
                    {
                       UTDClothType
                    };

                    var clothTypetable = ObjectDatatableConverter.ToDataTable(ClothTypes);
                    var SPParameters = new DynamicParameters();
                    SPParameters.Add("@UDTClothType", clothTypetable.AsTableValuedParameter(UDT.UDTCLOTHTYPE));
                    if (Form.Files.Count > 0)
                        SPParameters.Add("@Extension", Path.GetExtension(Form.Files[0].FileName));
                    else
                        SPParameters.Add("@Extension", null);

                    SPParameters.Add("@DiskDirectory", DiskDirectory);
                    var URL = await _unitOfWork.SP.ListAsnyc<string, string>(StoredProcedure.UPDATECLOTHTYPE, SPParameters);
                    var URL1 = Path.Combine(machineDirectory, URL.Item1.FirstOrDefault());
                    var URL2 = Path.Combine(machineDirectory, URL.Item2.FirstOrDefault());

                    if (URL1 != URL2)
                        _filemapper.MoveDataBetweenTwoFile(URL1, URL2);
                

                    if (Form.Files.Count > 0)
                    {
                        var files1 = URL1.Split("\\");
                        var files2 = URL2.Split("\\");
                        var URL2Directory = URL2.Remove(URL2.IndexOf(files2[^1]));
                        File.Delete(URL2Directory + files1[^1]);

                        using var stream = new FileStream(URL2, FileMode.Create);
                        Form.Files[0].CopyTo(stream);
                    }

                    return EnumUpdatingResult.Successed;
                }

                return EnumUpdatingResult.Failed;
            }
            catch (Exception ex)
            {
                #region Logging info
                _logger.ErrorInDetail(Form, correlationGuid, $"{nameof(ClothesService)}_{nameof(UpdateClothDepartment)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);
                #endregion Logging info

                return EnumUpdatingResult.Failed;
            }
        }

        public async Task<ClothTypeDTO> GetClothType(Guid Id)
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info

                _logger.InfoInDetail(Id, correlationGuid, nameof(ClothesService), nameof(GetClothType), 1, _userIdentity.Name);

                #endregion Logging info

                var Model = await _unitOfWork.ClothTypeRepository.GetAsync(Id);
                return _mapper.Map<ClothTypeEntity, ClothTypeDTO>(Model);
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail(Id, correlationGuid, $"{nameof(ClothesService)}_{nameof(GetClothType)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

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

                _logger.ErrorInDetail(clothCategory, correlationGuid, $"{nameof(ClothesService)}_{nameof(AddNewClothCategory)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

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
                return await _unitOfWork.SP.OneRecordAsnyc<string>(StoredProcedure.DELETECLOTHCATEGORY, SPParameters);
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail(Id, correlationGuid, $"{nameof(ClothesService)}_{nameof(DeleteClothCategory)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

                #endregion Logging info

                return ex.Message;
            }
        }

        public async Task<EnumUpdatingResult> UpdateClothCategory(ClothCategoryDTO clothCategory, string machineDirectory)
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
                    var UTDClothCategory = _mapper.Map<ClothCategoryDTO, UTDClothCategoryDTO>(clothCategory);
                    var ClothCategories = new List<UTDClothCategoryDTO>
                    {
                       UTDClothCategory
                    };

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

        public async Task<ClothDepartmentDTO> AddNewClothDepartment(IFormCollection Form, string DiskDirectory)
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info
                _logger.InfoInDetail(Form, correlationGuid, nameof(ClothesService), nameof(AddNewClothDepartment), 1, _userIdentity.Name);
                #endregion Logging info

                var ClothDepartment = await _unitOfWork.ClothDepartmentRepository.GetClothDepartmentByName(Form["Name"].ToString().Trim(), Guid.NewGuid());
                if (ClothDepartment == null)
                {

                    var identityName = _userIdentity?.Name ?? "Unknown";

                    var clothDepartment = new ClothDepartmentDTO
                    {
                        Id = Guid.NewGuid(),
                        CreatedBy = identityName,
                        CreatedDate = DateTime.Now,
                        ModifiedBy = identityName,
                        ModifiedDate = DateTime.Now,
                        Name = Form["Name"].ToString(),
                    };
                    var clothDepartmentEntity = _mapper.Map<ClothDepartmentDTO, UTDClothDepartmentDTO>(clothDepartment);
                    var clothDepartments = new List<UTDClothDepartmentDTO>
                        {
                           clothDepartmentEntity
                        };

                    var clothDepartmenttable = ObjectDatatableConverter.ToDataTable(clothDepartments);
                    var SPParameters = new DynamicParameters();
                    SPParameters.Add("@UDTClothDepartment", clothDepartmenttable.AsTableValuedParameter(UDT.UDTCLOTHDEPARTMENT));
                    SPParameters.Add("@Extension", Path.GetExtension(Form.Files[0].FileName));
                    SPParameters.Add("@DiskDirectory", DiskDirectory);

                    var List = await _unitOfWork.SP.ListAsnyc<ClothDepartmentDTO>(StoredProcedure.ADDCLOTHDEPARTMENT, SPParameters);
                    return List.FirstOrDefault();
                }
                return null;
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail(Form, correlationGuid, $"{nameof(ClothesService)}_{nameof(AddNewClothDepartment)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

                #endregion Logging info

                throw;
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

        public async Task<string> DeleteClothDepartment(Guid Id)
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info

                _logger.InfoInDetail(Id, correlationGuid, nameof(ClothesService), nameof(DeleteClothDepartment), 1, _userIdentity.Name);

                #endregion Logging info

                var SPParameters = new DynamicParameters();
                SPParameters.Add("@DepartmentId", Id);
                return await _unitOfWork.SP.OneRecordAsnyc<string>(StoredProcedure.DELETECLOTHDEPARTMENT, SPParameters);
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail(Id, correlationGuid, $"{nameof(ClothesService)}_{nameof(DeleteClothDepartment)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

                #endregion Logging info

                return ex.Message;
            }
        }

        public async Task<EnumUpdatingResult> UpdateClothDepartment(IFormCollection Form, string DiskDirectory,string machineDirectory)
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info

                _logger.InfoInDetail(Form, correlationGuid, nameof(ClothesService), nameof(UpdateClothDepartment), 1, _userIdentity.Name);

                #endregion Logging info

                var _clothDepartment = await _unitOfWork.ClothDepartmentRepository.GetClothDepartmentByName(Form["Name"].ToString().Trim(), Guid.Parse(Form["Id"].ToString()));
                if (_clothDepartment == null)
                {
                    var identityName = _userIdentity?.Name ?? "Unknown";
                    var clothDepartment = new ClothDepartmentDTO
                    {
                        Id = Guid.Parse(Form["Id"].ToString()),
                        ModifiedBy = identityName,
                        ModifiedDate = DateTime.Now,
                        Name = Form["Name"].ToString(),
                    };

                    var UTDClothDepartment = _mapper.Map<ClothDepartmentDTO, UTDClothDepartmentDTO>(clothDepartment);
                    var ClothDepartments = new List<UTDClothDepartmentDTO>
                    {
                       UTDClothDepartment
                    };

                    var clothDepartmenttable = ObjectDatatableConverter.ToDataTable(ClothDepartments);
                    var SPParameters = new DynamicParameters();
                    SPParameters.Add("@UDTClothDepartment", clothDepartmenttable.AsTableValuedParameter(UDT.UDTCLOTHDEPARTMENT));
                    if (Form.Files.Count > 0)
                        SPParameters.Add("@Extension", Path.GetExtension(Form.Files[0].FileName));
                    else
                        SPParameters.Add("@Extension", null);

                    SPParameters.Add("@DiskDirectory", DiskDirectory);
                    var URL = await _unitOfWork.SP.ListAsnyc<string, string>(StoredProcedure.UPDATECLOTHDEPARTMENT, SPParameters);
                    var URL1 = Path.Combine(machineDirectory, URL.Item1.FirstOrDefault());
                    var URL2 = Path.Combine(machineDirectory, URL.Item2.FirstOrDefault());

                    if (URL1 != URL2)
                        _filemapper.MoveDataBetweenTwoFile(URL1, URL2);


                    if (Form.Files.Count > 0)
                    {
                        var files1 = URL1.Split("\\");
                        var files2 = URL2.Split("\\");
                        var URL2Directory = URL2.Remove(URL2.IndexOf(files2[^1]));
                        File.Delete(URL2Directory + files1[^1]);

                        using var stream = new FileStream(URL2, FileMode.Create);
                        Form.Files[0].CopyTo(stream);
                    }

                    return EnumUpdatingResult.Successed;
                }

                return EnumUpdatingResult.Failed;
            }
            catch (Exception ex)
            {
                #region Logging info
                _logger.ErrorInDetail(Form, correlationGuid, $"{nameof(ClothesService)}_{nameof(UpdateClothDepartment)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);
                #endregion Logging info

                return EnumUpdatingResult.Failed;
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

        #region Clothes

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

                    var cloth = new ClothDTO
                    {
                        Id = Guid.NewGuid(),
                        CreatedBy = identityName,
                        CreatedDate = DateTime.Now,
                        ModifiedBy = identityName,
                        ModifiedDate = DateTime.Now,
                        Name = Form["Name"].ToString(),
                        Description = Form["Description"].ToString(),
                        ClothCategoryId = Guid.Parse(Form["ClothCategoryId"].ToString())
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
                    SPParameters.Add("@UDTCloth", Producttable.AsTableValuedParameter(UDT.UDTCLOTH));
                    SPParameters.Add("@UDTClothImage", image1table.AsTableValuedParameter(UDT.UDTCLOTHIMAGE));
                    SPParameters.Add("@UDTClothImage2", image2table.AsTableValuedParameter(UDT.UDTCLOTHIMAGE));
                    SPParameters.Add("@Extension1", Path.GetExtension(Form.Files[0].FileName));
                    if (Form.Files.Count > 1)
                        SPParameters.Add("@Extension2", Path.GetExtension(Form.Files[1].FileName));
                    else
                        SPParameters.Add("@Extension2", General.NOEXTENSION);

                    SPParameters.Add("@DiskDirectory", DiskDirectory);



                    var List = await _unitOfWork.SP.ListAsnyc<NumberClothDTO>(StoredProcedure.ADDCLOTH, SPParameters);
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
                SPParameters.Add("@ClothId", Id);
                return await _unitOfWork.SP.OneRecordAsnyc<string>(StoredProcedure.DELETECLOTH, SPParameters);
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail(Id, correlationGuid, $"{nameof(ClothesService)}_{nameof(DeleteCloth)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

                #endregion Logging info

                return ex.Message;
            }
        }

        public async Task<List<string>> UpdateCloth(IFormCollection Form, string DiskDirectory,string MachineDirectory)
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
                        Name = Form["Name"].ToString(),
                        Description = Form["Description"].ToString(),
                        ClothCategoryId = Guid.Parse(Form["ClothCategoryId"].ToString())
                    };
                    string Extention1 = "NoExtension";
                    if (!string.IsNullOrEmpty(Form["Img1"]))
                        Extention1 = Path.GetExtension(Form.Files[0].FileName);
                      
                    string Extention2 = "NoExtension";
                    var Img2Id = Guid.Empty;
                    if (Form.Files.Count > 0)
                    {
                        if (Form.Files[0].Name == "ImgFile2")
                            Extention2 = Path.GetExtension(Form.Files[0].FileName);
                      
                        if (Form.Files.Count > 1)
                        {
                            if (Form.Files[1].Name == "ImgFile2")
                            {
                                Extention2 = Path.GetExtension(Form.Files[1].FileName);
                                Img2Id = Guid.NewGuid();
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
                    SPParameters.Add("@UDTCloth", Clothtable.AsTableValuedParameter(UDT.UDTCLOTH));
                    SPParameters.Add("@ImageId2", Img2Id);
                    SPParameters.Add("@Extension1", Extention1);
                    SPParameters.Add("@Extension2", Extention2);
                    SPParameters.Add("@DiskDirectory", DiskDirectory);


                    var List = await _unitOfWork.SP.FourListAsnyc<string, string, string, string>(StoredProcedure.UPDATECLOTH, SPParameters);
                    var Result = new List<string>();
                    var URL1 = List.Item1.FirstOrDefault();
                    var URL2 = List.Item2.FirstOrDefault();
                    var URL3 = List.Item3.FirstOrDefault();
                    var URL4 = List.Item4.FirstOrDefault();

                    Result.Add(URL1); Result.Add(URL2);
                    Result.Add(URL3); Result.Add(URL4);

                    if (URL1 != URL2)
                        _filemapper.MoveDataBetweenTwoProductFile(Path.Combine(MachineDirectory, List.Item1.FirstOrDefault()), Path.Combine(MachineDirectory, List.Item2.FirstOrDefault()));

                    

                    return Result;
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

        public async Task<ClothImageDTO> SaveClothImage(string ClothId, IFormFile file, string DiskDirectory)
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
                SPParameters.Add("@UDTClothImage", table.AsTableValuedParameter(UDT.UDTCLOTHIMAGE));
                SPParameters.Add("@Extension", Path.GetExtension(file.FileName));
                SPParameters.Add("@DiskDirectory", DiskDirectory);

                var Imglist = await _unitOfWork.SP.ListAsnyc<UTDClothImageDTO>(StoredProcedure.ADDCLOTHIMAGE, SPParameters);
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

                _logger.InfoInDetail(Id, correlationGuid, nameof(ClothesService), nameof(GetClothImage), 1, _userIdentity.Name);

                #endregion Logging info


                var entity = await _unitOfWork.ClothImageRepository.GetAsync(Id);
                return _mapper.Map<ClothImageEntity, ClothImageDTO>(entity);
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail(Id, correlationGuid, $"{nameof(ClothesService)}_{nameof(GetClothImage)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

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
                SPParameters.Add("@ClothId", ClothId);


                return (await _unitOfWork.SP.ListAsnyc<ClothImageDTO>(StoredProcedure.GETCLOTHIMAGES, SPParameters)).ToList();
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

        public async Task<string> AddClothBrands(SelectedBrandsDTO selectedBrands)
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info

                _logger.InfoInDetail(selectedBrands, correlationGuid, nameof(ClothesService), nameof(AddClothBrands), 1, _userIdentity.Name);

                #endregion Logging info

                var identityName = _userIdentity?.Name ?? "Unknown";
                var clothBrands = new List<UTDClothBrandDTO>();

                foreach (var brand in selectedBrands.Brands)
                {

                    var clothBrand = new ClothBrandDTO
                    {
                        Id = brand,
                        CreatedBy = identityName,
                        CreatedDate = DateTime.Now,
                        ModifiedBy = identityName,
                        ModifiedDate = DateTime.Now,
                    };
                    var clothBrandEntity = _mapper.Map<ClothBrandDTO, UTDClothBrandDTO>(clothBrand);
                    clothBrands.Add(clothBrandEntity);
                }
               
                var clothDepartmenttable = ObjectDatatableConverter.ToDataTable(clothBrands);
                var SPParameters = new DynamicParameters();
                SPParameters.Add("@UDTClothBrand", clothDepartmenttable.AsTableValuedParameter(UDT.UDTCLOTHBRAND));
                SPParameters.Add("@ClothId", selectedBrands.ClothId);
              
                 await _unitOfWork.SP.ExecuteAsnyc<ClothBrandDTO>(StoredProcedure.ADDCLOTHBRANDS, SPParameters);
                return CommonMessages.SUCCESSFULLY_ADDING;
 
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail(selectedBrands, correlationGuid, $"{nameof(ClothesService)}_{nameof(AddClothBrands)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

                #endregion Logging info

                return ex.Message;
            }

        }

        public async Task<List<ClothBrandsDTO>> GetClothBrands(Guid ClothId)
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info

                _logger.InfoInDetail(ClothId, correlationGuid, nameof(ClothesService), nameof(GetClothBrands), 1, _userIdentity.Name);

                #endregion Logging info

                var SPParameters = new DynamicParameters();
                SPParameters.Add("@ClothId", ClothId);


                return (await _unitOfWork.SP.ListAsnyc<ClothBrandsDTO>(StoredProcedure.GETCLOTHBRANDS, SPParameters)).ToList();
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail(ClothId, correlationGuid, $"{nameof(ClothesService)}_{nameof(GetClothBrands)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

                #endregion Logging info

                throw;
            }
        }
        #endregion

        #region Brands

        public async Task<ClothBrandDTO> AddNewClothBrand(IFormCollection Form, string DiskDirectory)
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info
                _logger.InfoInDetail(Form, correlationGuid, nameof(ClothesService), nameof(AddNewClothBrand), 1, _userIdentity.Name);
                #endregion Logging info

                var ClothBrand = await _unitOfWork.ClothBrandRepository.GetClothBrandByName(Form["Name"].ToString().Trim(), Guid.NewGuid());
                if (ClothBrand == null)
                {

                    var identityName = _userIdentity?.Name ?? "Unknown";

                    var clothBrand = new ClothBrandDTO
                    {
                        Id = Guid.NewGuid(),
                        CreatedBy = identityName,
                        CreatedDate = DateTime.Now,
                        ModifiedBy = identityName,
                        ModifiedDate = DateTime.Now,
                        Name = Form["Name"].ToString(),
                    };
                    var clothBrandEntity = _mapper.Map<ClothBrandDTO, UTDClothBrandDTO>(clothBrand);
                    var clothBrands = new List<UTDClothBrandDTO>
                        {
                           clothBrandEntity
                        };

                    var clothDepartmenttable = ObjectDatatableConverter.ToDataTable(clothBrands);
                    var SPParameters = new DynamicParameters();
                    SPParameters.Add("@UDTClothBrand", clothDepartmenttable.AsTableValuedParameter(UDT.UDTCLOTHBRAND));
                    SPParameters.Add("@Extension", Path.GetExtension(Form.Files[0].FileName));
                    SPParameters.Add("@DiskDirectory", DiskDirectory);

                    var List = await _unitOfWork.SP.ListAsnyc<ClothBrandDTO>(StoredProcedure.ADDCLOTHBRAND, SPParameters);
                    return List.FirstOrDefault();
                }
                return null;
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail(Form, correlationGuid, $"{nameof(ClothesService)}_{nameof(AddNewClothBrand)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

                #endregion Logging info

                throw;
            }
        }

        public object GetClothBrands()
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info

                _logger.InfoInDetail("GetClothBrands", correlationGuid, nameof(ClothesService), nameof(GetClothBrands), 1, _userIdentity.Name);

                #endregion Logging info

                return _unitOfWork.ClothBrandRepository.GetAll();

            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail("GetClothBrands", correlationGuid, $"{nameof(ClothesService)}_{nameof(GetClothBrands)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

                #endregion Logging info

                throw;
            }
        }

        public async Task<string> DeleteClothBrand(Guid Id)
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info

                _logger.InfoInDetail(Id, correlationGuid, nameof(ClothesService), nameof(DeleteClothBrand), 1, _userIdentity.Name);

                #endregion Logging info

                var SPParameters = new DynamicParameters();
                SPParameters.Add("@BrandId", Id);
                return await _unitOfWork.SP.OneRecordAsnyc<string>(StoredProcedure.DELETECLOTHBRAND, SPParameters);
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail(Id, correlationGuid, $"{nameof(ClothesService)}_{nameof(DeleteClothBrand)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

                #endregion Logging info

                return ex.Message;
            }
        }

        public async Task<EnumUpdatingResult> UpdateClothBrand(IFormCollection Form, string DiskDirectory, string machineDirectory)
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info

                _logger.InfoInDetail(Form, correlationGuid, nameof(ClothesService), nameof(UpdateClothBrand), 1, _userIdentity.Name);

                #endregion Logging info

                var _clothBrand = await _unitOfWork.ClothBrandRepository.GetClothBrandByName(Form["Name"].ToString().Trim(), Guid.Parse(Form["Id"].ToString()));
                if (_clothBrand == null)
                {
                    var identityName = _userIdentity?.Name ?? "Unknown";
                    var clothBrand = new ClothBrandDTO
                    {
                        Id = Guid.Parse(Form["Id"].ToString()),
                        ModifiedBy = identityName,
                        ModifiedDate = DateTime.Now,
                        Name = Form["Name"].ToString(),
                    };

                    var UTDClothBrand = _mapper.Map<ClothBrandDTO, UTDClothBrandDTO>(clothBrand);
                    var ClothBrands = new List<UTDClothBrandDTO>
                    {
                       UTDClothBrand
                    };

                    var clothBrandtable = ObjectDatatableConverter.ToDataTable(ClothBrands);
                    var SPParameters = new DynamicParameters();
                    SPParameters.Add("@UDTClothBrand", clothBrandtable.AsTableValuedParameter(UDT.UDTCLOTHBRAND));
                    if (Form.Files.Count > 0)
                        SPParameters.Add("@Extension", Path.GetExtension(Form.Files[0].FileName));
                    else
                        SPParameters.Add("@Extension", null);

                    SPParameters.Add("@DiskDirectory", DiskDirectory);
                    var URL = await _unitOfWork.SP.ListAsnyc<string, string>(StoredProcedure.UPDATECLOTHBRAND, SPParameters);
                    var URL1 = Path.Combine(machineDirectory, URL.Item1.FirstOrDefault());
                    var URL2 = Path.Combine(machineDirectory, URL.Item2.FirstOrDefault());

                    if (URL1 != URL2)
                        _filemapper.MoveDataBetweenTwoFile(URL1, URL2);


                    if (Form.Files.Count > 0)
                    {
                        var files1 = URL1.Split("\\");
                        var files2 = URL2.Split("\\");
                        var URL2Directory = URL2.Remove(URL2.IndexOf(files2[^1]));
                        File.Delete(URL2Directory+ files1[^1]);
                     
                        using var stream = new FileStream(URL2, FileMode.Create);
                        Form.Files[0].CopyTo(stream);
                    }

                    return EnumUpdatingResult.Successed;
                }

                return EnumUpdatingResult.Failed;
            }
            catch (Exception ex)
            {
                #region Logging info
                _logger.ErrorInDetail(Form, correlationGuid, $"{nameof(ClothesService)}_{nameof(UpdateClothBrand)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);
                #endregion Logging info

                return EnumUpdatingResult.Failed;
            }
        }

        public async Task<ClothBrandDTO> GetClothBrand(Guid Id)
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info

                _logger.InfoInDetail(Id, correlationGuid, nameof(ClothesService), nameof(GetClothBrand), 1, _userIdentity.Name);

                #endregion Logging info

                var Model = await _unitOfWork.ClothBrandRepository.GetAsync(Id);
                return _mapper.Map<ClothBrandEntity, ClothBrandDTO>(Model);
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail(Id, correlationGuid, $"{nameof(ClothesService)}_{nameof(GetClothBrand)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

                #endregion Logging info

                throw;
            }
        }

        #endregion

        #region Sizes
        public async Task<EnumInsertingResult> AddNewClothSize(ClothSizeDTO clothSize)
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info

                _logger.InfoInDetail(clothSize, correlationGuid, nameof(ClothesService), nameof(AddNewClothSize), 1, _userIdentity.Name);

                #endregion Logging info

                var Size = await _unitOfWork.ClothSizeRepository.GetClothSizeByName(clothSize.Name.Trim(), clothSize.Id); ;
                if (Size == null)
                {
                    var Entity = _mapper.Map<ClothSizeDTO, ClothSizeEntity>(clothSize);
                    Entity.Id = Guid.NewGuid();
                    await _unitOfWork.ClothSizeRepository.AddAsync(Entity);
                    await _unitOfWork.SaveAsync();
                    return EnumInsertingResult.Successed;
                }
                return EnumInsertingResult.Failed;
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail(clothSize, correlationGuid, $"{nameof(ClothesService)}_{nameof(AddNewClothSize)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

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

                return _unitOfWork.ClothSizeRepository.GetAll();

            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail("GetClothSizes", correlationGuid, $"{nameof(ClothesService)}_{nameof(GetSizes)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

                #endregion Logging info

                throw;
            }
        }

        public async Task<string> DeleteClothSize(Guid Id)
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info

                _logger.InfoInDetail(Id, correlationGuid, nameof(ClothesService), nameof(DeleteClothSize), 1, _userIdentity.Name);

                #endregion Logging info

                var IsDeleted =await _unitOfWork.ClothSizeRepository.RemoveAsync(Id);
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

                _logger.ErrorInDetail(Id, correlationGuid, $"{nameof(ClothesService)}_{nameof(DeleteClothSize)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

                #endregion Logging info

                return ex.Message;
            }
        }

        public async Task<EnumUpdatingResult> UpdateClothSize(ClothSizeDTO clothSize)
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info

                _logger.InfoInDetail(clothSize, correlationGuid, nameof(ClothesService), nameof(UpdateClothSize), 1, _userIdentity.Name);

                #endregion Logging info

                var Size = await _unitOfWork.ClothSizeRepository.GetClothSizeByName(clothSize.Name.Trim(), clothSize.Id);

                if (Size == null)
                {
                    var ClothSize = _mapper.Map<ClothSizeDTO, ClothSizeEntity>(clothSize);
                    await _unitOfWork.ClothSizeRepository.UpdateClothSize(ClothSize);
                    await _unitOfWork.SaveAsync();
                    return EnumUpdatingResult.Successed;
                }

                return EnumUpdatingResult.Failed;
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail(clothSize, correlationGuid, $"{nameof(ClothesService)}_{nameof(UpdateClothSize)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

                #endregion Logging info

                throw;
            }
        }

        public async Task<ClothSizeDTO> GetClothSize(Guid Id)
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info

                _logger.InfoInDetail(Id, correlationGuid, nameof(ClothesService), nameof(GetClothSize), 1, _userIdentity.Name);

                #endregion Logging info

                var Model = await _unitOfWork.ClothSizeRepository.GetAsync(Id);
                return _mapper.Map<ClothSizeEntity, ClothSizeDTO>(Model);
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail(Id, correlationGuid, $"{nameof(ClothesService)}_{nameof(GetClothSize)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

                #endregion Logging info

                throw;
            }
        }

        public async Task<object> GetClothSizes(Guid ClothId)
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info

                _logger.InfoInDetail(ClothId, correlationGuid, nameof(ClothesService), nameof(GetClothSizes), 1, _userIdentity.Name);

                #endregion Logging info

                return await _unitOfWork.ClothSizeRepository.GetClothSizes(ClothId);
                
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail(ClothId, correlationGuid, $"{nameof(ClothesService)}_{nameof(GetClothSizes)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

                #endregion Logging info

                throw;
            }
        }
        #endregion

    }
}
