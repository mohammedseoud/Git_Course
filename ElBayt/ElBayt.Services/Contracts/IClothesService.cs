using ElBayt.Common.Enums;
using ElBayt.DTO.ELBayt.DBDTOs;
using ElBayt.DTO.ELBayt.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElBayt.Services.Contracts
{
    public interface IClothesService: IClothTypeService, IClothCategoryService, 
        IClothService, IClothDepartmentService, IClothBrandService, IClothSizeService
    {
             
        
    }
}
