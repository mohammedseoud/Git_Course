using ElBayt.Common.Enums;
using ElBayt.DTO.ELBayt.DBDTOs;
using ElBayt.DTO.ELBayt.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElBayt.Services.Contracts
{
    public interface IGeneralService
    {
        #region Colors
        public Task<EnumInsertingResult> AddNewColor(ColorDTO Color);
        public Task<List<GetColorDTO>> GetColors();
        public Task<string> DeleteColor(int Id);
        public Task<EnumUpdatingResult> UpdateColor(ColorDTO Color);
        public Task<ColorDTO> GetColor(int Id);
        #endregion


    }
}
