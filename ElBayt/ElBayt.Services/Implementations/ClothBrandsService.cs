using Dapper;
using ElBayt.Common.Enums;
using ElBayt.Common.Utilities;
using ElBayt.DTO.ELBayt.DBDTOs;
using ElBayt.DTO.ELBayt.DBUDTDTOs;
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
        #region Brands

        public async Task<string> AddNewClothBrand(IFormCollection Form, string DiskDirectory)
        {
            try
            {
                var ClothBrand = await _unitOfWork.ClothBrandRepository.
                    GetClothBrandByName(Form["ClothBrand.Name"].ToString().Trim(), 0);
                if (ClothBrand == null)
                {

                    var identityName = _userIdentity?.Name ?? "Unknown";

                    var clothBrand = new ClothBrandDTO
                    {
                        Name = Form["ClothBrand.Name"].ToString(),
                        
                    };
                    var clothBrandEntity = _mapper.Map<ClothBrandDTO, UTDClothBrandDTO>(clothBrand);
                    clothBrandEntity.CreatedBy = identityName;
                    clothBrandEntity.CreatedDate = DateTime.Now;

                    var clothBrands = new List<UTDClothBrandDTO>
                        {
                           clothBrandEntity
                        };

                    var clothDepartmenttable = ObjectDatatableConverter.ToDataTable(clothBrands);
                    var SPParameters = new DynamicParameters();
                    SPParameters.Add("@UDTClothBrand", clothDepartmenttable.AsTableValuedParameter(UDT.UDTCLOTHBRAND));
                    SPParameters.Add("@Extension", Path.GetExtension(Form.Files[0].FileName));
                    SPParameters.Add("@DiskDirectory", DiskDirectory);

                    var List = await _unitOfWork.SP.ListAsnyc<string>(StoredProcedure.ADDCLOTHBRAND, SPParameters);
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
                    GetClothBrandByName(Form["ClothBrand.Name"].ToString().Trim(), 
                    Convert.ToInt32(Form["ClothBrand.Id"].ToString()));
                if (_clothBrand == null)
                {
                    var identityName = _userIdentity?.Name ?? "Unknown";
                    var clothBrand = new ClothBrandDTO
                    {
                        Id = Convert.ToInt32(Form["ClothBrand.Id"].ToString()),
                        Name = Form["ClothBrand.Name"].ToString(),
                    };

                    var UTDClothBrand = _mapper.Map<ClothBrandDTO, UTDClothBrandDTO>(clothBrand);
                    UTDClothBrand.ModifiedBy = identityName;
                    UTDClothBrand.ModifiedDate = DateTime.Now;
                    var ClothBrands = new List<UTDClothBrandDTO>
                    {
                       UTDClothBrand
                    };

                    var clothBrandtable = ObjectDatatableConverter.ToDataTable(ClothBrands);
                    var SPParameters = new DynamicParameters();
                    SPParameters.Add("@UDTClothBrand", clothBrandtable.AsTableValuedParameter(UDT.UDTCLOTHBRAND));
                    if (Form.Files[0].FileName != "No File" && Form.Files[0].Length > 0)
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
    }
}
