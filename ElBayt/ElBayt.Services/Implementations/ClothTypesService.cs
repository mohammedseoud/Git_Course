using Dapper;
using ElBayt.Common.Enums;
using ElBayt.Common.Utilities;
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

        #region Types

        public async Task<string> AddNewClothType(IFormCollection Form, string DiskDirectory)
        {
            try
            {
                var ClothType = await _unitOfWork.ClothTypeRepository.GetClothTypeByName(Form["ClothType.Name"].ToString().Trim(), 0);
                if (ClothType == null)
                {

                    var identityName = _userIdentity?.Name ?? "Unknown";


                    var clothtype = new ClothTypeDTO
                    {
                        Name = Form["ClothType.Name"].ToString(),
                        ClothDepartmentId = Convert.ToInt32(Form["ClothType.ClothDepartmentId"].ToString()),                       
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

                    var PicURL = await _unitOfWork.SP.ListAsnyc<string>(StoredProcedure.ADDCLOTHTYPE, SPParameters);
                    return PicURL.FirstOrDefault();
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
                var _clothtype = await _unitOfWork.ClothTypeRepository.GetClothTypeByName(Form["ClothType.Name"].ToString().Trim(),
                    Convert.ToInt32(Form["ClothType.Id"].ToString()));
                if (_clothtype == null)
                {
                    var identityName = _userIdentity?.Name ?? "Unknown";
                    var clothtype = new ClothTypeDTO
                    {
                        Id = Convert.ToInt32(Form["ClothType.Id"].ToString()),
                        Name = Form["ClothType.Name"].ToString(),
                        ClothDepartmentId = Convert.ToInt32(Form["ClothType.ClothDepartmentId"].ToString())
                    };

                    var UTDClothType = _mapper.Map<ClothTypeDTO, UTDClothTypeDTO>(clothtype);
                    UTDClothType.ModifiedBy = identityName;
                    UTDClothType.ModifiedDate = DateTime.Now;

                    var ClothTypes = new List<UTDClothTypeDTO>
                    {
                       UTDClothType
                    };

                    var clothTypetable = ObjectDatatableConverter.ToDataTable(ClothTypes);
                    var SPParameters = new DynamicParameters();
                    SPParameters.Add("@UDTClothType", clothTypetable.AsTableValuedParameter(UDT.UDTCLOTHTYPE));
                    if (Form.Files.Count > 0)
                    {
                        if (Form.Files[0].FileName != "No File" && Form.Files[0].Length > 0)
                            SPParameters.Add("@Extension", Path.GetExtension(Form.Files[0].FileName));
                        else
                            SPParameters.Add("@Extension", null);
                    }
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
                        if (Form.Files[0].FileName != "No File" && Form.Files[0].Length > 0)
                        {
                            var files1 = URL1.Split("\\");
                            var files2 = URL2.Split("\\");
                            var URL2Directory = URL2.Remove(URL2.IndexOf(files2[^1]));
                            File.Delete(URL2Directory + files1[^1]);

                            using var stream = new FileStream(URL2, FileMode.Create);
                            Form.Files[0].CopyTo(stream);
                        }
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
    }
}
