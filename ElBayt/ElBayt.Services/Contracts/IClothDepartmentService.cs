using ElBayt.Common.Enums;
using ElBayt.DTO.ELBayt.DBDTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ElBayt.Services.Contracts
{
    public interface IClothDepartmentService
    {
        public Task<ClothDepartmentDTO> AddNewClothDepartment(IFormCollection Form, string DiskDirectory);
        public Task<List<GetClothDepartmentDTO>> GetClothDepartments();
        public Task<string> DeleteClothDepartment(Guid Id);
        public Task<EnumUpdatingResult> UpdateClothDepartment(IFormCollection Form, string DiskDirectory, string MachineDirectory);
        public Task<ClothDepartmentDTO> GetClothDepartment(Guid Id);

    }
}
