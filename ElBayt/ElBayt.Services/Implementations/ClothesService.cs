﻿using AutoMapper;
using Dapper;
using ElBayt.Common.Common;
using ElBayt.Common.Core.Logging;
using ElBayt.Common.Core.Mapping;
using ElBayt.Common.Core.SecurityModels;
using ElBayt.Common.Enums;
using ElBayt.Common.Utilities;
using ElBayt.Infra.Entities;
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
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ElBayt.Core.Models;

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
            try
            {
                var ClothType = await _unitOfWork.ClothTypeRepository.GetClothTypeByName(Form["ClothType.Name"].ToString().Trim(), 0);
                if (ClothType == null)
                {

                    var identityName = _userIdentity?.Name ?? "Unknown";
                    var test = Form["ClothType.ClothDepartmentId"];
                    var test1 = Form["ClothDepartmentId"];


                    var clothtype = new ClothTypeDTO
                    {
                        Id = Guid.NewGuid(),
                        Name = Form["Name"].ToString(),
                        ClothDepartmentId = Guid.Parse(Form["ClothType.ClothDepartmentId"].ToString())
                    };

                    var clothtypeEntity = _mapper.Map<ClothTypeDTO, UTDClothTypeDTO>(clothtype);
                    clothtypeEntity.CreatedDate= DateTime.Now;
                    clothtypeEntity.CreatedBy = identityName;

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
                throw;
            }
        }

        public async Task<List<GetClothTypeDTO>> GetClothTypes()
        {
     

            try
            {
                var types = (await _unitOfWork.ClothTypeRepository.GetAllAsync()).
                    Select(c => new GetClothTypeDTO
                    {
                        Id = c.Id,
                        Name = c.Name,
                    }).ToList();

                return types;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<string> DeleteClothType(int Id)
        {
     

            try
            {
                var SPParameters = new DynamicParameters();
                SPParameters.Add("@TypeId", Id);
                return await _unitOfWork.SP.OneRecordAsnyc<string>(StoredProcedure.DELETECLOTHTYPE, SPParameters);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<EnumUpdatingResult> UpdateClothType(IFormCollection Form, string DiskDirectory,string machineDirectory)
        {
     

            try
            {
                var _clothtype = await _unitOfWork.ClothTypeRepository.GetClothTypeByName(Form["Name"].ToString().Trim(),
                    Convert.ToInt32(Form["Id"].ToString()));
                if (_clothtype == null)
                {
                    var identityName = _userIdentity?.Name ?? "Unknown";
                    var clothtype = new ClothTypeDTO
                    {
                        Id = Guid.Parse(Form["Id"].ToString()),
                        //ModifiedBy = identityName,
                        //ModifiedDate = DateTime.Now,
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
                return EnumUpdatingResult.Failed;
            }
        }

        public async Task<ClothTypeDTO> GetClothType(int Id)
        {
            try
            {
                var Model = await _unitOfWork.ClothTypeRepository.GetAsync(Id);
                return _mapper.Map<ClothTypeModel, ClothTypeDTO>(Model);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

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

        #endregion

        #region Departments

        public async Task<ClothDepartmentDTO> AddNewClothDepartment(IFormCollection Form, string DiskDirectory)
        {
            try
            {
                var ClothDepartment = await _unitOfWork.ClothDepartmentRepository.GetClothDepartmentByName(Form["Name"].ToString().Trim(), 0);
                if (ClothDepartment == null)
                {

                    var identityName = _userIdentity?.Name ?? "Unknown";

                    var clothDepartment = new ClothDepartmentDTO
                    {
                        //CreatedBy = identityName,
                        //CreatedDate = DateTime.Now,
                        //ModifiedBy = identityName,
                        //ModifiedDate = DateTime.Now,
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
                throw;
            }
        }

        public async Task<List<GetClothDepartmentDTO>> GetClothDepartments()
        {
            try
            {
                var Departments = (await _unitOfWork.ClothDepartmentRepository.GetAllAsync()).
                    Select(
                    c => new GetClothDepartmentDTO
                    {
                        DepartmentPic = c.DepartmentPic,
                        Name = c.Name,
                        Id = c.Id
                    });

                return Departments.ToList();

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<string> DeleteClothDepartment(int Id)
        {
            try
            {
                var SPParameters = new DynamicParameters();
                SPParameters.Add("@DepartmentId", Id);
                return await _unitOfWork.SP.OneRecordAsnyc<string>(StoredProcedure.DELETECLOTHDEPARTMENT, SPParameters);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<EnumUpdatingResult> UpdateClothDepartment(IFormCollection Form, string DiskDirectory,string machineDirectory)
        {
            try
            {
                var _clothDepartment = await _unitOfWork.ClothDepartmentRepository.
                    GetClothDepartmentByName(Form["Name"].ToString().Trim(), Convert.ToInt32(Form["Id"].ToString()));
                if (_clothDepartment == null)
                {
                    var identityName = _userIdentity?.Name ?? "Unknown";
                    var clothDepartment = new ClothDepartmentDTO
                    {
                        Id = Convert.ToInt32(Form["Id"].ToString()),
                        //ModifiedBy = identityName,
                        //ModifiedDate = DateTime.Now,
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
                return EnumUpdatingResult.Failed;
            }
        }

        public async Task<ClothDepartmentDTO> GetClothDepartment(int Id)
        {    
            try
            {
                var Model = await _unitOfWork.ClothDepartmentRepository.GetAsync(Id);
                return _mapper.Map<ClothDepartmentModel, ClothDepartmentDTO>(Model);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

        #region Clothes

        public async Task<NumberClothDTO> AddNewCloth(IFormCollection Form, string DiskDirectory)
        {

            try
            {

                var Cloth = await _unitOfWork.ClothRepository.GetClothByName(Form["Name"].ToString().Trim(), 0);
                if (Cloth == null)
                {

                    var identityName = _userIdentity?.Name ?? "Unknown";

                    var cloth = new ClothDTO
                    {
                        Id = Guid.NewGuid(),
                        //CreatedBy = identityName,
                        //CreatedDate = DateTime.Now,
                        //ModifiedBy = identityName,
                        //ModifiedDate = DateTime.Now,
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
                throw;
            }
        }

        public async Task<List<GetClothInfoDTO>> GetClothesInfo()
        {
            try
            {
                var Clothes = (await _unitOfWork.ClothInfoRepository.GetAllAsync()).
                    Select(c => new GetClothInfoDTO
                    {
                        Id = c.Id,
                        Amount = c.Amount,
                        BrandId = c.BrandId?.ToString(),
                        ClothId = c.ClothId,
                        ColorId = c.ColorId?.ToString(),
                        Price = c.Price,
                        PriceAfterDiscount = (decimal)c.PriceAfterDiscount,
                        SizeId = c.SizeId

                    });

                return Clothes.ToList();

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<GetClothDTO>> GetClothes()
        {
     

            try
            {
                var Clothes = (await _unitOfWork.ClothRepository.GetAllAsync()).
                    Select(c => new GetClothDTO
                    {
                        Id = c.Id,
                        Name = c.Name,
                        ClothCategoryId = c.ClothCategoryId,
                        Description = c.Description,
                        ProductImageURL1 = c.ProductImageURL1,
                        ProductImageURL2 = c.ProductImageURL2,
                       
                    });

                return Clothes.ToList();

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<string> DeleteCloth(int Id)
        {
            try
            {
                var SPParameters = new DynamicParameters();
                SPParameters.Add("@ClothId", Id);
                return await _unitOfWork.SP.OneRecordAsnyc<string>(StoredProcedure.DELETECLOTH, SPParameters);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<List<string>> UpdateCloth(IFormCollection Form, string DiskDirectory,string MachineDirectory)
        {
            try
            {
                var cloth = await _unitOfWork.ClothRepository.
                    GetClothByName(Form["Name"].ToString().Trim(), Convert.ToInt32(Form["Id"].ToString()));

                if (cloth == null)
                {
                    var identityName = _userIdentity?.Name ?? "Unknown";

                    var Newcloth = new ClothDTO
                    {
                        Id = Guid.Parse(Form["Id"].ToString()),
                        //ModifiedBy = identityName,
                        //ModifiedDate = DateTime.Now,
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
                throw;
            }
        }

        public async Task<NumberClothDTO> GetCloth(int Id)
        {    
            try
            {
                var Model = await _unitOfWork.ClothRepository.GetAsync(Id);
                return _mapper.Map<ClothModel, NumberClothDTO>(Model);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ClothImageDTO> SaveClothImage(string ClothId, IFormFile file, string DiskDirectory)
        {
            try
            {
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
                throw;
            }
        }

        public async Task<ClothImageDTO> GetClothImage(int Id)
        {
            try
            {
                var entity = await _unitOfWork.ClothImageRepository.GetAsync(Id);
                return _mapper.Map<ClothImageModel, ClothImageDTO>(entity);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<ClothImageDTO>> GetClothImages(int ClothId)
        {
            try
            {
                var SPParameters = new DynamicParameters();
                SPParameters.Add("@ClothId", ClothId);


                return (await _unitOfWork.SP.ListAsnyc<ClothImageDTO>(StoredProcedure.GETCLOTHIMAGES, SPParameters)).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<string> DeleteClothImage(int ImageId)
        {
            try
            {
                var IsDeleted = await _unitOfWork.ClothImageRepository.RemoveAsync(ImageId);
                if (IsDeleted)
                {
                    var res = await _unitOfWork.SaveAsync();
                    return CommonMessages.SUCCESSFULLY_DELETING;

                }
                return CommonMessages.ITEM_NOT_EXISTS;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> DeleteClothImageByURL(string URL)
        {
            try
            {
                var IsDeleted = _unitOfWork.ClothImageRepository.DeleteByURL(URL);
                if (IsDeleted)
                {
                    var res = await _unitOfWork.SaveAsync();
                    return "true";

                }
                return CommonMessages.ITEM_NOT_EXISTS;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        public async Task<string> AddClothBrands(SelectedBrandsDTO selectedBrands)
        {     
            try
            {
                var identityName = _userIdentity?.Name ?? "Unknown";
                var clothBrands = new List<UTDClothBrandDTO>();

                foreach (var brand in selectedBrands.Brands)
                {

                    var clothBrand = new ClothBrandDTO
                    {
                       //CreatedBy = identityName,
                        //CreatedDate = DateTime.Now,
                        //ModifiedBy = identityName,
                        //ModifiedDate = DateTime.Now,
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
                return ex.Message;
            }

        }

        public async Task<List<ClothBrandsDTO>> GetClothBrands(int ClothId)
        {    
            try
            {
                var SPParameters = new DynamicParameters();
                SPParameters.Add("@ClothId", ClothId);


                return (await _unitOfWork.SP.ListAsnyc<ClothBrandsDTO>(StoredProcedure.GETCLOTHBRANDS, SPParameters)).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ClothDBLDataDTO> GetClothDBLInfo(int ClothId)
        {
            try
            {
                var SPParameters = new DynamicParameters();
                SPParameters.Add("@ClothId", ClothId);


                var Data = await _unitOfWork.SP.ThreeListAsnyc<DBLInfoDTO, DBLInfoDTO, DBLInfoDTO>(StoredProcedure.GETCLOTHDBLDATA, SPParameters);

                var ClothDBLData = new ClothDBLDataDTO
                {
                    Brands = Data.Item1.ToList(),
                    Colors = Data.Item2.ToList(),
                    Sizes = Data.Item3.ToList()
                };

                return ClothDBLData;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<string> AddClothInfo(ClothInfoDTO ClothInfo)
        {     
            try
            {
                var Entity = _mapper.Map<ClothInfoDTO, ClothInfoModel>(ClothInfo);
                if (ClothInfo.Id == 0)
                {
                    int? ColorId = null;
                    int? BrandId = null ;

                    if (ClothInfo.BrandId != 0)
                        BrandId = ClothInfo.BrandId;


                    if (ClothInfo.ColorId != 0)
                        ColorId = ClothInfo.ColorId;

                    var _info = await _unitOfWork.ClothInfoRepository.GetClothInfo(ClothInfo.SizeId, ColorId, BrandId);

                    if (_info == null)
                    {
                        await _unitOfWork.ClothInfoRepository.AddAsync(Entity);
                        await _unitOfWork.SaveAsync();
                        return CommonMessages.SUCCESSFULLY_ADDING;
                    }
                    else
                    {
                        var Model = _mapper.Map<ClothInfoDTO, ClothInfoModel>(ClothInfo);
                        await _unitOfWork.ClothInfoRepository.UpdateInfo(Model);
                        await _unitOfWork.SaveAsync();
                        return CommonMessages.SUCCESSFULLY_UPDATING;
                    }

                }
                else
                {
                    await _unitOfWork.ClothInfoRepository.UpdateInfo(Entity);
                    await _unitOfWork.SaveAsync();
                    return CommonMessages.SUCCESSFULLY_UPDATING;
                }
                
                
                
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
      
        public async Task<List<ClothInfoDataDTO>> GetClothInfo(int ClothId)
        {
            try
            {
                var SPParameters = new DynamicParameters();
                SPParameters.Add("@ClothId", ClothId);
                var Clothes = (await _unitOfWork.SP.ListAsnyc<ClothInfoDataDTO>(StoredProcedure.GETCLOTHINFO, SPParameters)).ToList();
                
                return Clothes;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
       
        public async Task<string> DeleteClothInfo(int Id)
        {
            try
            {
                var IsDeleted =await _unitOfWork.ClothInfoRepository.RemoveAsync(Id);
                if (IsDeleted)
                {
                    var res = await _unitOfWork.SaveAsync();
                    return CommonMessages.SUCCESSFULLY_DELETING;

                }
                return CommonMessages.ITEM_NOT_EXISTS;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        public async Task<ClothInfoDTO> GetInfo(int Id)
        {
            try
            {
                var Model = await _unitOfWork.ClothInfoRepository.GetAsync(Id);
                return _mapper.Map<ClothInfoModel, ClothInfoDTO>(Model);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region Brands

        public async Task<ClothBrandDTO> AddNewClothBrand(IFormCollection Form, string DiskDirectory)
        {
            try
            {
                var ClothBrand = await _unitOfWork.ClothBrandRepository.GetClothBrandByName(Form["Name"].ToString().Trim(), 0);
                if (ClothBrand == null)
                {

                    var identityName = _userIdentity?.Name ?? "Unknown";

                    var clothBrand = new ClothBrandDTO
                    {
                        //CreatedBy = identityName,
                        //CreatedDate = DateTime.Now,
                        //ModifiedBy = identityName,
                        //ModifiedDate = DateTime.Now,
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
                throw;
            }
        }

        public async Task<List<GetClothBrandDTO>> GetClothBrands()
        {
            try
            {
                var Brands = (await _unitOfWork.ClothBrandRepository.GetAllAsync()).
                    Select(c => new GetClothBrandDTO
                    {
                        BrandPic = c.BrandPic,
                        Id = c.Id,
                        Name = c.Name
                    });


                return Brands.ToList();

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<string> DeleteClothBrand(int Id)
        {
            try
            {
                var SPParameters = new DynamicParameters();
                SPParameters.Add("@BrandId", Id);
                return await _unitOfWork.SP.OneRecordAsnyc<string>(StoredProcedure.DELETECLOTHBRAND, SPParameters);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<EnumUpdatingResult> UpdateClothBrand(IFormCollection Form, string DiskDirectory, string machineDirectory)
        {
            try
            {
                var _clothBrand = await _unitOfWork.ClothBrandRepository.
                    GetClothBrandByName(Form["Name"].ToString().Trim(), Convert.ToInt32(Form["Id"].ToString()));
                if (_clothBrand == null)
                {
                    var identityName = _userIdentity?.Name ?? "Unknown";
                    var clothBrand = new ClothBrandDTO
                    {
                        Id = Convert.ToInt32(Form["Id"].ToString()),
                        //ModifiedBy = identityName,
                        //ModifiedDate = DateTime.Now,
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
                return EnumUpdatingResult.Failed;
            }
        }

        public async Task<ClothBrandDTO> GetClothBrand(int Id)
        {
            try
            {
                var Model = await _unitOfWork.ClothBrandRepository.GetAsync(Id);
                return _mapper.Map<ClothBrandModel, ClothBrandDTO>(Model);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

        #region Sizes
        public async Task<EnumInsertingResult> AddNewClothSize(ClothSizeDTO clothSize)
        {
            try
            {
                var Size = await _unitOfWork.ClothSizeRepository.GetClothSizeByName(clothSize.Name.Trim(), clothSize.Id); ;
                if (Size == null)
                {
                    var Entity = _mapper.Map<ClothSizeDTO, ClothSizeModel>(clothSize);
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
                        ClothId = c.ClothId,
                        Name = c.Name,
                        Id = c.Id,
                        Height = c.Height,
                        Width = c.Width
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
                var Sizes = (await _unitOfWork.ClothSizeRepository.GetAllAsync(C => C.ClothId == ClothId)).
                    ToList();
                var SizesClothes = Sizes.Select(c=>
                    new GetClothSizeDTO
                    {
                        ClothId = c.ClothId,
                        Width=c.Width,
                        Height=c.Height,
                        Id=c.Id,
                        Name=c.Name
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
