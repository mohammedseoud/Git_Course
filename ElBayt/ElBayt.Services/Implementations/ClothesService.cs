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
                    {
                        var files1 = URL1.Split("\\");
                        var files2 = URL2.Split("\\");

                        var URL1Directory = URL1.Remove(URL1.IndexOf(files1[^1]));
                        var URL2Directory = URL2.Remove(URL2.IndexOf(files2[^1]));
                        var _files1 = Directory.GetFiles(URL1Directory);
                        if (Directory.Exists(URL1Directory))
                        {
                            if (!Directory.Exists(URL2Directory))
                            {
                                var _files = Directory.GetFiles(URL1Directory);
                                var _directories = Directory.GetDirectories(URL1Directory);
                                Directory.CreateDirectory(URL2Directory);
                                foreach (var file in _files)
                                {
                                    var files = file.Split("\\");
                                    File.Move(file, URL2Directory + files[^1]);
                                }

                                foreach (var directory in _directories)
                                {
                                    var directories = directory.Split("\\");
                                    Directory.Move(directory, URL2Directory + directories[^1]);
                                }
                                Directory.Delete(URL1Directory);
                            }
                        }
                    }


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
                    {
                        var files1 = URL1.Split("\\");
                        var files2 = URL2.Split("\\");
                      
                        var URL1Directory = URL1.Remove(URL1.IndexOf(files1[^1])); 
                        var URL2Directory = URL2.Remove(URL2.IndexOf(files2[^1]));
                        var _files1 = Directory.GetFiles(URL1Directory);
                       if (Directory.Exists(URL1Directory))
                        {
                            if (!Directory.Exists(URL2Directory))
                            {
                                var _files = Directory.GetFiles(URL1Directory);
                                var _directories = Directory.GetDirectories(URL1Directory);
                                Directory.CreateDirectory(URL2Directory);
                                foreach (var file in _files)
                                {
                                    var files = file.Split("\\");
                                    File.Move(file, URL2Directory + files[^1]);
                                }

                                foreach (var directory in _directories)
                                {
                                    var directories = directory.Split("\\"); 
                                    Directory.Move(directory, URL2Directory + directories[^1]);
                                }


                                Directory.Delete(URL1Directory);
                            }
                        }
                    }


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
                    {
                        var files1 = URL1.Split("\\");
                        var files2 = URL2.Split("\\");

                        var URL1Directory = URL1.Remove(URL1.IndexOf(files1[^1]));
                        var URL2Directory = URL2.Remove(URL2.IndexOf(files2[^1]));
                        var _files1 = Directory.GetFiles(URL1Directory);
                        if (Directory.Exists(URL1Directory))
                        {
                            if (!Directory.Exists(URL2Directory))
                            {
                                var _files = Directory.GetFiles(URL1Directory);
                                var _directories = Directory.GetDirectories(URL1Directory);
                                Directory.CreateDirectory(URL2Directory);
                                foreach (var file in _files)
                                {
                                    var files = file.Split("\\");
                                    File.Move(file, URL2Directory + files[^1]);
                                }

                                foreach (var directory in _directories)
                                {
                                    var directories = directory.Split("\\");
                                    Directory.Move(directory, URL2Directory + directories[^1]);
                                }


                                Directory.Delete(URL1Directory);
                            }
                        }
                    }


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


    }
}
