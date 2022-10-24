using Dapper;
using ElBayt.Common.Common;
using ElBayt.Common.Core.Logging;
using ElBayt.Common.Core.Mapping;
using ElBayt.Common.Core.SecurityModels;
using ElBayt.Common.Utilities;
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
using ElBayt.Core.Models;

namespace ElBayt.Services.Implementations
{
    public partial class ClothesService : IClothesService
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
     
        #region Clothes

        public async Task<NumberClothDTO> AddNewCloth(IFormCollection Form, string DiskDirectory)
        {

            try
            {

                var Cloth = await _unitOfWork.ClothRepository.GetClothByName(Form["Cloth.Name"].ToString().Trim(), 0);
                if (Cloth == null)
                {

                    var identityName = _userIdentity?.Name ?? "Unknown";

                    var cloth = new ClothDTO
                    {
                        Name = Form["Cloth.Name"].ToString(),
                        Description = Form["Cloth.Description"].ToString(),
                        ClothCategoryId = Convert.ToInt32(Form["Cloth.ClothCategoryId"].ToString())
                    };

                    var ClothEntity = _mapper.Map<ClothDTO, UTDClothDTO>(cloth);
                    ClothEntity.CreatedBy = identityName;
                    ClothEntity.CreatedDate = DateTime.Now;
                    var Clothes = new List<UTDClothDTO>
                    {
                       ClothEntity
                    };
                    var image1 = new ClothImageDTO
                    {
                        ClothId = cloth.Id,
                        CreatedBy = identityName,
                        CreatedDate = DateTime.Now,
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
                            ClothId = cloth.Id,
                            CreatedBy = identityName,
                            CreatedDate = DateTime.Now,
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
                    {
                        if (Form.Files[1].FileName != "No File" && Form.Files[1].Length > 0)
                            SPParameters.Add("@Extension2", Path.GetExtension(Form.Files[1].FileName));
                        else
                            SPParameters.Add("@Extension2", General.NOEXTENSION);
                    }
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
                    GetClothByName(Form["Cloth.Name"].ToString().Trim(), Convert.ToInt32(Form["Cloth.Id"].ToString()));

                if (cloth == null)
                {
                    var identityName = _userIdentity?.Name ?? "Unknown";

                    var Newcloth = new ClothDTO
                    {
                        Id = Convert.ToInt32(Form["Cloth.Id"].ToString()),
                        Name = Form["Cloth.Name"].ToString(),
                        Description = Form["Cloth.Description"].ToString(),
                        ClothCategoryId = Convert.ToInt32(Form["ClothCategoryId"].ToString())
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
                                Extention2 = Path.GetExtension(Form.Files[1].FileName);
                        }
                        
                    }

                    var UTDCloth = _mapper.Map<ClothDTO, UTDClothDTO>(Newcloth);
                    UTDCloth.ModifiedBy = identityName;
                    UTDCloth.ModifiedDate = DateTime.Now;
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
                    ClothId = Convert.ToInt32(ClothId),
                    CreatedBy = identityName,
                    CreatedDate = DateTime.Now,
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

                var URL = await _unitOfWork.SP.ListAsnyc<ClothImageDTO>(StoredProcedure.ADDCLOTHIMAGE, SPParameters);
                return URL.FirstOrDefault();
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
                var identityName = _userIdentity?.Name ?? "Unknown";
                var Entity = _mapper.Map<ClothInfoDTO, ClothInfoModel>(ClothInfo);
                if (ClothInfo.Id == 0)
                {
   
                    var IsExisted = (await _unitOfWork.ClothInfoRepository.GetClothInfo(ClothInfo.SizeId, ClothInfo.ColorId, ClothInfo.BrandId)).Any();

                    if (!IsExisted)
                    {
                        await _unitOfWork.ClothInfoRepository.AddAsync(Entity);
                        await _unitOfWork.SaveAsync();
                        return CommonMessages.SUCCESSFULLY_ADDING;
                    }
                    else
                    {
                        var Model = _mapper.Map<ClothInfoDTO, ClothInfoModel>(ClothInfo);
                        Entity.CreatedBy = _userIdentity.Name;
                        Entity.CreatedDate = DateTime.Now;
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
    }
}
