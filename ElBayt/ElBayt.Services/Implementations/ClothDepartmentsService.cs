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
        #region Departments

        public async Task<string> AddNewClothDepartment(IFormCollection Form, string DiskDirectory)
        {
            try
            {
                var ClothDepartment = await _unitOfWork.ClothDepartmentRepository.
                    GetClothDepartmentByName(Form["ClothDepartment.Name"].ToString().Trim(), 0);
                if (ClothDepartment == null)
                {

                    var identityName = _userIdentity?.Name ?? "Unknown";

                    var clothDepartment = new ClothDepartmentDTO
                    {
                        Name = Form["ClothDepartment.Name"].ToString(),
                    };

                    
                    var clothDepartmentEntity = _mapper.Map<ClothDepartmentDTO, UTDClothDepartmentDTO>(clothDepartment);
                    clothDepartmentEntity.CreatedBy = identityName;
                    clothDepartmentEntity.CreatedDate = DateTime.Now;

                    var clothDepartments = new List<UTDClothDepartmentDTO>
                        {
                           clothDepartmentEntity
                        };

                    var clothDepartmenttable = ObjectDatatableConverter.ToDataTable(clothDepartments);
                    var SPParameters = new DynamicParameters();
                    SPParameters.Add("@UDTClothDepartment", clothDepartmenttable.AsTableValuedParameter(UDT.UDTCLOTHDEPARTMENT));
                    SPParameters.Add("@Extension", Path.GetExtension(Form.Files[0].FileName));
                    SPParameters.Add("@DiskDirectory", DiskDirectory);

                    var PicUrl = await _unitOfWork.SP.ListAsnyc<string>(StoredProcedure.ADDCLOTHDEPARTMENT, SPParameters);
                    return PicUrl.FirstOrDefault();
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

        public async Task<EnumUpdatingResult> UpdateClothDepartment(IFormCollection Form, string DiskDirectory, string machineDirectory)
        {
            try
            {
                var _clothDepartment = await _unitOfWork.ClothDepartmentRepository.
                    GetClothDepartmentByName(Form["ClothDepartment.Name"].ToString().Trim(),
                    Convert.ToInt32(Form["ClothDepartment.Id"].ToString()));
                if (_clothDepartment == null)
                {
                    var identityName = _userIdentity?.Name ?? "Unknown";
                    var clothDepartment = new ClothDepartmentDTO
                    {
                        Id = Convert.ToInt32(Form["ClothDepartment.Id"].ToString()),                      
                        Name = Form["ClothDepartment.Name"].ToString(),
                    };

                    var UTDClothDepartment = _mapper.Map<ClothDepartmentDTO, UTDClothDepartmentDTO>(clothDepartment);
                    UTDClothDepartment.ModifiedBy = identityName;
                    UTDClothDepartment.ModifiedDate = DateTime.Now;
                    var ClothDepartments = new List<UTDClothDepartmentDTO>
                    {
                       UTDClothDepartment
                    };

                    var clothDepartmenttable = ObjectDatatableConverter.ToDataTable(ClothDepartments);
                    var SPParameters = new DynamicParameters();
                    SPParameters.Add("@UDTClothDepartment", clothDepartmenttable.AsTableValuedParameter(UDT.UDTCLOTHDEPARTMENT));
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
                    var URL = await _unitOfWork.SP.ListAsnyc<string, string>(StoredProcedure.UPDATECLOTHDEPARTMENT, SPParameters);
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

    }
}
